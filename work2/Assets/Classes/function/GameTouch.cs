using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.iOS;

public class GameTouch
{
    static public bool onTouchBegan()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            return true;
        }
        return false;
    }
    static public bool onTouchMoving()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            return true;
        }
        return false;
    }
    static public bool onTouchEnd()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            return true;
        }
        return false;
    }
}
