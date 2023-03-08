using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeHead : MonoBehaviour
{
    float step = 4; //蛇每次走多少
    private float x;
    private float y;  //蛇的速度

    public GameObject m_fruitPrefab;

    List<body> m_body = new List<body>();
    List<Vector2> m_WaitPos = new List<Vector2>();
    private void Start()
    {
        x = 0;
        y = 0;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) && y != -step )
        {
            gameObject.transform.localRotation = Quaternion.Euler(0 , 0 , 0);
            x = 0;
            y = step;
        }
        if (Input.GetKey(KeyCode.S) && y != step)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
            x = 0;
            y = -step;
        }
        if (Input.GetKey(KeyCode.D) && x != -step)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
            x = step;
            y = 0;
        }
        if (Input.GetKey(KeyCode.A) && x != step)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            x = -step;
            y = 0;
        }

        RectTransform rect = gameObject.GetComponent<RectTransform>();
        Vector2 headPos = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y);
        rect.anchoredPosition = new Vector2(headPos.x + x, headPos.y + y);


        if (m_WaitPos.Count == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                m_WaitPos.Add(new Vector2(x, y));
            }
        }
        else
        {
            m_WaitPos.RemoveAt(m_WaitPos.Count - 1);
            m_WaitPos.Insert(0, new Vector2(x, y));
        }

        for (int i = 0; i < m_body.Count; i++)
        {
            Vector2 offset;
            if (i == 0)
            {
                offset = m_WaitPos[m_WaitPos.Count - 1];
            }
            else
            {
                offset = m_body[i - 1].GetComponent<body>().getOffset();
            }
            m_body[i].updatePos(offset);
        
        }

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

    public void AddBody()
    {
        Update();
        GameObject fruit =  Instantiate(m_fruitPrefab, transform.parent);
        fruit.GetComponent<body>().init(x, y);
        if (m_body.Count == 0)
        {
            fruit.GetComponent<RectTransform>().anchoredPosition = getNextPos();
        }
        else
        {
            fruit.GetComponent<RectTransform>().anchoredPosition = m_body[m_body.Count - 1].getNextPos();
        }

        m_body.Add(fruit.GetComponent<body>());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {


        for (int i = 0; i < m_body.Count; i++)
        {
            Destroy(m_body[i].gameObject);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<fruit>() != null)
        {
            GameLayer.instance.AddLength(collision.gameObject);
            return;
        }
    }
}
