using System;
using UnityEngine;

public class PlayerController : UnitController
{
   private Animator _animator;
   private Enums.Directions _lastDirection;

   public override void TakeDamage(float damage)
   {
      base.TakeDamage(damage);
      EventManager.FireEvent("PlayerTakeDamage");
   }

   public void StopMoving()
   {
      _animator.SetBool("moving", false);
   }

   public void Move(Enums.Directions direction)
   {
      Vector3 translation = Vector3.zero;

      switch (direction)
      {
         case Enums.Directions.Up:
            translation = Vector3.up;
            break;
         case Enums.Directions.UpRight:
            translation = Vector3.up + Vector3.right;
            break;
         case Enums.Directions.Right:
            
            translation = Vector3.right;
            break;
         case Enums.Directions.DownRight:
            translation = Vector3.down + Vector3.right;
            break;
         case Enums.Directions.Down:
            translation = Vector3.down;
            break;
         case Enums.Directions.DownLeft:
            translation = Vector3.down + Vector3.left;
            break;
         case Enums.Directions.Left:
            translation = Vector3.left;
            break;
         case Enums.Directions.UpLeft:
            translation = Vector3.up + Vector3.left;
            break;
         default:
            translation = Vector3.zero;
            break;
      }

      //convert 8-way to 4-way direction for animation purposes
      if ((int)direction >= 4)
      {
         direction -= 4;
      }

      //set animation
      _animator.SetFloat("direction", (int)direction);
      _animator.SetBool("moving", true);

      transform.Translate(translation.normalized * _speed * Time.deltaTime);
   }

   public void Jump()
   {
      throw new NotImplementedException();
   }

   public void Attack()
   {
      throw new NotImplementedException();
   }

   public void Ultimate()
   {
      throw new NotImplementedException();
   }

   private void Awake()
   {
      _animator = GetComponentInChildren<Animator>();
   }
}
