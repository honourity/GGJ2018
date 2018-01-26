using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class InterfaceManager : MonoBehaviour
{
   private static InterfaceManager _instance;
   public static InterfaceManager Instance { get { return _instance = _instance ?? FindObjectOfType<InterfaceManager>(); } }

   [SerializeField] Image _playerHealth;

   private void OnEnable()
   {
      EventManager.AddListener("PlayerTakeDamage", OnTakeDamage);
   }

   private void OnDisable()
   {
      EventManager.RemoveListener("PlayerTakeDamage", OnTakeDamage);
   }

   private void Start()
   {
      UpdateHealthBar();
   }

   private void OnTakeDamage()
   {
      UpdateHealthBar();
   }

   private void UpdateHealthBar()
   {
      _playerHealth.fillAmount = GameManager.Instance.Player.Health / GameManager.Instance.Player.MaxHealth;
   }

}
