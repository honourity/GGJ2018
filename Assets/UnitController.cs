using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnitController : MonoBehaviour
{
   public float MaxHealth = 2;
   public float Health = 2;

   [SerializeField] private Image _healthBar;
   [SerializeField] private float _desiredDistance = 2.5f;
   [SerializeField] float _speed = 2f;

   [SerializeField] Transform targetTest;

   private float _initialScale;

   void Start()
   {
      _initialScale = transform.localScale.x;
   }

   void Update()
   {
      Vector3 playerPos = GameManager.Instance.PlayerController.transform.position;

      if (Vector2.Distance(transform.position, playerPos) > _desiredDistance)
      {
         //Move in closer to the player
         Vector3 diffVector = playerPos - transform.position;

         if (diffVector.x <= 0)
            transform.localScale = new Vector3(-_initialScale, _initialScale);
         else
            transform.localScale = new Vector3(_initialScale, _initialScale);

         transform.Translate((diffVector) * _speed * Time.deltaTime);

      }
      else
      {
         //Close enough to the player
      }

   }

   public void TakeDamage(float amount)
   {
      Health -= amount;
   }

}
