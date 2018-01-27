using System.Collections;
using UnityEngine;

public class MainBaseController : MonoBehaviour
{
   [SerializeField] private GameObject[] _towersToSend;
   [SerializeField] GameObject _signalsPrefab;
   [SerializeField] float _timeBetweenSignals;

   private RadioTowerController[] _linkedReceivers;

   private void Start()
   {
      _linkedReceivers = new RadioTowerController[_towersToSend.Length];

      //Build the _linkedReceivers Array
      for (int i = 0; i < _towersToSend.Length; i++)
      {
         _linkedReceivers[i] = _towersToSend[i].GetComponent<RadioTowerController>();
      }
      StartCoroutine(DoStuffIGuess());
   }

   private IEnumerator DoStuffIGuess()
   {
      while (true)
      {
         yield return new WaitForSeconds(_timeBetweenSignals);
         int randIndex = Random.Range(0, _linkedReceivers.Length);

         SignalController signal = Instantiate(_signalsPrefab).GetComponent<SignalController>();
         signal.Initialize(transform.position, _linkedReceivers[randIndex].SignalTarget, _linkedReceivers[randIndex]);
      }

   }

   void OnDrawGizmos()
   {
      if (_towersToSend != null && _towersToSend.Length > 0)
      {
         foreach (var radio in _towersToSend)
         {
            if (radio != null) Debug.DrawLine(transform.position, radio.transform.position, Color.red);
         }
      }
   }
}
