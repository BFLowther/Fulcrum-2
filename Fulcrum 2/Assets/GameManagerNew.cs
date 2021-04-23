using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerNew : MonoBehaviour
{
    public static GameManagerNew SharedInstance;
    private bool isMale = true;
    private int NumOfCollectables = 0;

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

    private void Start()
    {
        NumOfCollectables = 0;
    }

    public void ChangeGender()
    {
        isMale = isMale ? false : true;
    }

    public bool IsMale()
    {
        return isMale;
    }

    public void ResetCollectables()
    {
        NumOfCollectables = 0;
    }

    public int GetCollectables()
    {
        return NumOfCollectables;
    }

    public void IncreaseCollectables()
    {
        NumOfCollectables++;
    }
}
