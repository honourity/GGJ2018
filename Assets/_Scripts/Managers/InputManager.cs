using UnityEngine;

[DisallowMultipleComponent]
public class InputManager : MonoBehaviour
{
   private static InputManager _instance;
   public static InputManager Instance { get { return _instance = _instance ?? FindObjectOfType<InputManager>(); } }

   private PlayerController _playerController;

   private void Awake()
   {
      _playerController = FindObjectOfType<PlayerController>();
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space)) _playerController.Jump();
   }
}
