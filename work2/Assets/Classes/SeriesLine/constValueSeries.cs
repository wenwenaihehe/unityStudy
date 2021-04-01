using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constValueSeries : BaseSeries
{
    // Start is called before the first frame update

    public int m_i32Value;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public int GetValue()
    {
        return m_i32Value;
    }
    
}
