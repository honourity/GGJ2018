using System.Collections;
using UnityEngine;

public class RadioTowerController : MonoBehaviour, IMessageReceiver
{
   public bool Broken
   {
      get
      {
         if (_needsRepair && _durability < _maxDurability)
         {
            return true;
         }
         else
         {
            return false;
         }
      }
   }

   public bool Transmitting { get; private set; }

   [SerializeField] private SignalController _signalPrefab = null;
   [SerializeField] private GameObject _truckPrefab = null;
   [SerializeField] private SignalPulseController _signalPulsePrefab = null;
   [SerializeField] private GameObject[] _linkedReceiverObjects = null;
   [SerializeField] private float _maxDurability = 3f;
   [SerializeField] private float _transmitTime = 1f;
   private IMessageReceiver[] _linkedReceivers;
   [SerializeField] private bool _needsRepair;
   [SerializeField] private Color _DurabilityLossBlinkColor = Color.red;

   public void ResetThing()
   {
      _durability = _maxDurability;
      _needsRepair = false;
      Transmitting = false;
      StopAllCoroutines();
      _animator.SetBool("repairing", false);
      _animator.SetBool("broken", false);
   }

   [SerializeField] private Transform _signalTarget = null;
   [SerializeField] private GameObject _repairSpriteGameObject = null;

   private Animator _animator;
   private SpriteRenderer _sprite;
   private Color _originalSpriteColor;
   private Vector3 _originalPosition;
   private Coroutine _transmitCoroutine;
   private SignalPulseController _currentSignalPulse;

   private float _durability;

   public Transform GetSignalTarget()
   {
      return _signalTarget;
   }

   public void ProcessMessage()
   {
      if (!Transmitting)
      {
         if (_currentSignalPulse == null) _currentSignalPulse = Instantiate(_signalPulsePrefab, _signalTarget.position, Quaternion.identity, null);

         if (!Broken)
         {
            if (_linkedReceivers.Length > 0)
            {
               var receiverIndex = UnityEngine.Random.Range(0, _linkedReceivers.Length);
               _transmitCoroutine = StartCoroutine(Transmit(_linkedReceivers[receiverIndex]));
            }
         }
         else
         {
            if (_currentSignalPulse != null) _currentSignalPulse.Animator.SetTrigger("Fizzle");
         }
      }
   }

   private void Awake()
   {
      _durability = _maxDurability;
      _linkedReceivers = new IMessageReceiver[_linkedReceiverObjects.Length];
      _sprite = GetComponent<SpriteRenderer>();
      _originalSpriteColor = _sprite.color;
      _originalPosition = transform.position;
      _animator = GetComponent<Animator>();

      for (var i = 0; i < _linkedReceiverObjects.Length; i++)
      {
         var receiver = _linkedReceiverObjects[i].GetComponent<IMessageReceiver>();
         if (receiver == null) Debug.LogError(gameObject.name + " has a GameObject in its linked receivers which isnt a MessageReceiver");
         _linkedReceivers[i] = receiver;
      }
   }

   private IEnumerator Transmit(IMessageReceiver receiver)
   {
      Transmitting = true;
      
      yield return new WaitForSeconds(_transmitTime);

      var signal = Instantiate(_signalPrefab);
      signal.Initialize(_signalTarget.position, receiver.GetSignalTarget(), receiver);

      _currentSignalPulse.Animator.SetTrigger("Transmitted");

      Transmitting = false;

      yield return null;
   }

   public void RemoveDurability(float amount)
   {
      if (!Broken)
      {
         _durability -= amount;
         if (_durability < 0) _durability = 0;

         if (_durability <= 0)
         {
            if (_currentSignalPulse != null) _currentSignalPulse.Animator.SetTrigger("Fizzle");
            _needsRepair = true;
            TruckController truck = Instantiate(_truckPrefab).GetComponent<TruckController>();
            truck.Initialize(this);
            if (_transmitCoroutine != null) StopCoroutine(_transmitCoroutine);
            _animator.SetBool("broken", true);
            Transmitting = false;
         }

         StopCoroutine(DamageBlinkCoroutine());
         StopCoroutine(DamageShakeCoroutine());
         StartCoroutine(DamageBlinkCoroutine());
         StartCoroutine(DamageShakeCoroutine());
      }
   }

   private IEnumerator DamageBlinkCoroutine()
   {
      var colorFrame = true;
      var timer = 0.1f;

      while (timer > 0f)
      {
         _sprite.color = colorFrame ? _DurabilityLossBlinkColor : _originalSpriteColor;
         colorFrame = !colorFrame;

         timer -= Time.deltaTime;
         yield return new WaitForSeconds(0.05f);
      }

      _sprite.color = _originalSpriteColor;

      yield return null;
   }

   private IEnumerator DamageShakeCoroutine()
   {
      var timer = 0.1f;
      var shakeAmount = 0.5f;
      var positiveShake = true;

      transform.position -= new Vector3(shakeAmount / 2f, 0f, 0f);

      while (timer > 0f)
      {
         var shakeOffset = new Vector3(shakeAmount, 0f, 0f);

         if (!positiveShake) shakeOffset = -shakeOffset;

         transform.position += shakeOffset;

         shakeAmount *= 0.5f;

         positiveShake = !positiveShake;

         timer -= Time.deltaTime;
         yield return new WaitForSeconds(0.005f);
      }

      transform.position = _originalPosition;

      yield return null;
   }

   public void AddDurability(float amount)
   {
      _durability += amount;
      if (_maxDurability < 0) _maxDurability = 0;

      if (_durability >= _maxDurability)
      {
         _needsRepair = false;
      }
   }

   public IEnumerator Repair()
   {
      _animator.SetBool("repairing", true);
      _repairSpriteGameObject.SetActive(true);

      while (_needsRepair)
      {
         AddDurability(0.1f);
         yield return new WaitForSeconds(0.15f);
      }

      _repairSpriteGameObject.SetActive(false);
      _animator.SetBool("repairing", false);
      _animator.SetBool("broken", false);

      yield return null;
   }

   void OnDrawGizmos()
   {
      if (_linkedReceivers != null && _linkedReceivers.Length > 0)
      {
         foreach (var receiver in _linkedReceivers)
         {
            if (receiver != null) Debug.DrawLine(_signalTarget.position, receiver.GetSignalTarget().position, Color.red);
         }

      }
   }

}
