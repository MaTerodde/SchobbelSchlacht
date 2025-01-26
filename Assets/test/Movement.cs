using UnityEngine;

namespace test
{
    public class Movement : MonoBehaviour
    {
        public GrabblerSO grabblerSo;

        private float _acceleration;
        private float _sensitivity;
        private float _maxYAngle ;
        private float _walkingSpeed;
        private Rigidbody _rb;
        private Vector2 _currentRotation;
        private Camera _camera;
        private Transform _transform;
        private float _lastY;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _acceleration = grabblerSo.acceleration;
            _sensitivity = grabblerSo.sensitivity;
            _maxYAngle = grabblerSo.maxYAngle;
            _walkingSpeed = grabblerSo.walkingSpeed;
        
            _rb = gameObject.GetComponent<Rigidbody>();
            _currentRotation = new Vector2();
            _camera = gameObject.GetComponent<Camera>();
            _transform = gameObject.transform;
            _lastY = _transform.position.y;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            LoadFlyingInputs();
            /*
            if (_lastY - _transform.position.y >= 0.01f)
            {
                LoadFlyingInputs();
            }
            else
            {
                LoadWalkingInputs();
            }
            
            _lastY = _transform.position.y;
            */
        }

        private void LoadFlyingInputs()
        {
            if (Input.GetKey(KeyCode.W))
            {
                _rb.AddForce(_camera.transform.TransformDirection(Vector3.forward) * _acceleration, ForceMode.Force);
            }

            if (Input.GetKey(KeyCode.S))
            {
                _rb.AddForce(_camera.transform.TransformDirection(Vector3.back) * _acceleration, ForceMode.Force);
            }

            if (Input.GetKey(KeyCode.A))
            {
                _rb.AddForce(_camera.transform.TransformDirection(Vector3.left) * _acceleration, ForceMode.Force);
            }

            if (Input.GetKey(KeyCode.D))
            {
                _rb.AddForce(_camera.transform.TransformDirection(Vector3.right) * _acceleration, ForceMode.Force);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                _rb.AddForce(_camera.transform.TransformDirection(Vector3.up) * _acceleration, ForceMode.Force);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _rb.AddForce(_camera.transform.TransformDirection(Vector3.down) * _acceleration, ForceMode.Force);
            }

            _currentRotation.x += Input.GetAxis("Mouse X") * _sensitivity;
            _currentRotation.y -= Input.GetAxis("Mouse Y") * _sensitivity;
            _currentRotation.x = Mathf.Repeat(_currentRotation.x, 360);
            _currentRotation.y = Mathf.Clamp(_currentRotation.y, -_maxYAngle, _maxYAngle);
            _camera.transform.rotation = Quaternion.Euler(_currentRotation.y, _currentRotation.x, 0);
        }

        private void LoadWalkingInputs()
        {
            if (Input.GetKey(KeyCode.W))
            {
                _transform.position += new Vector3((_currentRotation * _walkingSpeed).x, 0, (_currentRotation * _walkingSpeed).y);
            }

            if (Input.GetKey(KeyCode.S))
            {
                _rb.AddForce(_camera.transform.TransformDirection(Vector3.back) * _acceleration, ForceMode.Force);
            }

            if (Input.GetKey(KeyCode.A))
            {
                _rb.AddForce(_camera.transform.TransformDirection(Vector3.left) * _acceleration, ForceMode.Force);
            }

            if (Input.GetKey(KeyCode.D))
            {
                _rb.AddForce(_camera.transform.TransformDirection(Vector3.right) * _acceleration, ForceMode.Force);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                _rb.AddForce(_camera.transform.TransformDirection(Vector3.up) * _acceleration, ForceMode.Force);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _rb.AddForce(_camera.transform.TransformDirection(Vector3.down) * _acceleration, ForceMode.Force);
            }

            _currentRotation.x += Input.GetAxis("Mouse X") * _sensitivity;
            _currentRotation.y -= Input.GetAxis("Mouse Y") * _sensitivity;
            _currentRotation.x = Mathf.Repeat(_currentRotation.x, 360);
            _currentRotation.y = Mathf.Clamp(_currentRotation.y, -_maxYAngle, _maxYAngle);
            _camera.transform.rotation = Quaternion.Euler(_currentRotation.y, _currentRotation.x, 0);
        }
    }
}