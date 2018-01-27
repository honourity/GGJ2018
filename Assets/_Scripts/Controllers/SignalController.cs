using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalController : MonoBehaviour
{
   [SerializeField] Transform _target;
   private IMessageReceiver _messageReceiver;
   [SerializeField] float _desiredDistance;
   [SerializeField] float _speed;
   private Vector3 _diffVector;

   private void Awake()
   {
   }

   public void Initialize(Vector3 from, Transform to)
   {
      transform.position = from;
      _messageReceiver = to.GetComponent<IMessageReceiver>();
      _target = to.transform;
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
