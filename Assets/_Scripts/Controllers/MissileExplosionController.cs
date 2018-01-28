using UnityEngine;

public class MissileExplosionController : MonoBehaviour
{
   [SerializeField]
   private AudioClip _explosionSound = null;

   private AudioSource _audioSource;

   private void Awake()
   {
      _audioSource = GetComponent<AudioSource>();
   }

   private void Start()
   {
      _audioSource.PlayOneShot(_explosionSound);
      Destroy(gameObject, 0.5f);
   }
}
