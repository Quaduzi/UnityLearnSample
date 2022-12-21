using UnityEngine;

public abstract class DamageableObject : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected float currentValue = 1;

    public virtual void GetDamage(float value)
    {
        currentValue -= value;
    }
}
