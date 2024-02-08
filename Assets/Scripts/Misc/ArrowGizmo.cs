using UnityEngine;

namespace Misc
{
    public class ArrowGizmo : MonoBehaviour
    {
        #if UNITY_EDITOR
        [SerializeField] [Min(0f)] private float arrowLength = 1f;
        
        private void OnDrawGizmos()
        {
            if (Application.isPlaying) return;

            UnityEditor.Handles.color = Color.magenta;
            Transform t = gameObject.transform;
            UnityEditor.Handles.ArrowHandleCap(0, t.position, t.rotation * Quaternion.LookRotation(Vector3.right), 
                arrowLength, EventType.Repaint);
        }
        #endif
    }
}
