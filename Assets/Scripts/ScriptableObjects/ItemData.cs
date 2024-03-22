using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName="Item", menuName="ScriptableObjects/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string displayName;
        public Texture2D icon;
        public GameObject pickupPrefab;
    }
}
