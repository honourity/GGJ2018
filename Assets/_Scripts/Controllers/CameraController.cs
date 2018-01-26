using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   [SerializeField] private float _followSpeed = 2f;
   [SerializeField] private Transform _target;

   // Use this for initialization
   void Start()
   {

   }

   void LateUpdate()
   {
      Vector3 newPosition = _target.position;
      newPosition.z = -10;
      transform.position = Vector3.Slerp(transform.position, newPosition, _followSpeed * Time.deltaTime);
   }
}
