using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBaseController : MonoBehaviour
{
   [SerializeField] private RadioTowerController[] _towersToSend;
   private IMessageReceiver[] _linkedReceivers;
   [SerializeField] GameObject _signalsPrefab;

   // Use this for initialization
   void Start()
   {
      SignalController signals = Instantiate(_signalsPrefab).GetComponent<SignalController>();
      _linkedReceivers = new IMessageReceiver[_towersToSend.Length];

      //Build the _linkedReceivers Array

      for (int i = 0; i < _towersToSend.Length; i++)
      {
         _linkedReceivers[i] = _towersToSend[i].GetComponent<IMessageReceiver>();
      }


      signals.Initialize(transform.position, _towersToSend[0]);
   }

   // Update is called once per frame
   void Update()
   {

   }

   void OnDrawGizmos()
   {
      if (_towersToSend != null && _towersToSend.Length > 0)
      {
         foreach (RadioTowerController radio in _towersToSend)
         {
            if (radio != null)
               Debug.DrawLine(transform.position, radio.transform.position, Color.red);
         }
      }
   }
}
