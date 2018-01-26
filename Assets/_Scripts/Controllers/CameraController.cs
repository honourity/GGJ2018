using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   [SerializeField] private float _followSpeed = 2f;
   [SerializeField] private float _minZoom;
   [SerializeField] private float _maxZoom;
   [SerializeField] private Transform _target;

   [SerializeField] private BoxCollider2D _levelBounds;
   private Vector3 _min;
   private Vector3 _max;

   void Start()
   {
      _min = _levelBounds.bounds.min;
      _max = _levelBounds.bounds.max;
   }

   void LateUpdate()
   {
      Vector3 newPosition = _target.position;
      newPosition.z = -10;
      transform.position = Vector3.Slerp(transform.position, newPosition, _followSpeed * Time.deltaTime);

      var cameraHalfWidth = Camera.main.orthographicSize * ((float)Screen.width / Screen.height);
      var x = transform.position.x;
      var y = transform.position.y;

      // lock the camera to the right or left bound if we are touching it
      x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);

      // lock the camera to the top or bottom bound if we are touching it
      y = Mathf.Clamp(y, _min.y + Camera.main.orthographicSize, _max.y - Camera.main.orthographicSize);

      transform.position = new Vector3(x, y, transform.position.z);

   }
}
