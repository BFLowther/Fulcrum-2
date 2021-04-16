using UnityEngine;

public class DontDestroyUI : MonoBehaviour
{
    public static DontDestroyUI instance;
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
