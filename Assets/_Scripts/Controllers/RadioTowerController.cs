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

   [SerializeField] private GameObject _signalPrefab = null;
   [SerializeField] private GameObject _truckPrefab = null;
   [SerializeField] private GameObject _blipBlipPrefab = null;
   [SerializeField] private GameObject[] _linkedReceiverObjects = null;
   [SerializeField] private float _maxDurability = 3f;
   [SerializeField] private float _transmitTime = 1f;
   private IMessageReceiver[] _linkedReceivers;
   [SerializeField] private bool _needsRepair;
   [SerializeField] private Color _DurabilityLossBlinkColor = Color.red;
   [SerializeField] private Transform _signalTarget;

   private Animator _animator;
   private SpriteRenderer _sprite;
   private Color _originalSpriteColor;
   private Vector3 _originalPosition;
   private Coroutine _transmitCoroutine;

   private GameObject _blipBlip;
   private float _durability;
   private Animator _blipBlipAnimator;


   public Transform GetSignalTarget()
   {
      return _signalTarget;
   }

   public void ProcessMessage()
   {
      _blipBlip = Instantiate(_blipBlipPrefab, transform.GetChild(0).position, Quaternion.identity);
      _blipBlipAnimator = _blipBlip.GetComponent<Animator>();

      if (!Broken)
      {
         if (_linkedReceivers.Length > 0)
         {
            var receiverIndex = Random.Range(0, _linkedReceivers.Length);
            _transmitCoroutine = StartCoroutine(Transmit(_linkedReceivers[receiverIndex]));
         }
      }
      else
      {
         _blipBlipAnimator.Play("SignalFade");
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

   private void Update()
   {
      if (Input.GetKeyDown("k"))
         RemoveDurability(1);
   }

   private IEnumerator Transmit(IMessageReceiver receiver)
   {
      if (!Broken)
      {
         _blipBlipAnimator.Play("BlipBlip");
         yield return new WaitForSeconds(_transmitTime);

         foreach (GameObject go in _linkedReceiverObjects)
         {
            var signal = Instantiate(_signalPrefab).GetComponent<SignalController>();
            signal.Initialize(_signalTarget.position, receiver.GetSignalTarget(), receiver);
            if (_blipBlip != null) Destroy(_blipBlip);
         }
      }

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
            if (_blipBlip)
               _blipBlipAnimator.Play("SignalFade");
            _needsRepair = true;
            TruckController truck = Instantiate(_truckPrefab).GetComponent<TruckController>();
            truck.Initialize(this);
            if (_transmitCoroutine != null)
               StopCoroutine(_transmitCoroutine);
            if (_blipBlip != null)
               Destroy(_blipBlip, 1.5f);
            _animator.SetBool("broken", true);
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

      while (_needsRepair)
      {
         AddDurability(0.1f);
         yield return new WaitForSeconds(0.15f);
      }

      _animator.SetBool("repairing", false);
      _animator.SetBool("broken", false);
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
