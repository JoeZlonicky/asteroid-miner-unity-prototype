using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace UI
{
    public class NotificationDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject notificationTextPrefab;
        [SerializeField] private float displayTime = 2.0f;

        private void Start()
        {
            GameManager.Instance.PlayerInventory.OnItemAdded += OnItemAddedToInventory;
        }

        private void OnDestroy()
        {
            GameManager.Instance.PlayerInventory.OnItemAdded -= OnItemAddedToInventory;
        }
        
        private void OnItemAddedToInventory(ItemData itemData, int n, bool isNew)
        {
            if (itemData.customNotificationMessage.Length > 0)
            {
                AddNotification(itemData.customNotificationMessage);
                return;
            }
            
            AddNotification($"+{n} {itemData.displayName}");
        }

        private void AddNotification(string text)
        {
            var notification = Instantiate(notificationTextPrefab, transform, false);
            notification.GetComponent<TMP_Text>().text = text;
            Destroy(notification.gameObject, displayTime);
        }
    }
}
