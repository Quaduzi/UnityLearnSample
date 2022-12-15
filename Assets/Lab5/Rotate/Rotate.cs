using System.Collections;
using UnityEngine;

public class Rotate : OurName
{
    [Header("Модуль")]
    [SerializeField]
    private Vector3 rotateAngle;

    [SerializeField]
    private float speed;

    private Transform myTransform;

    private void Start()
    {
        myTransform = transform;
    }

    [ContextMenu("Use")]
    public override void Use()
    {
        StartCoroutine(RotateCoroutine(rotateAngle));
    }

    private IEnumerator RotateCoroutine(Vector3 target)
    {
        Quaternion startRotation = myTransform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(target);
        while (myTransform.rotation != endRotation)
        {
            myTransform.rotation = Quaternion.RotateTowards(myTransform.rotation, endRotation, speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        myTransform.rotation = endRotation;
    }
}