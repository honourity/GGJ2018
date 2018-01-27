using UnityEngine;

public class MissileSiloController : MonoBehaviour, IMessageReceiver
{
   [SerializeField]
   private GameObject _missilePrefab;
   [SerializeField]
   private Transform _signalTarget;
   [SerializeField]
   private Transform _missileSpawnPoint;

   private Animator _animator;

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
   }

   public void CreateMissile()
   {
      Instantiate(_missilePrefab, _missileSpawnPoint.position, Quaternion.identity, null);
   }
}
