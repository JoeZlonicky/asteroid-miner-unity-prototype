using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField] private RawImage image;
        [SerializeField] private TMP_Text countLabel;

        private bool _isFilled;
        
        public void SetItemCount(ItemData itemData, int n)
        {
            Debug.Assert(n >= 1);
            image.texture = itemData.icon;
            countLabel.text = $"{n}";
            _isFilled = true;
            image.gameObject.SetActive(true);
        }

        public void Clear()
        {
            _isFilled = false;
            image.texture = null;
            countLabel.text = "";
            image.gameObject.SetActive(false);
        }

        public bool IsFilled()
        {
            return _isFilled;
        }
    }
}
