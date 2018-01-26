using UnityEngine;

public class MissileController : MonoBehaviour
{
   private float _upDistance = 2f;
   private float _launchSpeed = 4f;
   private float _acceleration = 1f;

   private Vector3 _initialPosition;
   private bool _finishedLaunching;
   private float _currentSpeed;

   private void Awake()
   {
      _initialPosition = transform.position;
   }

   private void Update()
   {
      if (_finishedLaunching)
      {
         //fly at player
         var vectorToPlayer = (GameManager.Instance.PlayerController.transform.position - transform.position).normalized;
         _currentSpeed += _acceleration;
         transform.position += vectorToPlayer * _currentSpeed * Time.deltaTime;

         //look at player
         transform.up = vectorToPlayer;

         if (Vector3.Distance(transform.position, GameManager.Instance.PlayerController.transform.position) <= 0.5f)
         {
            Explode();
         }
      }
      else
      {
         //go straight up
         if (Vector3.Distance(transform.position, _initialPosition) < _upDistance)
         {
            transform.Translate(Vector3.up * _launchSpeed * Time.deltaTime);
         }
         else
         {
            _finishedLaunching = true;
         }
      }
   }

   private void Explode()
   {
      GameManager.Instance.PlayerController.Health--;
      Destroy(gameObject);
   }
}
