using UnityEngine;

public class SignalController : MonoBehaviour
{
   private Transform _target;
   private IMessageReceiver _messageReceiver;
   private float _desiredDistance = 0.5f;
   private float _speed = 2f;
   private Vector3 _diffVector;

   public void Initialize(Vector3 start, Transform end, IMessageReceiver receiver)
   {
      transform.position = start;
      _target = end.transform;
      _messageReceiver = receiver;
      _diffVector = _target.position - transform.position;
      transform.right = -_diffVector;
   }

   private void Update()
   {
      if (Vector2.Distance(transform.position, _target.transform.position) > _desiredDistance)
      {
         transform.Translate((_diffVector.normalized) * _speed * Time.deltaTime, Space.World);
      }
      else
      {
         _messageReceiver.ProcessMessage();
         Destroy(gameObject);
      }
   }
}
