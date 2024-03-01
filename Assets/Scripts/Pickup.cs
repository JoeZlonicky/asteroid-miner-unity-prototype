using ScriptableObjects;
using UnityEngine;

public delegate void CollectAction();

[RequireComponent(typeof(Rigidbody2D))]
public class Pickup : MonoBehaviour
{
    public event CollectAction OnCollect;
    
    [field: SerializeField] public ItemData Data { get; private set; }

    public void Collect()
    {
        OnCollect?.Invoke();
        Destroy(gameObject);
    }
}
