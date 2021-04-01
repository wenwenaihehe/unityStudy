using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject m_test1;
    public GameObject m_test2;
    public GameObject m_test3;
    public GameObject m_test4;
    void Start()
    {
        BaseSeries test1 = m_test1.GetComponent<constValueSeries>();
        BaseSeries test2 = m_test2.GetComponent<constValueSeries>();
        orSeries test3 = m_test3.AddComponent<orSeries>();
        endSeries test4 = m_test4.AddComponent<endSeries>();

        test1.m_pOutSeries = test3;
        test2.m_pOutSeries = test3;

        test3.m_bOneInSeries = test1;
        test3.m_bTwoInSeries = test2;

        test4.m_InSeries = test3;

        test4.showValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
