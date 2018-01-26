using UnityEngine;

public class MissileSiloController : MonoBehaviour
{
   [SerializeField]
   private GameObject _missilePrefab;

   public void Launch()
   {
      Instantiate(_missilePrefab, transform.position, Quaternion.identity, null);
   }
}
