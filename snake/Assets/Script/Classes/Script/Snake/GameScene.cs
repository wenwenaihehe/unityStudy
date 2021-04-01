using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GameScene : MonoBehaviour
{
    // Start is called before the first frame update

    public int i32Width;
    public int i32Height;
    public int i32StartX;
    public int i32StartY;
    public float fTime = 0.3f;

    public GameObject[,] m_mapTile;
    List<GameObject> m_vecSnake;
    SnakeHead m_pHead;
    Vector2 m_headPos;
    private void Awake()
    {
    
    }

    void Start()
    {
        initTile();
        GameObject pSnake = Function.getInstance().LoadPrefab("Snake/Prefab/SnakeHead");
        pSnake = Instantiate(pSnake);
        pSnake.transform.SetParent(transform);

        m_pHead = pSnake.GetComponent<SnakeHead>();
        m_pHead.transform.localPosition = m_mapTile[i32Width / 2, i32Height / 2].transform.localPosition;
        m_pHead.transform.localScale = Vector3.one;
        m_headPos = new Vector2(i32Width / 2, i32Height / 2);
        InvokeRepeating("updateTimer", 0, fTime);
    }
    void initTile()
    {
        m_mapTile = new GameObject[i32Width, i32Height];
        for (int x = 0; x < i32Width; x++)
        {
            for (int y = 0; y < i32Height; y++)
            {
                GameObject oTile = Function.getInstance().LoadPrefab("Snake/Prefab/BaseTile");
                oTile = Instantiate(oTile);
                oTile.transform.SetParent(transform);
                oTile.transform.localScale = Vector3.one;
                m_mapTile[x, y] = oTile;

                int i32Width = (int)oTile.GetComponent<RectTransform>().rect.width;
                int i32Height = (int)oTile.GetComponent<RectTransform>().rect.height;
                Vector3 oPos = new Vector3(x * i32Width, y * i32Height) + new Vector3(i32StartX, i32StartY);
                oTile.transform.localPosition = oPos;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_pHead.gameObject.transform.SetParent(null);
            //m_pHead.onRecieveKeyCode(KeyCode.LeftArrow);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_pHead.onRecieveKeyCode(KeyCode.RightArrow);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_pHead.onRecieveKeyCode(KeyCode.UpArrow);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_pHead.onRecieveKeyCode(KeyCode.DownArrow);
        }

    }
    void updateTimer()
    {
        Vector2 curOffset = new Vector2(0, 0);
        if (m_pHead.m_emCode == KeyCode.UpArrow)
        {
            curOffset = new Vector2(0, 1);
        }
        if (m_pHead.m_emCode == KeyCode.RightArrow)
        {
            curOffset = new Vector2(1, 0);
        }
        if (m_pHead.m_emCode == KeyCode.LeftArrow)
        {
            curOffset = new Vector2(-1, 0);
        }
        if (m_pHead.m_emCode == KeyCode.DownArrow)
        {
            curOffset = new Vector2(0, -1);
        }
        m_headPos += curOffset;

        Vector3 targetPos = m_mapTile[(int)m_headPos.x, (int)m_headPos.y].transform.localPosition;
        Vector3 curPos = m_pHead.transform.localPosition;
        m_pHead.gameObject.transform.DOLocalMove(targetPos, fTime).SetEase(Ease.Linear);
    }
}
