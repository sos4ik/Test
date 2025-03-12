using UnityEngine;

public class UnitMovemenr : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _stoppingDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    private Vector3 _targetPosition;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _targetPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                _targetPosition = hit.point;
                _targetPosition.y = transform.position.y; 
            }
        }

        MoveUnit();
        RotateUnit();
    }

    private void MoveUnit()
    {
        float distanceToTarget = Vector3.Distance(transform.position, _targetPosition);

        if (distanceToTarget <= _stoppingDistance)
        {
            _rigidbody.velocity = Vector3.zero; 
            return;
        }

        Vector3 direction = (_targetPosition - transform.position).normalized;
        _rigidbody.velocity = direction * _moveSpeed;
    }

    private void RotateUnit()
    {
        Vector3 direction = _targetPosition - transform.position;
        direction.y = 0;

        if (direction.magnitude > 0.1f)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
        }
    }
}
