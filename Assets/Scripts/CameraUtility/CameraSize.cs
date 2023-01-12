using UnityEngine;

namespace CameraUtility
{
    [RequireComponent(typeof(Camera)), ExecuteInEditMode]
    public class CameraSize : MonoBehaviour
    {
        private Camera _camera;
        [SerializeField] private float _size;
        [Range(0, 1)]
        [SerializeField] private float _match;

        private void Awake()
        {
            _camera ??= GetComponent<Camera>();
        }

        private void Update()
        {
            float aspect = _camera.aspect;
            _camera.orthographicSize = Mathf.Lerp(_size, _size / aspect, _match);
        }
    }
}