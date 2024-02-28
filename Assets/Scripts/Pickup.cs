using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Pickup : MonoBehaviour
{
    
    public delegate void CollectAction();
    public event CollectAction OnCollect;
    
    [field: SerializeField] public ItemData Data { get; private set; }

    public void Collect()
    {
        OnCollect?.Invoke();
        Destroy(gameObject);
    }
}
