using Unity.Netcode;
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
        private LineScript _lineScript;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        { 
            _targetPos = new Vector3();
            _hanging = false;
            _rb = gameObject.GetComponent<Rigidbody>();
            _transform = gameObject.transform;
            _camera = gameObject.GetComponent<Camera>();
            _lineScript = gameObject.GetComponent<LineScript>();
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
                    CutHookRpc();
                }
            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Physics.Raycast(_transform.position, _camera.transform.forward, out var hit))
                {
                    ThrowHookRpc(hit.transform);
                }
            }
        }

        [Rpc] private void ThrowHookRpc(Transform targetTransform)
        {
            _targetPos = targetTransform.position;
            _lineScript.AttachHook(targetTransform.position);
            _hanging = true;
        }

        [Rpc] private void CutHookRpc()
        {
            _lineScript.DetachHook();
            _hanging = false;
        }
    }
}
