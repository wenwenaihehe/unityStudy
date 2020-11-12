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
    public bool isFileExit(string strAddress)
    {
        return System.IO.File.Exists(strAddress);
    }
    public GameObject LoadPrefab(string strAddress)
    {
        if (isFileExit(strAddress) == false)
        {
            return null;
        }
        GameObject o = Resources.Load(strAddress) as GameObject;
        return o;
    }
    public void LoadImage(GameObject T, string address)
    {
        if (T == null)
        {
            return;
        }
        Sprite Tb3 = (Sprite)Resources.Load(address, typeof(Sprite)) as Sprite;
        T.GetComponent<Image>().sprite = Tb3;
    }
    GameObject seekObjectByName(GameObject T, string name)
    {
        if (T == null)
        {
            return null;
        }
        if (T.transform.name == name)
        {
            return T;
        }
        for (int i = 0; i < T.transform.childCount; i++)
        {
            GameObject Ct = T.transform.GetChild(i).gameObject;
            GameObject curT = seekObjectByName(Ct, name);
            if (curT)
            {
                return curT;
            }
        }
        return null;
    }
}
