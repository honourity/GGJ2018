using UnityEngine;

public class MissileSiloController : MonoBehaviour, IMessageReceiver
{
   [SerializeField]
   private GameObject _missilePrefab;
   [SerializeField]
   private Transform _signalTarget;
   [SerializeField]
   private Transform _missileSpawnPoint;
   [SerializeField]
   private AudioClip _launchSound;

   private Animator _animator;
   private AudioSource _audioSource;

   public Transform GetSignalTarget()
   {
      return _signalTarget;
   }

   public void ProcessMessage()
   {
      _animator.SetTrigger("Launch");
   }

   private void Awake()
   {
      _animator = GetComponent<Animator>();
      _audioSource = GetComponent<AudioSource>();
   }

   public void CreateMissile()
   {
      Instantiate(_missilePrefab, _missileSpawnPoint.position, Quaternion.identity, null);
      _audioSource.PlayOneShot(_launchSound);
   }
}
