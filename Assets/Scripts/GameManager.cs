using Components;
using UnityEngine;

public delegate void NotificationAction(string text);

[RequireComponent(typeof(Inventory))]
public class GameManager : MonoBehaviour
{
    public event NotificationAction OnNotification;
    
    public static GameManager Instance { get; private set; }
    
    public Inventory PlayerInventory { get; private set; }
    public int ShipTier { get; private set; }

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

    public void TriggerNotification(string text)
    {
        OnNotification?.Invoke(text);
    }

    public void UpgradeShipTier()
    {
        ShipTier += 1;
    }
}
