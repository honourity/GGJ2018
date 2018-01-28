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

   private void OnDestroy()
   {
      _instance = null;
   }

   public void LoadScene(string scene)
   {
      _playerController = null;
      _camera = null;

      SceneManager.LoadScene(scene);
   }

   //private void Awake()
   //{
   //   DontDestroyOnLoad(gameObject);
   //}
}
