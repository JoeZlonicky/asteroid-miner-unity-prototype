namespace UI
{
    public class PlayerInventoryPanel : InventoryPanel
    {
        private void Start()
        {
            ConnectInventory(GameManager.Instance.PlayerInventory);
        }

        private void OnDestroy()
        {
            DisconnectInventory();
        }
    }
}