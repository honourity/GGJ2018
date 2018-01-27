using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBaseController : MonoBehaviour
{
   [SerializeField] private Transform[] _towersToSend;
   private IMessageReceiver[] _linkedReceivers;
   [SerializeField] GameObject _signalsPrefab;

   [SerializeField] float _timeBetweenSignals;

   // Use this for initialization
   void Start()
   {
      _linkedReceivers = new IMessageReceiver[_towersToSend.Length];

      //Build the _linkedReceivers Array
      //for (int i = 0; i < _towersToSend.Length; i++)
      //{
      //   _linkedReceivers[i] = _towersToSend[i].GetComponent<IMessageReceiver>();
      //}
      StartCoroutine(DoStuffIGuess());
   }

   private IEnumerator DoStuffIGuess()
   {
      while (true)
      {
         yield return new WaitForSeconds(_timeBetweenSignals);
         int randIndex = Random.Range(0, _towersToSend.Length);

         SignalController signal = Instantiate(_signalsPrefab).GetComponent<SignalController>();
         signal.Initialize(transform.position, _towersToSend[randIndex]);
      }

   }

   void OnDrawGizmos()
   {
      if (_towersToSend != null && _towersToSend.Length > 0)
      {
         foreach (Transform radio in _towersToSend)
         {
            if (radio != null)
               Debug.DrawLine(transform.position, radio.position, Color.red);
         }
      }
   }
}
