using System.Linq;
using Components;
using ScriptableObjects;
using UnityEngine;

public delegate void NotificationAction(string text);
public delegate void ShipTierUpAction(int newTier);
public delegate void WarpGateRepairedAction();

[RequireComponent(typeof(Inventory))]
public class GameManager : MonoBehaviour
{
    public event NotificationAction OnNotification;
    public event ShipTierUpAction OnShipTierUp;
    public event WarpGateRepairedAction OnWarpGateRepaired;
    
    public static GameManager Instance { get; private set; }
    
    public Inventory PlayerInventory { get; private set; }
    public int ShipTier { get; private set; }
    public bool IsWarpGateRepaired { get; private set; }

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
        OnShipTierUp?.Invoke(ShipTier);
    }

    public void SetWarpGateRepaired()
    {
        IsWarpGateRepaired = true;
        OnWarpGateRepaired?.Invoke();
    }
}
