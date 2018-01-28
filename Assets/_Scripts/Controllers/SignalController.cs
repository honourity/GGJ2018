using UnityEngine;

public class SignalController : MonoBehaviour
{
   private Transform _target;
   private IMessageReceiver _messageReceiver;
   [SerializeField] private float _desiredDistance = 0.02f;
   private float _speed = 2f;
   private Vector3 _diffVector;
   private Animator _animator;
   private bool _messageDelivered;

   public void Initialize(Vector3 start, Transform end, IMessageReceiver receiver)
   {
      transform.position = start;
      _target = end.transform;
      _messageReceiver = receiver;
   }

   private void Awake()
   {
      _animator = GetComponent<Animator>();
   }

   private void Update()
   {
      if (Vector2.Distance(transform.position, _target.transform.position) > _desiredDistance)
      {
         _diffVector = _target.position - transform.position;
         transform.Translate((_diffVector.normalized) * _speed * Time.deltaTime, Space.World);

         SetDirection();
      }
      else
      {
         if (!_messageDelivered)
         {
            _messageReceiver.ProcessMessage();
            _messageDelivered = true;
         }
         
         Destroy(gameObject);
      }
   }

   private void SetDirection()
   {
      var adjustedAngle = Vector3.SignedAngle(Vector3.up, _diffVector, Vector3.back);
      if (adjustedAngle < 0) adjustedAngle += 360f;

      //force only 4-way since the way the Enums.Directions is configured, we cant move around cardinals in sequential order
      var directionValue = Mathf.RoundToInt(adjustedAngle / 45f);
      if (directionValue == 8) directionValue = 0;
      var direction = (Enums.Directions8Way)directionValue;
      _animator.SetFloat("direction", (int)direction);
   }
}
