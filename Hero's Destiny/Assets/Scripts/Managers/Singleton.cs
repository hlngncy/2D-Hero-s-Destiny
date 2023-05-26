using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    static T m_instance;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                Debug.LogWarning($"SINGLETON {typeof(T).FullName} SHOULD NOT BE EMPTY!");
                m_instance = GameObject.FindObjectOfType<T>();

                if (m_instance == null)
                {
                    Debug.LogWarning($"SINGLETON {typeof(T).FullName} SHOULD NOT BE CREATE WITH CONSTRUCTOR!");
                    GameObject singleton = new GameObject(typeof(T).Name);
                    m_instance = singleton.AddComponent<T>();
                }
            }
            return m_instance;
        }
    }

    public virtual void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this as T;
            if (TryGetComponent(out RectTransform _)) return;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.LogWarning($"THERE ARE 2 COPY OF {m_instance.GetType().Name}!");
            if (m_instance != this as T)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning($"SINGLETON {m_instance.GetType().Name} CALLING BEFORE ITS OWN AWAKE CALL");
            }
        }
    }
}
