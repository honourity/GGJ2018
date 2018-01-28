using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   [SerializeField] private float _followSpeed = 2f;
   [SerializeField] private float _minZoom;
   [SerializeField] private float _maxZoom;
   [SerializeField] private Transform _target;
   [SerializeField] private BoxCollider2D _levelBounds;

   [SerializeField]
   private Sprite[] _screenArtifacts;

   private Vector3 _min;
   private Vector3 _max;

   private void Start()
   {
      if (_target == null) _target = GameManager.Instance.Player.transform;

      _min = _levelBounds.bounds.min;
      _max = _levelBounds.bounds.max;

      StartCoroutine(ScreenArtifactGenerator());
   }

   private void LateUpdate()
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

   private IEnumerator ScreenArtifactGenerator()
   {
      //var artifactCount = _screenArtifacts.Length;
      //if (artifactCount > 0)
      //{
      //   while (true)
      //   {
      //      var artifact = _screenArtifacts[Random.Range(0, artifactCount)];

      //      //todo - flicker do an artifact coroutine?
      //      // and wait a a random amount of time to do another random one
      //   }
      //}

      yield return null;
   }
}
