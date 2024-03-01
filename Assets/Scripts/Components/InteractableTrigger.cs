using UnityEngine;

namespace Components
{
    public delegate void InteractAction();
    
    public class InteractableTrigger : MonoBehaviour
    {
        public event InteractAction OnInteract;
        
        [SerializeField] private GameObject toggleActiveWhenWithinRange;
        
        private int _insideCount;

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
                OnInteract?.Invoke();
            }
        }
    }
}