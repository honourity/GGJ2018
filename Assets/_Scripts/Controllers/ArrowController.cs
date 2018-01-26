using UnityEngine;

public class ArrowController : MonoBehaviour
{
   public GameObject Target;

   [SerializeField]
   private SpriteRenderer _sprite;

   private bool _initialized;

   public void Initialize(GameObject target)
   {
      Target = target;
      _initialized = true;
   }

   private void Update()
   {
      if (Target && _initialized)
      {
         //look at missile
         transform.up = (Target.transform.position - transform.position).normalized;

         //stick to edge of screen
         var screenMiddle = new Vector3(GameManager.Instance.Camera.transform.position.x, GameManager.Instance.Camera.transform.position.y, 0f);
         var vectorMiddleToMissile = Target.transform.position - screenMiddle;
         var arrowOffsetPosition = screenMiddle + vectorMiddleToMissile;

         var clampedArrowOffsetPosition = new Vector3(Mathf.Clamp(arrowOffsetPosition.x, 0f, 1f), Mathf.Clamp(arrowOffsetPosition.y, 0f, 1f), 0f);

         transform.position = clampedArrowOffsetPosition;

         //from centre of screen, to missile, get vector
         //offset by sprite bounds
         // clamp to screen x and y


         
         //var asdf = Camera.main.ScreenToWorldPoint(new Vector3(300f, 300f, 0));
         //transform.position = new Vector3(asdf.x, asdf.y, 0f);






      }
      else if (!Target && _initialized)
      {
         Destroy(gameObject);
      }
   }
}
