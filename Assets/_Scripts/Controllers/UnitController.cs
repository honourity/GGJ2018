using UnityEngine;
using UnityEngine.UI;


public class UnitController : MonoBehaviour
{
   [SerializeField] private float _maxHealth = 10f;
   public float MaxHealth { get { return _maxHealth; } }
   public float Health { get; protected set; }

   [SerializeField] protected float _speed = 2f;

   void Update()
   {

   }

   public virtual void TakeDamage(float amount)
   {
      Health -= amount;
   }

}
