using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.UIElements;

public class GameScene : MonoBehaviour
{
    // Start is called before the first frame update
    public int nHeight = 9;
    public int nWidth = 9;
    public int beginX = 70;
    public int beginY = 70;
    public int SpacingX = 5; //x的间隔
    public int SpacingY = 5; //y的间隔
    public int TileWidth = 55;
    public int TileHeigiht = 55;

    private GameObject pFruitBack;
    private GameObject[,] m_pTile;
    private GameObject[,] m_pItem;
    private bool m_drop = true;
    private Vector3 startPoint;

    private GameObject m_pClickItem;

    static private GameScene m_pInstance = null;
    static public GameScene getInstance()
    {
        return m_pInstance;
    }
    ~GameScene()
    {
        m_pInstance = null;
    }
    private void Awake()
    {
        m_pInstance = this;
        //Input.mousePresent = true;
    }
    void Start()
    {
        pFruitBack = GameObject.Find("Back");
        Rect backSize = pFruitBack.GetComponent<RectTransform>().rect;
        startPoint = new Vector3(backSize.width / 2 * -1, backSize.height / 2 * -1, 0);
        initTile();
        initItem();
       // InvokeRepeating("updateDropTimer", 0.1f, Time.deltaTime);
        m_pClickItem = null;
        StateMachine.getInstance().setState(StateMachine.stateType.STATE_START_GAME);
    }
    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) == true)
        {
            m_pClickItem = null;
            Vector3 pCurPoint = Input.mousePosition;
            if (m_pClickItem == null)
            {
                int curX = (int)((pCurPoint.x - beginX) / TileWidth);
                int curY = (int)((pCurPoint.x - beginY) / TileHeigiht);
                if (curX >= 0 && curX < nWidth && curY >=0 && curY < nHeight)
                {
                    m_pClickItem = m_pTile[curX, curY];
                }
            }
        }

        if (Input.GetKey(KeyCode.Mouse0) && m_pClickItem != null)// || Input.GetMouseButtonDown(0) == true)
        {
            Vector3 curMousePoint = Input.mousePosition;
            m_pClickItem.transform.position = curMousePoint;
            m_pClickItem.transform.localPosition = curMousePoint;
            Debug.Log("sssssss" + curMousePoint.x.ToString());

        }
        if (Input.GetKeyUp(KeyCode.Mouse0) == true)
        {
            m_pClickItem = null;
            Debug.Log("3333333");
        }

    }
    public int getBoardHeight()
    {
        return nHeight;
    }
    public int getBoardWidth()
    {
        return nWidth;
    }
    public void startDrop()
    {
        InvokeRepeating("updateDropTimer", 0.0f, Time.deltaTime);
    }
    public void dropOver()
    {
        CancelInvoke("updateDropTimer");
        StateMachine.getInstance().setState(StateMachine.stateType.STATE_DROP_OVER);
    }
    void updateDropTimer()
    {
        m_drop = false;
        for (int x = 0; x < nWidth; x++)
        {
            for (int y = 0; y < nHeight; y++)
            {
                m_pTile[x, y].GetComponent<BaseTile>().check();
            }
        }
        for (int x = 0; x < nWidth; x++)
        {
            for (int y = 0; y < nHeight; y++)
            {
                BaseTile pTile = m_pTile[x, y].GetComponent<BaseTile>();
                GameObject pItem = pTile.getItem();
                if (pItem && pItem.GetComponent<fruit>().getDrop() == true)
                {
                    m_drop = true;
                    break;
                }
            }
        }
        if (m_drop == false)
        {
            dropOver();
        }
        else
        {
            //InvokeRepeating("updateDropTimer", 0, Time.deltaTime);
            //CancelInvoke
        }
    }
    void initTile()
    {
        m_pTile = new GameObject[nWidth, nHeight];

        for (int x = 0; x < nWidth; x++)
        {
            for (int y = nHeight - 1; y >=0; y--)
            {
                GameObject pTile = getTile();
                pTile.transform.SetParent(pFruitBack.transform);
                float nWidth = pTile.GetComponent<RectTransform>().rect.width;
                float nHeight = pTile.GetComponent<RectTransform>().rect.height;


                pTile.GetComponent<BaseTile>().setPoint(x, y);
                pTile.transform.localScale = Vector3.one;
                Vector3 pos = new Vector3(beginX + x * (nWidth + SpacingX), beginY + y * (nHeight + SpacingY), 0);
                pTile.transform.localPosition = pos + startPoint;

                m_pTile[x, y] = pTile;
                pTile.transform.SetSiblingIndex(100 - (x * y));
                //pTile.GetComponent<SpriteRenderer>().sortingOrder = 100- x * y;
           
            }
        }
    }
    GameObject getTile()
    {
        GameObject pTile = (GameObject)Resources.Load("Prefab/Tile");
        GameObject pRet = Instantiate(pTile);
        pRet.transform.localPosition = Vector3.zero;
        pRet.transform.localScale = Vector3.one;
        return pRet;
    }
    void initItem()
    {
        m_pItem = new GameObject[nWidth, nHeight];

        for (int x = 0; x < nWidth; x++)
        {
            for (int y = 0; y < nHeight; y++)
            {
                GameObject pTile = m_pTile[x, y];
                GameObject pItem = getItem();
                pItem.transform.SetParent(pTile.transform);
                pItem.transform.localScale = Vector3.one;
                pItem.transform.localPosition = Vector3.zero;

                pTile.GetComponent<BaseTile>().SetItem(pItem);
                //pItem.GetComponent<fruit>().setTile(pTile);
            }
        }
    }
    public GameObject getItem()
    {
        GameObject pItem = (GameObject)Resources.Load("Prefab/Image");
        GameObject pRet = Instantiate(pItem);
        pRet.transform.localPosition = Vector3.zero;
        return pRet;
    }
    public GameObject[, ] getAllTile()
    {
        return m_pTile;
    }
    public GameObject getTile(int x, int y)
    {
        if (x < 0 || x >= nWidth || y < 0 || y >= nHeight)
        {
            return null;
        }
        return m_pTile[x, y];
    }
}
