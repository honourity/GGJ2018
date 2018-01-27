using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalController : MonoBehaviour
{
   [SerializeField] Transform _target;
   private IMessageReceiver _messageReceiver;
   [SerializeField] float _desiredDistance;
   [SerializeField] float _speed;
   private float _initialScale;
   private Vector3 _diffVector;

   private void Awake()
   {
      _initialScale = transform.localScale.x;
   }

   public void Initialize(Vector3 from, RadioTowerController radioTower)
   {
      transform.position = from;
      _messageReceiver = radioTower.GetComponent<IMessageReceiver>();
      _target = radioTower.transform;
      _diffVector = _target.position - transform.position;
   }

   private void Update()
   {
      if (Vector2.Distance(transform.position, _target.transform.position) > _desiredDistance)
      {
         transform.Translate((_diffVector) * _speed * Time.deltaTime);
      }
      else
      {
         _messageReceiver.ProcessMessage();
         Destroy(gameObject);
      }
   }
}
