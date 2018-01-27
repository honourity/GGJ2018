using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DudeController : UnitController
{
   [SerializeField] private float _desiredDistance = 2.5f;
   [SerializeField] private Image _healthBar;
   private float _initialScale;

   private void Start()
   {
      _initialScale = transform.localScale.x;
   }

   // Update is called once per frame
   private void Update()
   {
      Vector3 playerPos = GameManager.Instance.Player.transform.position;

      if (Vector2.Distance(transform.position, playerPos) > _desiredDistance)
      {
         //Move in closer to the player
         Vector3 diffVector = playerPos - transform.position;

         if (diffVector.x <= 0)
            transform.localScale = new Vector3(-_initialScale, _initialScale);
         else
            transform.localScale = new Vector3(_initialScale, _initialScale);

         transform.position = Vector3.Slerp(transform.position, playerPos, _speed * Time.deltaTime);
         //transform.Translate((diffVector) * _speed * Time.deltaTime);

      }
      else
      {
         //Close enough to the player
      }
   }
}
