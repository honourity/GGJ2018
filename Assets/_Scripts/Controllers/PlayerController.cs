using System;
using System.Collections;
using UnityEngine;

public class PlayerController : UnitController
{
   [SerializeField]
   private Color _killColor = Color.red;

   private Animator _animator;
   private SpriteRenderer _sprite;
   private Color _originalSpriteColor;
   private Enums.Directions _previousDirection = Enums.Directions.Right;
   private bool _invulnerable;

   public override void TakeDamage(float damage)
   {
      if (!_invulnerable)
      {
         base.TakeDamage(damage);
         EventManager.FireEvent("PlayerTakeDamage");
         StopAllCoroutines();
         StartCoroutine(TakingDamageCoroutine());
      }
   }

   public void StopMoving()
   {
      _animator.SetBool("moving", false);
   }

   private IEnumerator TakingDamageCoroutine()
   {
      InputManager.Instance.InputLocked = true;

      var colorFrame = true;
      var timer = 0.05f;

      while (timer > 0f)
      {
         _sprite.color = colorFrame ? _killColor : _originalSpriteColor;
         colorFrame = !colorFrame;

         timer -= Time.deltaTime;
         yield return new WaitForSeconds(0.025f);
      }

      _sprite.color = _originalSpriteColor;

      InputManager.Instance.InputLocked = false;

      yield return null;
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

      //todo - attempting to make direction sticky (pressing left, then up/left should keep direction facing left)
      // (doesnt work properly) - make it work if we have time
      var adjustedDirection = direction;
      Enums.Directions tempPositive = (((int)direction + 1) > 7) ? (Enums.Directions)0 : direction;
      Enums.Directions tempNegative = (((int)direction - 1) < 0) ? (Enums.Directions)7 : direction;
      if (_previousDirection == tempPositive)
      {
         adjustedDirection = tempPositive;
      }
      else if (_previousDirection == tempNegative)
      {
         adjustedDirection = tempNegative;
      }

      if (adjustedDirection != direction)
      {
         _previousDirection = direction;
      }

      //convert 8-way to 4-way direction for animation purposes
      if ((int)adjustedDirection >= 4)
      {
         adjustedDirection -= 4;
      }

      //set animation
      _animator.SetFloat("direction", (int)adjustedDirection);
      _animator.SetBool("moving", true);

      transform.Translate(translation.normalized * _speed * Time.deltaTime);
   }

   public void Ultimate()
   {
      _animator.SetTrigger("ultimate");
   }

   public void Attack()
   {
      _animator.SetTrigger("attack");
   }

   private void Awake()
   {
      Health = MaxHealth;

      _animator = GetComponentInChildren<Animator>();
      _sprite = GetComponentInChildren<SpriteRenderer>();
      _originalSpriteColor = _sprite.color;
   }
}
