using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class BaseSeries : MonoBehaviour
{
    public BaseSeries m_pOutSeries;
    public abstract int GetValue();
    virtual public void showValue()
    {
        Transform oValue = gameObject.transform.Find("Value");
        if (oValue != null)
        {
            Text T = oValue.gameObject.GetComponent<Text>();
            T.text = getShowValue(GetValue());//.ToString();
        }
    }
    virtual public string getShowValue(int T)
    {
        string str = Function.getInstance().getIntSystem(T, 3);
        string strTarget = "";
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == '2')
            {
                strTarget += "+";
            }
            if (str[i] == '1')
            {
                strTarget += "*";
            }
            if (str[i] == '0')
            {
                strTarget += ".";
            }
        }

        return strTarget;
    }
}

abstract public class BaseMathSeries : BaseSeries
{
    public BaseSeries m_bOneInSeries;
    public BaseSeries m_bTwoInSeries;
}