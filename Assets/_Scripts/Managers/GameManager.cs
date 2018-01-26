using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
   private static GameManager _instance;
   public static GameManager Instance { get { return _instance = _instance ?? FindObjectOfType<GameManager>(); } }

   private PlayerController _playerController;
   public PlayerController PlayerController { get { return _playerController = _playerController ?? FindObjectOfType<PlayerController>(); } }
}
