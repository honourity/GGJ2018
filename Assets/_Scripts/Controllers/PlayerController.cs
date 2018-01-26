using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] float _speed;

   private void Update()
   {
      float h = Input.GetAxis("Horizontal");
      float v = Input.GetAxis("Vertical");

      transform.Translate(Vector3.right * h * _speed * Time.deltaTime);
      transform.Translate(Vector3.up * v * _speed * Time.deltaTime);

   }

   public void Jump()
   {
      throw new NotImplementedException();
   }
}
