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
      SceneManager.LoadScene(scene);
   }

   public void Reset()
   {
      _playerController = null;
      _camera = null;
   }
}
