using UnityEngine;

public class MissileExplosionController : MonoBehaviour
{
   private void Start()
   {
      Destroy(gameObject, 2f);
   }
}
