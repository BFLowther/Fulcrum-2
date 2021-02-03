﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperUtilities {

    public static Vector3 CloneVector3(Vector3 origVector3)
    {
        return new Vector3(origVector3.x, origVector3.y, origVector3.z);
    }

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        bool over180 = false;
        if (angle > 180)
        {
            angle = angle - 360;
            over180 = true;
        }
        angle = Mathf.Clamp(angle, min, max);
        if (over180)
        {
            angle = 360 + angle;
        }

        return angle;
    }

    public static void UpdateCursorLock(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public static string GetFormattedValue(int val)
    {
        if (val >= 0 && val < 10)
        {
            return "0" + val;
        }

        return val.ToString();
    }
}
