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
      //capturing all input
      var w = Input.GetKey(KeyCode.W);
      var a = Input.GetKey(KeyCode.A);
      var s = Input.GetKey(KeyCode.S);
      var d = Input.GetKey(KeyCode.D);

      //movement
      if (w && d)
      {
         _playerController.Move(Enums.Directions.UpRight);
      }
      else if (s && d)
      {
         _playerController.Move(Enums.Directions.DownRight);
      }
      else if (s && a)
      {
         _playerController.Move(Enums.Directions.DownLeft);
      }
      else if (a && w)
      {
         _playerController.Move(Enums.Directions.UpLeft);
      }
      else if (w)
      {
         _playerController.Move(Enums.Directions.Up);
      }
      else if (d)
      {
         _playerController.Move(Enums.Directions.Right);
      }
      else if (s)
      {
         _playerController.Move(Enums.Directions.Down);
      }
      else if (a)
      {
         _playerController.Move(Enums.Directions.Left);
      }

      //actions
      if (Input.GetKeyDown(KeyCode.F)) _playerController.Attack();
      if (Input.GetKeyDown(KeyCode.Space)) _playerController.Pounce();
   }
}
