using UnityEngine;

public class Session : MonoBehaviour
{
    private static Session _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Session GetInstance()
    {
        return _instance;
    }
}
