using ScriptableObjects;
using UnityEngine;

namespace Misc
{
    public class TestUtility : MonoBehaviour
    {
        [SerializeField] private ItemData[] addItemsToInventory;

        private void Start()
        {
            foreach (var itemData in addItemsToInventory)
            {
                GameManager.Instance.PlayerInventory.AddItem(itemData, 99);
            }
        }
    }
}