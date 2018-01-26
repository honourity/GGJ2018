using UnityEngine;

public class MissileSiloController : MonoBehaviour, IMessageReceiver
{
   [SerializeField]
   private GameObject _missilePrefab;

   public void ProcessMessage()
   {
      Instantiate(_missilePrefab, transform.position, Quaternion.identity, null);

      Debug.Log("Missile launch detected from: " + gameObject.name);
   }
}
