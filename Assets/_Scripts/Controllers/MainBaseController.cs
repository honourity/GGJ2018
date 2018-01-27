using System.Collections;
using UnityEngine;

public class MainBaseController : MonoBehaviour
{
   [SerializeField] private GameObject[] _towersToSend;
   [SerializeField] GameObject _signalsPrefab;
   [SerializeField] float _timeBetweenSignals;
   [SerializeField] Transform _signalSource;

   private IMessageReceiver[] _linkedReceivers;

   private void Start()
   {
      _linkedReceivers = new IMessageReceiver[_towersToSend.Length];

      //Build the _linkedReceivers Array
      for (int i = 0; i < _towersToSend.Length; i++)
      {
         _linkedReceivers[i] = _towersToSend[i].GetComponent<IMessageReceiver>();
      }
      StartCoroutine(Transmitting());
   }

   private IEnumerator Transmitting()
   {
      while (true)
      {
         yield return new WaitForSeconds(_timeBetweenSignals);
         int randIndex = Random.Range(0, _linkedReceivers.Length);

         var signal = Instantiate(_signalsPrefab).GetComponent<SignalController>();
         signal.Initialize(_signalSource.position, _linkedReceivers[randIndex].GetSignalTarget(), _linkedReceivers[randIndex]);
      }
   }

   void OnDrawGizmos()
   {
      if (_linkedReceivers != null && _linkedReceivers.Length > 0)
      {
         foreach (var receiver in _linkedReceivers)
         {
            if (receiver != null) Debug.DrawLine(_signalSource.position, receiver.GetSignalTarget().position, Color.red);
         }
      }
   }
}
