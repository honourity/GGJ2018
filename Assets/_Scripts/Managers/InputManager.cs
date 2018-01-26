using UnityEngine;

[DisallowMultipleComponent]
public class InputManager : MonoBehaviour
{
   private static InputManager _instance;
   public static InputManager Instance { get { return _instance = _instance ?? FindObjectOfType<InputManager>(); } }

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
         GameManager.Instance.PlayerController.Move(Enums.Directions.UpRight);
      }
      else if (s && d)
      {
         GameManager.Instance.PlayerController.Move(Enums.Directions.DownRight);
      }
      else if (s && a)
      {
         GameManager.Instance.PlayerController.Move(Enums.Directions.DownLeft);
      }
      else if (a && w)
      {
         GameManager.Instance.PlayerController.Move(Enums.Directions.UpLeft);
      }
      else if (w)
      {
         GameManager.Instance.PlayerController.Move(Enums.Directions.Up);
      }
      else if (d)
      {
         GameManager.Instance.PlayerController.Move(Enums.Directions.Right);
      }
      else if (s)
      {
         GameManager.Instance.PlayerController.Move(Enums.Directions.Down);
      }
      else if (a)
      {
         GameManager.Instance.PlayerController.Move(Enums.Directions.Left);
      }

      //actions
      if (Input.GetKeyDown(KeyCode.Space)) GameManager.Instance.PlayerController.Jump();
      if (Input.GetKeyDown(KeyCode.F)) GameManager.Instance.PlayerController.Attack();
      if (Input.GetKeyDown(KeyCode.R)) GameManager.Instance.PlayerController.Ultimate();


      #region Debug Input
      if (Input.GetKeyDown(KeyCode.M))
      {
         var silo = FindObjectOfType<MissileSiloController>();
         silo.Launch();
      }
      #endregion  
   }
}
