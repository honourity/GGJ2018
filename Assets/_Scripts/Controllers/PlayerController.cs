using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField]
   private float _maxHealth = 10f;
   public float MaxHealth { get { return _maxHealth; } }
   public float Health { get; private set; }
   
   [SerializeField]
   private float _speed;

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

      transform.Translate(translation.normalized * _speed * Time.deltaTime);
   }

   public void TakeDamage(float damage)
   {
      Health -= damage;
      EventManager.FireEvent("TakeDamage");
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
}
