using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerNew : MonoBehaviour
{
    public static GameManagerNew SharedInstance;
    private bool isMale = true;

    private void Awake()
    {
        SharedInstance = this;
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
