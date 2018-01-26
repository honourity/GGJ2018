using System.Collections;
using UnityEngine;

public class RadioTowerController : MonoBehaviour, IMessageReceiver
{
   public bool Broken { get
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

   [SerializeField]
   private GameObject[] _linkedReceiverObjects;

   [SerializeField]
   private float _maxDurability = 1f;
   [SerializeField]
   private float _transmitTime = 1f;
   private IMessageReceiver[] _linkedReceivers;
   private float _durability;
   private bool _needsRepair;

   public void ProcessMessage()
   {
      Debug.Log(gameObject.name + " got a message, processing...");

      var receiverIndex = Random.Range(0, _linkedReceivers.Length);
      StartCoroutine(Transmit(_linkedReceivers[receiverIndex]));
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

   private IEnumerator Transmit(IMessageReceiver receiver)
   {
      var timer = 0f;

      while (timer < _transmitTime)
      {
         //todo - animate?!?

         timer -= Time.deltaTime;
         yield return new WaitForEndOfFrame();
      }

      receiver.ProcessMessage();

      yield return null;
   }

   public void RemoveDurability(float amount)
   {
      _durability -= amount;
      if (_durability < 0) _durability = 0;

      if (_durability == 0)
      {
         _needsRepair = true;
      }
   }

   public void AddDurability(float amount)
   {
      _durability += amount;
      if (_maxDurability < 0) _maxDurability = 0;

      if (_durability == _maxDurability)
      {
         _needsRepair = false;
      }
   }
}
