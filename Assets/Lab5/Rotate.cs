using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : OurName
{
    [Header("Модуль")]
    [SerializeField]
    float rotationMaxAngle;
    [ContextMenu("Use")]

    public override void Use()
    {
        StartCoroutine(RotateCoroutine());
    }

    private IEnumerator RotateCoroutine()
    {
        while(transform.localEulerAngles.y < rotationMaxAngle)
        {
            float translation = Time.deltaTime * 10;
            transform.Rotate(Vector3.up, translation, Space.World);
            yield return new WaitForFixedUpdate();
        }
    }
}