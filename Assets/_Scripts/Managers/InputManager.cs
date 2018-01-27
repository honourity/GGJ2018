using UnityEngine;

[DisallowMultipleComponent]
public class InputManager : MonoBehaviour
{
   private static InputManager _instance;
   public static InputManager Instance { get { return _instance = _instance ?? FindObjectOfType<InputManager>(); } }

   #region Debug Input
   [SerializeField] RadioTowerController _tower = null;
   #endregion

   public bool InputLocked { get; set; }

   private void Update()
   {
      if (!InputLocked)
      {
         //capturing all input
         var w = Input.GetKey(KeyCode.W);
         var a = Input.GetKey(KeyCode.A);
         var s = Input.GetKey(KeyCode.S);
         var d = Input.GetKey(KeyCode.D);

         //movement
         if (w && d)
         {
            GameManager.Instance.Player.Move(Enums.Directions.UpRight);
         }
         else if (s && d)
         {
            GameManager.Instance.Player.Move(Enums.Directions.DownRight);
         }
         else if (s && a)
         {
            GameManager.Instance.Player.Move(Enums.Directions.DownLeft);
         }
         else if (a && w)
         {
            GameManager.Instance.Player.Move(Enums.Directions.UpLeft);
         }
         else if (w)
         {
            GameManager.Instance.Player.Move(Enums.Directions.Up);
         }
         else if (d)
         {
            GameManager.Instance.Player.Move(Enums.Directions.Right);
         }
         else if (s)
         {
            GameManager.Instance.Player.Move(Enums.Directions.Down);
         }
         else if (a)
         {
            GameManager.Instance.Player.Move(Enums.Directions.Left);
         }
         else
         {
            GameManager.Instance.Player.StopMoving();
         }

         //actions
         if (Input.GetKeyDown(KeyCode.Space)) GameManager.Instance.Player.Ultimate();
         if (Input.GetKeyDown(KeyCode.F)) GameManager.Instance.Player.Attack();
      }
      else
      {
         GameManager.Instance.Player.StopMoving();
      }

      #region Debug Input

      if (Input.GetKeyDown(KeyCode.M))
      {
         //force a missile launch
         var silo = FindObjectOfType<MissileSiloController>();
         silo.ProcessMessage();
      }

      if (Input.GetKeyDown(KeyCode.T))
      {
         _tower.ProcessMessage();
      }

      if (Input.GetKeyDown(KeyCode.K))
      {
         GameManager.Instance.Player.TakeDamage(1);
      }

      #endregion  
   }
}
