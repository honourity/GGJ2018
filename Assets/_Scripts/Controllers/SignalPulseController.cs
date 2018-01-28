using UnityEngine;

public class SignalPulseController : MonoBehaviour {
   public Animator Animator { get; private set; }

   private SpriteRenderer _sprite;

   public void Fizzle()
   {
      Delete();
   }

   public void Delete()
   {
      _sprite.enabled = false;
      Destroy(gameObject);
   }

   private void Awake()
   {
      Animator = GetComponent<Animator>();
      _sprite = GetComponent<SpriteRenderer>();
   }
}
