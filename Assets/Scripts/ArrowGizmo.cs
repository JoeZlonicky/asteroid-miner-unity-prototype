using UnityEditor;
using UnityEngine;

public class ArrowGizmo : MonoBehaviour
{
    [SerializeField] [Min(0f)] private float arrowLength = 1f;

    private void OnDrawGizmos()
    {
        if (Application.isPlaying) return;

        Handles.color = Color.magenta;
        Transform t = gameObject.transform;
        Handles.ArrowHandleCap(0, t.position, t.rotation * Quaternion.LookRotation(Vector3.right), arrowLength, EventType.Repaint);
    }
}
