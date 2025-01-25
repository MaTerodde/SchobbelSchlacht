using UnityEngine;

namespace test
{
    public class Grappler : MonoBehaviour
    {
        public GameObject target;
        public float range;
        public float hookStrengthMultiplier;
        
        private Vector3 _targetPos;
        private bool _hanging;
        private Rigidbody _rb;
        private Transform _transform;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _targetPos = target.transform.position;
            _hanging = false;
            _rb = gameObject.GetComponent<Rigidbody>();
            _transform = gameObject.transform;
            
            ThrowHook();
            var t = _hanging ? "Hanging in there!" : "Falling :(";
            Debug.Log(t);
        }

        // Update is called once per frame
        void Update()
        {
            if (_hanging)
            {
                var targetVector = (_targetPos - _transform.position).normalized * hookStrengthMultiplier;
                _rb.AddForce( targetVector, ForceMode.Force );    
            }
            
        }

        private void ThrowHook()
        {
            var pos = _transform.position;
            RaycastHit hitInfo;
            if (Physics.Raycast(pos, target.transform.position, out hitInfo, range)) return;
            {
                _hanging = true;
            }
        }
    }
}
