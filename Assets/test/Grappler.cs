using Unity.VisualScripting;
using UnityEngine;

namespace test
{
    public class Grappler : MonoBehaviour
    {
        public float range;
        public float hookStrengthMultiplier;
        
        private Vector3 _targetPos;
        private bool _hanging;
        private Rigidbody _rb;
        private Transform _transform;
        private Camera _camera;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        { 
            _targetPos = new Vector3();
            _hanging = false;
            _rb = gameObject.GetComponent<Rigidbody>();
            _transform = gameObject.transform;
            _camera = gameObject.GetComponent<Camera>();
            
            var t = _hanging ? "Hanging in there!" : "Falling :(";
            Debug.Log(t);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_hanging)
            {
                var targetVector = (_targetPos - _transform.position).normalized * hookStrengthMultiplier;
                _rb.AddForce(targetVector, ForceMode.Force);
                if (Input.GetKey(KeyCode.Mouse1) || Vector3.Distance(_transform.position, _targetPos) > range)
                {
                    CutHook();
                }
            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                Debug.Log("Throwing hook");
                if (Physics.Raycast(_transform.position, _camera.transform.forward, out var hit))
                {
                    ThrowHook(hit.transform);
                }
                else Debug.Log("Hook missed :(");
            }
        }

        private void ThrowHook(Transform targetTransform)
        {
            Debug.Log("Hooked to " + targetTransform.position);
            _targetPos = targetTransform.position;
            _hanging = true;
        }

        private void CutHook()
        {
            Debug.Log("Cut hook");
            _hanging = false;
        }
    }
}
