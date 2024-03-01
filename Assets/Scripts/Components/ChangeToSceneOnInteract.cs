using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components
{
    public class ChangeToSceneOnInteract : MonoBehaviour
    {
        [SerializeField] private InteractableTrigger interactableTrigger;
        [SerializeField] private string sceneName;

        private void Awake()
        {
            interactableTrigger.OnInteract += ChangeScene;
        }
        
        public void ChangeScene()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
