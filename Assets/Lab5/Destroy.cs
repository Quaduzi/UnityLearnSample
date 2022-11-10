using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : OurName
{
    private Transform target;
    private float speed = 0.1f;
    private bool key;
    private float t;

    private void Start()
    {
        target = transform;
        key = false;
    }
    private void Update()
    {
        if (key)
        {
            if (target.childCount==0)
            {
                key = false;
            }

            for (int i = 0; i < target.childCount; i++)
            {
                Transform child = target.GetChild(i);

                if (child != null)
                {
                    t += speed * Time.deltaTime;
                    child.localScale = Vector3.Lerp(child.localScale, Vector3.zero, t);

                    if (child.localScale == Vector3.zero)
                    {
                        Destroy(child.gameObject);
                    }
                }
            }
        }
    }

    [ContextMenu("Удалить все дочерние объекты")]
    public override void Use()
    {
        key = true;
    }
}
