using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orSeries : BaseMathSeries
{
    public override int GetValue()
    {
        return m_bOneInSeries.GetValue() | m_bTwoInSeries.GetValue();
    }

}

public class plusSeries : BaseMathSeries
{
    public override int GetValue()
    {
        return m_bOneInSeries.GetValue() & m_bTwoInSeries.GetValue();
    }
}

public class negativeSeries : BaseSeries
{
    public BaseSeries m_bOneInSeries;

    public override int GetValue()
    {
        return ~m_bOneInSeries.GetValue();
    }
}

public class endSeries : BaseSeries
{
    public BaseSeries m_InSeries;

    public override int GetValue()
    {
        return m_InSeries.GetValue();
    }
}