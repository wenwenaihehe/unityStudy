using System;
using UnityEngine;
using UnityEngine.UI;
public class Function
{
    public Function()
    {
    }
    static private Function m_pInstance = null;
    static public Function getInstance()
    {
        if (m_pInstance == null)
        {
            m_pInstance = new Function();
        }
        return m_pInstance;
    }

    public void LoadImage(GameObject T, string address)
    {
        Sprite Tb3 = (Sprite)Resources.Load(address, typeof(Sprite)) as Sprite;
        T.GetComponent<Image>().sprite = Tb3;
    }

}
