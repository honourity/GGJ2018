using UnityEngine;
using UnityEngine.UI;


public class UnitController : MonoBehaviour
{
   [SerializeField] private float _maxHealth = 10f;
   public float MaxHealth { get { return _maxHealth; } }
   public float Health { get; protected set; }

   [SerializeField] private Image _healthBar;
   [SerializeField] private float _desiredDistance = 2.5f;
   [SerializeField] float _speed = 2f;

   [SerializeField] Transform targetTest;

   private float _initialScale;

   void Start()
   {
      _initialScale = transform.localScale.x;
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

      transform.Translate(translation.normalized * _speed * Time.deltaTime);
   }

   void Update()
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

   public virtual void TakeDamage(float amount)
   {
      Health -= amount;
   }

}
