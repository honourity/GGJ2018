using System.Collections;
using UnityEngine;

public class PlayerController : UnitController
{
   public bool Invulnerable { get; set; }
   public bool Dead { get; private set; }
   public float MaxUltimateCharge { get { return _maxUltimateCharge; } }
   public float UltimateCharge { get; private set; }

   [SerializeField]
   private Color _killColor = Color.red;
   [SerializeField]
   private float _maxUltimateCharge = 2f;
   [SerializeField]
   private float _attackDamage = 1f;
   [SerializeField]
   private float _attackRange = 1f;
   [SerializeField]
   private float _ultimateRange = 2f;

   private Animator _animator;
   private SpriteRenderer _sprite;
   private Color _originalSpriteColor;
   private Enums.Directions _previousDirection = Enums.Directions.Right;

   public override void TakeDamage(float damage)
   {
      if (!Invulnerable)
      {
         base.TakeDamage(damage);
         EventManager.FireEvent("PlayerTakeDamage");
         StopAllCoroutines();
         StartCoroutine(TakingDamageCoroutine());
      }
   }

   private void Die()
   {
      Dead = true;
      Invulnerable = true;
      _animator.SetTrigger("die");
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

      if (Health <= 0) Die();

      while (timer > 0f)
      {
         _sprite.color = colorFrame ? _killColor : _originalSpriteColor;
         colorFrame = !colorFrame;

         timer -= Time.deltaTime;
         yield return new WaitForSeconds(0.025f);
      }

      _sprite.color = _originalSpriteColor;

      if (!Dead)
      {
         InputManager.Instance.InputLocked = false;
      }

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
      adjustedDirection = Helpers.Compress8to4Directions(adjustedDirection);
      

      //set animation
      _animator.SetFloat("direction", (int)adjustedDirection);
      _animator.SetBool("moving", true);

      transform.Translate(translation.normalized * _speed * Time.deltaTime);
   }

   public void Ultimate()
   {
      if (UltimateCharge == MaxUltimateCharge)
      {
         Invulnerable = true;
         UltimateCharge = 0;
         _animator.SetTrigger("ultimate");
         EventManager.FireEvent("PlayerCharge");

         var allTowers = FindObjectsOfType<RadioTowerController>();
         foreach (var tower in allTowers)
         {
            if (!tower.Broken && Vector3.Distance(tower.transform.position, transform.position) < _ultimateRange)
            {
               tower.RemoveDurability(9001);
            }
         }

         var allMissiles = FindObjectsOfType<MissileController>();
         foreach (var missile in allMissiles)
         {
            if (Vector3.Distance(missile.transform.position, transform.position) < _ultimateRange)
            {
               missile.Explode();
            }
         }
      }
   }

   public void Attack()
   {
      _animator.SetTrigger("attack");

      var allTowers = FindObjectsOfType<RadioTowerController>();
      foreach (var tower in allTowers)
      {
         if (!tower.Broken && Vector3.Distance(tower.transform.position, transform.position) < _attackRange)
         {
            tower.RemoveDurability(_attackDamage);
         }
      }
   }

   private void Awake()
   {
      Health = MaxHealth;

      _animator = GetComponentInChildren<Animator>();
      _sprite = GetComponentInChildren<SpriteRenderer>();
      _originalSpriteColor = _sprite.color;
   }

   private void OnTriggerStay2D(Collider2D collision)
   {
      if (collision.CompareTag("RadioTower"))
      {
         UltimateCharge += Time.deltaTime;
         if (UltimateCharge > MaxUltimateCharge) UltimateCharge = MaxUltimateCharge;
         EventManager.FireEvent("PlayerCharge");
      }
   }
}
