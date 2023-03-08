using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameLayer : MonoBehaviour
{
    // Start is called before the first frame update
    static public GameLayer instance;
    public Text m_text;
    int m_Length;
    public SnakeHead m_head;
    bool m_canRefreshFruit = true;
    public GameObject m_fruitPrefab;
    private void Awake()
    {
        m_Length = 0;
        instance = this;
        m_canRefreshFruit = true;
    }

    public void AddLength(GameObject obj)
    {
        Destroy(obj);
        m_canRefreshFruit = true;
        m_Length++;
        m_head.AddBody();
        m_text.text = m_Length.ToString();
    }

    Vector2 getRandomPosition()
    {
        return new Vector3(Random.value * 720 + 10, Random.value * 1200 + 20);
    }



    // Update is called once per frame
    void Update()
    {
        if (m_canRefreshFruit)
        {
            GameObject fruit = Instantiate(m_fruitPrefab, transform);
            Vector2 pos = getRandomPosition();
            Debug.Log(pos);
            fruit.GetComponent<RectTransform>().anchoredPosition = pos;

            m_canRefreshFruit = false;
        }

  
    }
}
