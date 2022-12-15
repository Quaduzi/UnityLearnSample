using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Duplicate : OurName
{
    [SerializeField] 
    private bool debug;
    
    [SerializeField]
    private GameObject prefab;

    [SerializeField] 
    private int repeatCount = 1;
    
    [SerializeField]
    private float step = 1;

    [SerializeField] 
    private Vector3 duplicateDirection;

    [SerializeField] 
    private Vector3 duplicateOffset;
    
    [SerializeField] 
    private Vector3 duplicatedObjectRotation;

    [SerializeField]
    [ReadOnly]
    private List<GameObject> duplicatedObjects;

    private Transform myTransform;

    private void Start()
    {
        myTransform = transform;
    }

    [ContextMenu("Сгенерировать объекты")]
    public override void Use()
    {
        for (int i = 0; i < repeatCount; i++)
        {
            var startPosition = myTransform.position + duplicateOffset;
            var position = startPosition + myTransform.rotation * Quaternion.Euler(duplicateDirection) * Vector3.forward * step * i;
            var generated = Instantiate(prefab, position, myTransform.rotation * Quaternion.Euler(duplicatedObjectRotation));
            generated.transform.SetParent(myTransform);
            duplicatedObjects.Add(generated);
        }
    }

    [ContextMenu("Удалить сгенерированные объекты")]
    public void Undo()
    {
        foreach (var duplicatedObject in duplicatedObjects)
        {
            if (duplicatedObject != null)
            {
                if (Application.isEditor)
                    DestroyImmediate(duplicatedObject);
                else
                    Destroy(duplicatedObject);
            } 
                
        }
        duplicatedObjects.Clear();
    }

    private void OnDrawGizmos()
    {
        if (debug) DrawDebugLines();
    }

    private void DrawDebugLines()
    {
        var start = transform.position + duplicateOffset;
        var end = start + transform.rotation * Quaternion.Euler(duplicateDirection) * Vector3.forward * step;
        var objectRotation = start + transform.rotation * Quaternion.Euler(duplicatedObjectRotation) * Vector3.forward * 0.3f;
        Gizmos.DrawSphere(start, 0.1f);
        Gizmos.DrawLine(start, objectRotation);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(start, end);
    }
}
