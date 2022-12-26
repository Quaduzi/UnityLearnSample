using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class InteractiveBox : ChainableObject
{
    private LineRenderer _laser;
    
    [SerializeField]
    private float damageMultiplier = 1;

    private void Start()
    {
        _laser = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        ShootLaser();
    }

    private void ShootLaser()
    {
        if (next != null)
        {
            if (!_laser.enabled) _laser.enabled = true;
            var origin = transform.position;
            var direction = next.transform.position - origin;
            _laser.SetPosition(0, origin);
            if (Physics.Raycast(origin, direction, out var hit))
            {
                _laser.SetPosition(1, hit.point);
                if (hit.transform.TryGetComponent<DamageableObject>(out var damageable))
                {
                    damageable.GetDamage(Time.deltaTime * damageMultiplier);
                }
            }
        }
        else if (_laser.enabled) _laser.enabled = false;
    }

    private void OnDrawGizmos()
    {
        if (next != null)
        {
            Debug.DrawLine(transform.position, next.transform.position, Color.red);
        }
    }
}
