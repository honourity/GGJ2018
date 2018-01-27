using System.Collections;
using UnityEngine;

public class RadioTowerController : MonoBehaviour, IMessageReceiver
{
   [SerializeField] private GameObject _truckPrefab;
   [SerializeField] private GameObject[] _linkedReceiverObjects;
   private float _transmitTime = 1f;
   private IMessageReceiver[] _linkedReceivers;

   public void ProcessMessage()
   {
      Debug.Log(gameObject.name + " got a message, processing...");

      var receiverIndex = Random.Range(0, _linkedReceivers.Length);
      StartCoroutine(Transmit(_linkedReceivers[receiverIndex]));
   }

   private void Start()
   {
      _linkedReceivers = new IMessageReceiver[_linkedReceiverObjects.Length];

      for (var i = 0; i < _linkedReceiverObjects.Length; i++)
      {
         var receiver = _linkedReceiverObjects[i].GetComponent<IMessageReceiver>();

         _linkedReceivers[i] = receiver;

         if (receiver == null) Debug.LogError(gameObject.name + " has a GameObject in its linked receivers which isnt a MessageReceiver");
      }

      //TruckController truck = Instantiate(_truckPrefab).GetComponent<TruckController>();
      //truck.Initialize(transform.position);

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

   public void TakeDamage(float amount)
   {
   }

   public void AddHealth(float amount)
   {

   }
}
