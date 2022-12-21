using UnityEngine;

public abstract class ChainableObject : MonoBehaviour, ISelectable, IChainable<ChainableObject>
{
    [SerializeField]
    protected ChainableObject next;
    
    public virtual void AddNext(ChainableObject obj)
    {
        next = obj;
    }
}
