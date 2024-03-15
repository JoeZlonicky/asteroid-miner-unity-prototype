using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace UI
{
    public class NotificationDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject notificationTextPrefab;

        private void Start()
        {
            GameManager.Instance.OnNotification += OnGameNotification;
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnNotification -= OnGameNotification;
        }
        
        private void OnGameNotification(string text)
        {
            var notification = Instantiate(notificationTextPrefab, transform, false);
            notification.GetComponent<TMP_Text>().text = text;
        }
    }
}
