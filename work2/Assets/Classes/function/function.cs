using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
public class Function
{

    //doTewwn
    /*
              Vector3 endpos = transform.localPosition + new Vector3(0, 200);
        
        Tween Act = this.transform.DOLocalMove(endpos, 3.0f);
        Act.SetEase(Ease.Linear);
     */
    //
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
    //切换场景
    public void repleaceScene(string address)
    {
        SceneManager.LoadScene(address);
    }
    //获取当前运行的场景
    public Scene getCurScene()
    {
        return SceneManager.GetActiveScene();
    }
    public bool isFileExit(string strAddress)
    {
        return File.Exists(strAddress);
    }
    public GameObject LoadPrefab(string strAddress)
    {
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
    //获取游戏每一秒运行的帧数
    public float getDeltaTime()
    {
        return Time.deltaTime;
    }

    //将传入的字符转换为相应的进制
    public string getIntSystem(int Value, int System)
    {
        string strTarget = "";
        while (Value != 0)
        {
            int x = Value % System;
            strTarget = x.ToString() + strTarget;
            Value = Value / System;
        }

        return strTarget;
    }

    //将传入的字符转换为十进制 需指定当前字符是什么进制
    public int getIntSysTemToTen(string Value, int System)
    {
        return 1;
        //return Convert.ToInt32(Value, System);
    }
}
