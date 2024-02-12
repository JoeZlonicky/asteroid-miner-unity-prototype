using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[Serializable]
public class InteractEvent : UnityEvent {}

[RequireComponent(typeof(Rigidbody2D))]
public class Interactable : MonoBehaviour
{
    public GameObject toggleActiveWhenWithinRange;
    public InteractEvent onInteract;

    private int _insideCount = 0;

    private void Awake()
    {
        if (toggleActiveWhenWithinRange != null)
        {
            toggleActiveWhenWithinRange.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _insideCount += 1;
        if (_insideCount == 1 && toggleActiveWhenWithinRange != null)
        {
            toggleActiveWhenWithinRange.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _insideCount -= 1;
        Debug.Assert(_insideCount >= 0);

        if (_insideCount == 0 && toggleActiveWhenWithinRange != null)
        {
            toggleActiveWhenWithinRange.SetActive(false);
        }
    }

    public void CheckForInteraction()
    {
        if (_insideCount > 0)
        {
            onInteract?.Invoke();
        }
    }
}
