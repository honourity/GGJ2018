using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class InterfaceManager : MonoBehaviour
{
   private static InterfaceManager _instance;
   public static InterfaceManager Instance { get { return _instance = _instance ?? FindObjectOfType<InterfaceManager>(); } }

   [SerializeField] Image _playerHealth;
   [SerializeField] Image _ultimateCharge;
   [SerializeField] Animator _tvAnimator;
   [SerializeField] GameObject _gameOverButton;

   private void OnEnable()
   {
      EventManager.AddListener("PlayerTakeDamage", OnTakeDamage);
      EventManager.AddListener("PlayerCharge", OnPlayerCharge);
      EventManager.AddListener("DAED", OnDAED);
   }

   private void OnDisable()
   {
      EventManager.RemoveListener("PlayerTakeDamage", OnTakeDamage);
      EventManager.RemoveListener("PlayerCharge", OnPlayerCharge);
      EventManager.RemoveListener("DAED", OnDAED);
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

   private void OnDAED()
   {
      StartCoroutine(DelayedDAED());
   }

   private IEnumerator DelayedDAED()
   {
      yield return new WaitForSeconds(2.7f);
      _tvAnimator.Play("TVFrameStatic");
      yield return new WaitForSeconds(2f);
      _gameOverButton.SetActive(true);
      
   }

}
