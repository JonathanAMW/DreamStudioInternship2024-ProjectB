using UnityEngine;

public class DestroyOnLoadSingletonMonoBehaviour<T> : MonoBehaviour where T : Component
{
    // create a private reference to T instance
    private static T _instance;

    public static T Instance
    {
        get
        {
            // if instance is null
            if (_instance == null)
            {
                // find the generic instance
                _instance = FindObjectOfType<T>();

                // if it's null again create a new object
                // and attach the generic instance
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        // create the instance
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected void OnDestroy()
    {
        _instance = null;
    }
}