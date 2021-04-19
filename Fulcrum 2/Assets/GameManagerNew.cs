using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerNew : MonoBehaviour
{
    public static GameManagerNew SharedInstance;
    private bool isMale = true;

    private void Awake()
    {
        if (SharedInstance == null)
            SharedInstance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeGender()
    {
        isMale = isMale ? false : true;
    }

    public bool IsMale()
    {
        return isMale;
    }
}
