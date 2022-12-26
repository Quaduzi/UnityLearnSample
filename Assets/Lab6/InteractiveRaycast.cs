using UnityEngine;

[RequireComponent(typeof(Camera))]
public class InteractiveRaycast : MonoBehaviour
{
    private Camera _camera;
    private ChainableObject _selectedChainableObject;

    [SerializeField]
    private GameObject prefab;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        CheckLeftMouseButtonUp();
        CheckRightMouseButtonUp();
    }

    private void CheckRightMouseButtonUp()
    {
        if (!Input.GetMouseButtonUp(1)) return;
        
        if (!GetMouseHit(out var hit)) return;

        if (hit.transform.TryGetComponent<ChainableObject>(out var del))
        {
            Destroy(del.gameObject);
        }
    }

    private bool GetMouseHit(out RaycastHit hit)
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray.origin, ray.direction, out hit);
    }

    private void CheckLeftMouseButtonUp()
    {
        if (!Input.GetMouseButtonUp(0)) return;
        
        if (!GetMouseHit(out var hit)) return;
            
        if (hit.collider.CompareTag("InteractivePlane"))
        {
            InstantiateByRaycast(hit);
        }

        if (hit.transform.TryGetComponent<ChainableObject>(out var chainable))
        {
            ConnectChainable(chainable);
        }
    }

    private void ConnectChainable(ChainableObject chainable)
    {
        if (_selectedChainableObject == null)
        {
            _selectedChainableObject = chainable;
            return;
        }
        
        if (chainable != _selectedChainableObject) _selectedChainableObject.AddNext(chainable);
        
        _selectedChainableObject = null;
    }

    private void InstantiateByRaycast(RaycastHit hit)
    {
        var halfExtents = prefab.transform.localScale / 2;
        var center = hit.point + hit.normal * halfExtents.y;
        var rotation = hit.transform.rotation;
        if (!Physics.CheckBox(center + hit.normal * 0.000001f, halfExtents, rotation))
        {
            Instantiate(prefab, center, rotation, hit.transform);
        }
    }
}
