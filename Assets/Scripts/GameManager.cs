using Components;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public Inventory PlayerInventory { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        PlayerInventory = GetComponent<Inventory>();
    }
}
