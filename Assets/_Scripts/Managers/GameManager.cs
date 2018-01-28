using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
   private static GameManager _instance;
   public static GameManager Instance { get { return _instance = _instance ?? FindObjectOfType<GameManager>(); } }

   private PlayerController _playerController;
   public PlayerController Player { get { return _playerController = _playerController ?? FindObjectOfType<PlayerController>(); } }

   private CameraController _camera;
   public CameraController Camera { get { return _camera = _camera ?? FindObjectOfType<CameraController>(); } }

   public void LoadScene(string scene)
   {
      _playerController = null;
      _camera = null;

      SceneManager.LoadScene(scene);
   }

   public void ResetGame()
   {
      Player.ResetThing();
      var towers = FindObjectsOfType<RadioTowerController>();

      foreach (var tower in towers)
      {
         tower.ResetThing();
      }

      var signals = FindObjectsOfType<SignalController>();
      foreach (var signal in signals)
      {
         Destroy(signal.gameObject);
      }

      var missiles = FindObjectsOfType<MissileController>();
      foreach (var missile in missiles)
      {
         Destroy(missile.gameObject);
      }

      ScoreManager.Instance.Score = 0;
   }
      

   //private void Awake()
   //{
   //   DontDestroyOnLoad(gameObject);
   //}
}
