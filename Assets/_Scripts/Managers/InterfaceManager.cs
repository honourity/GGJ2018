using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class InterfaceManager : MonoBehaviour
{
   private static InterfaceManager _instance;
   public static InterfaceManager Instance { get { return _instance = _instance ?? FindObjectOfType<InterfaceManager>(); } }

   [SerializeField] Image _playerHealth;
   [SerializeField] Image _ultimateCharge;

   private void OnEnable()
   {
      EventManager.AddListener("PlayerTakeDamage", OnTakeDamage);
      EventManager.AddListener("PlayerCharge", OnPlayerCharge);
   }

   private void OnDisable()
   {
      EventManager.RemoveListener("PlayerTakeDamage", OnTakeDamage);
      EventManager.RemoveListener("PlayerCharge", OnPlayerCharge);
   }

   private void Start()
   {
      UpdateHealthBar();
      OnPlayerCharge();
   }

   private void OnTakeDamage()
   {
      UpdateHealthBar();
   }

   private void OnPlayerCharge()
   {
      _ultimateCharge.fillAmount = GameManager.Instance.Player.UltimateCharge / GameManager.Instance.Player.MaxUltimateCharge;
   }

   private void UpdateHealthBar()
   {
      _playerHealth.fillAmount = GameManager.Instance.Player.Health / GameManager.Instance.Player.MaxHealth;
   }

}
