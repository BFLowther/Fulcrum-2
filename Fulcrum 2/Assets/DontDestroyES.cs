using UnityEngine;

public class DontDestroyES : MonoBehaviour
{
    public static DontDestroyES instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
