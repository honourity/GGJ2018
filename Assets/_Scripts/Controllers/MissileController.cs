using UnityEngine;

public class MissileController : MonoBehaviour
{
   [SerializeField]
   private ArrowController _arrowPrefab = null;

   private float _upDistance = 2f;
   private float _launchSpeed = 4f;
   private float _acceleration = 1f;

   private Vector3 _initialPosition;
   private bool _finishedLaunching;
   private float _currentSpeed;
   private ArrowController _arrowInstance;
   private bool _visible;
   private bool _arrowCreated;

   private void Awake()
   {
      _initialPosition = transform.position;
   }

   private void Update()
   {
      if (_finishedLaunching)
      {
         if (!_visible && !_arrowCreated)
         {
            _arrowInstance = Instantiate(_arrowPrefab, transform.position, Quaternion.identity, null);
            _arrowInstance.Initialize(gameObject);
            _arrowCreated = true;
         }

         //fly at player
         var vectorToPlayer = (GameManager.Instance.Player.transform.position - transform.position).normalized;
         _currentSpeed += _acceleration;
         transform.position += vectorToPlayer * _currentSpeed * Time.deltaTime;

         //look at player
         transform.up = vectorToPlayer;

         if (Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) <= 0.5f)
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
      GameManager.Instance.Player.Health--;
      Destroy(gameObject);
   }

   private void OnBecameVisible()
   {
      _visible = true;
   }

   private void OnBecameInvisible()
   {
      _visible = false;
   }
}
