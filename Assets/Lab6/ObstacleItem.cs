using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleItem : DamageableObject
{
    [SerializeField]
    private UnityEvent onDestroyObstacle;
    [SerializeField] 
    private Color minHealthColor;
    [SerializeField] 
    private Color maxHealthColor;
    [SerializeField] 
    private float destroySpeed = 1;
    [SerializeField] 
    private float recoverCooldown = 3;

    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        CalcColor();
        StartCoroutine(Recover());
    }
    private IEnumerator Recover()
    {
        var baseValue = currentValue;
        var oldValue = currentValue;
        while (currentValue > 0)
        {
            if (oldValue > currentValue)
            {
                yield return new WaitForSeconds(recoverCooldown);
            }

            if (currentValue < baseValue)
            {
                currentValue += Time.deltaTime;
                if (currentValue > baseValue) currentValue = baseValue;
            }

            oldValue = currentValue;
            CalcColor();
            yield return null;
        }
    }
    public override void GetDamage(float value)
    {
        base.GetDamage(Time.deltaTime * destroySpeed);
        CalcColor();
        if (currentValue <= 0)
        {
            onDestroyObstacle.Invoke();
            Destroy(gameObject);
        }
    }

    private void CalcColor()
    {
        _renderer.material.color = Color.Lerp(minHealthColor, maxHealthColor, currentValue);
    }
}
