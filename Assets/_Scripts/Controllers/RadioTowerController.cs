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

   [SerializeField] private GameObject _signalPrefab;
   [SerializeField] private GameObject _truckPrefab;
   [SerializeField] private GameObject[] _linkedReceiverObjects;
   [SerializeField] private float _maxDurability = 1f;
   [SerializeField] private float _transmitTime = 1f;
   private IMessageReceiver[] _linkedReceivers;
   [SerializeField] private float _durability;
   [SerializeField] private bool _needsRepair;

   public void ProcessMessage()
   {
      if (!Broken)
      {
         //Debug.Log(gameObject.name + " got a message, processing...");

         if (_linkedReceivers.Length > 0)
         {
            var receiverIndex = Random.Range(0, _linkedReceivers.Length);
            StartCoroutine(Transmit(_linkedReceivers[receiverIndex]));
         }
         else
         {
            //Debug.Log(gameObject.name + " got a message, but has no targets to send to");
         }
      }
      else
      {
         //todo - fizzle and pop amongst wreckage
         Debug.Log(gameObject.name + " got a message, broken! cant transmit!");
      }
   }

   private void Awake()
   {
      _durability = _maxDurability;
      _linkedReceivers = new IMessageReceiver[_linkedReceiverObjects.Length];

      for (var i = 0; i < _linkedReceiverObjects.Length; i++)
      {
         var receiver = _linkedReceiverObjects[i].GetComponent<IMessageReceiver>();
         _linkedReceivers[i] = receiver;

         if (receiver == null) Debug.LogError(gameObject.name + " has a GameObject in its linked receivers which isnt a MessageReceiver");
      }
   }

   private void Update()
   {
      if (Input.GetKeyDown("k"))
         RemoveDurability(1);
   }

   private IEnumerator Transmit(IMessageReceiver receiver)
   {
      yield return new WaitForSeconds(_transmitTime);

      foreach (GameObject go in _linkedReceiverObjects)
      {
         SignalController signal = Instantiate(_signalPrefab).GetComponent<SignalController>();
         signal.Initialize(transform.position, go.transform);
      }

   }

   public void RemoveDurability(float amount)
   {
      _durability -= amount;
      if (_durability < 0) _durability = 0;

      if (_durability <= 0)
      {
         _needsRepair = true;
         TruckController truck = Instantiate(_truckPrefab).GetComponent<TruckController>();
         truck.Initialize(this);
      }
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
      SpriteRenderer sr = GetComponent<SpriteRenderer>();
      sr.color = Color.yellow;

      while (_needsRepair)
      {
         AddDurability(0.1f);
         yield return new WaitForSeconds(0.15f);
      }

      sr.color = Color.black;
   }

   void OnDrawGizmos()
   {
      if (_linkedReceiverObjects != null && _linkedReceiverObjects.Length > 0)
      {
         foreach (GameObject go in _linkedReceiverObjects)
         {
            if (go != null)
               Debug.DrawLine(transform.position, go.transform.position, Color.red);
         }

      }
   }

}
