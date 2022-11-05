using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : OurName
{
    [Header("Модуль")]

    [SerializeField]
    private Vector3 endPosition;
    [SerializeField]
    private float speed;
    public override void Use()
    {
        StartCoroutine(Coroutine());
    }
    private IEnumerator Coroutine()
    {
        while (transform.position != endPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed);
            yield return null;
        }
    }
}
