using System;
using UnityEngine;
public class GameTouch
{
    public static bool onTouchBegan()
    {
        if (Input.GetKeyDown(KeyCode.KeyCode.Mouse0))
        {
            return true;
        }
        return false;
    }
}
