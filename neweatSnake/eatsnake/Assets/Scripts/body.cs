using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body : MonoBehaviour
{
    List<Vector2> m_WaitPos = new List<Vector2>();
    // Start is called before the first frame update
    public void init(float x, float y)
    {
        for (int i = 0; i < 10; i++)
        {
            m_WaitPos.Add(new Vector2(x, y));
        }
    }

    public void updatePos(Vector2 offeset)
    {
        GetComponent<RectTransform>().anchoredPosition = offeset + GetComponent<RectTransform>().anchoredPosition;
        m_WaitPos.RemoveAt(m_WaitPos.Count - 1);
        m_WaitPos.Insert(0, offeset);
    }

    public Vector2 getOffset()
    {
        return m_WaitPos[m_WaitPos.Count - 1];
    }

    public Vector2 getNextPos()
    {
        Vector2 target = GetComponent<RectTransform>().anchoredPosition;
        for (int i = 0; i < m_WaitPos.Count; i++)
        {
            target = target + m_WaitPos[i] * -1;
        }

        return target;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
