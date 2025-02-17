using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

namespace test
{
    public class Grappler : MonoBehaviour
    {
        public GrabblerSO grabblerSo;
        
        private float _range;
        private float _hookStrengthMultiplier;
        private Vector3 _targetPos;
        [SerializeField] private bool _hanging;
        private Rigidbody _rb;
        private Transform _transform;
        private Camera _camera;
        private LineScript _lineScript;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _range = grabblerSo.range;
            _hookStrengthMultiplier = grabblerSo.hookStrengthModifier;
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
                var targetVector = (_targetPos - _transform.position).normalized * _hookStrengthMultiplier;
                _rb.AddForce(targetVector, ForceMode.Force);
                if (Input.GetKey(KeyCode.Mouse1) || Vector3.Distance(_transform.position, _targetPos) > _range)
                {
                    CutHookRpc();
                }
            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Physics.Raycast(_transform.position, _camera.transform.forward, out var hit))
                {
                    if(Vector3.Distance(_transform.position, hit.point) < _range)
                        ThrowHookRpc(hit.point);
                }
            }
        }

        private void ThrowHookRpc(Vector3 hitPoint)
        {
            _targetPos = hitPoint;
            _lineScript.AttachHook(hitPoint);
            _hanging = true;
        }

        private void CutHookRpc()
        {
            _lineScript.DetachHook();
            _hanging = false;
        }
    }
}
