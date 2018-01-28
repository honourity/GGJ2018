using UnityEngine;

[DisallowMultipleComponent]
public class InputManager : MonoBehaviour
{
   private static InputManager _instance;
   public static InputManager Instance { get { return _instance = _instance ?? FindObjectOfType<InputManager>(); } }

   public bool InputLocked { get; set; }

   private void Update()
   {
      if (GameManager.Instance.Player != null)
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
               GameManager.Instance.Player.Move(Enums.Directions4WayCompressing.UpRight);
            }
            else if (s && d)
            {
               GameManager.Instance.Player.Move(Enums.Directions4WayCompressing.DownRight);
            }
            else if (s && a)
            {
               GameManager.Instance.Player.Move(Enums.Directions4WayCompressing.DownLeft);
            }
            else if (a && w)
            {
               GameManager.Instance.Player.Move(Enums.Directions4WayCompressing.UpLeft);
            }
            else if (w)
            {
               GameManager.Instance.Player.Move(Enums.Directions4WayCompressing.Up);
            }
            else if (d)
            {
               GameManager.Instance.Player.Move(Enums.Directions4WayCompressing.Right);
            }
            else if (s)
            {
               GameManager.Instance.Player.Move(Enums.Directions4WayCompressing.Down);
            }
            else if (a)
            {
               GameManager.Instance.Player.Move(Enums.Directions4WayCompressing.Left);
            }
            else
            {
               GameManager.Instance.Player.StopMoving();
            }

            //actions
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1)) GameManager.Instance.Player.UltimateAnimate();
            if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0)) GameManager.Instance.Player.Attack();
         }
         else
         {
            GameManager.Instance.Player.StopMoving();
         }
      }

      if (Input.GetKeyDown(KeyCode.L))
      {
         GameManager.Instance.ResetGame();
      }
   }
}
