using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : OurName
{
    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    [Header("Модуль")]

    [SerializeField]
    private Vector3 endPosition;
    [SerializeField]
    private float speed;
    public override void Use()
    {
        StartCoroutine(MoveCoroutine());
    }
    private IEnumerator MoveCoroutine()
    {
        while (transform.position != endPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed/50);
            yield return _waitForFixedUpdate;
        }
    }
}
