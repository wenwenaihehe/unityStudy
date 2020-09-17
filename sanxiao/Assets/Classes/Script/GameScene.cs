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

    private GameObject pFruitBack;
    private GameObject[,] m_pTile;
    private GameObject[,] m_pItem;
    private bool m_drop = true;
    private Vector3 startPoint;
    void Start()
    {
        pFruitBack = GameObject.Find("Back");
        Rect backSize = pFruitBack.GetComponent<RectTransform>().rect;
        //backSize.Contains
        startPoint = new Vector3(backSize.width / 2 * -1, backSize.height / 2 * -1, 0);
        initTile();
        initItem();
        // Invoke("test", 1);
        // CheckConnect();
        CheckConnect();
        InvokeRepeating("updateDropTimer", 0.1f, Time.deltaTime);

    }
    // Update is called once per frame
    void Update()
    {

  
    }
    public int getBoardHeight()
    {
        return nHeight;
    }
    public int getBoardWidth()
    {
        return nWidth;
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
            CheckConnect();
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
    public bool CheckConnect()
    {
        int[, ] mark = new int[nWidth, nHeight];
        for (int nx = 0; nx < nWidth; nx++)
        {
            for (int ny = 0; ny < nHeight; ny++)
            {
                mark[nx, ny] = 0;
            }
        }

        for (int x = 0; x < nWidth; x++)
        {
            for (int y = 0; y < nHeight; y++)
            {
                if (mark[x, y] == 1)
                {
                    continue;
                }
                if (m_pTile[x, y].GetComponent<BaseTile>().getItem() != null)
                {
                    GameObject pItem = m_pTile[x, y].GetComponent<BaseTile>().getItem();
                    ArrayList pPos = GetConnectPos(x, y, pItem.GetComponent<fruit>().getColor());

                    if (pPos.Count >= 3)
                    {
                        ArrayList pTargetPos = getPartternStart(pPos, 3);
                        if (pTargetPos.Count >= 3)
                        {
                            for (int i = 0; i < pTargetPos.Count; i++)
                            {
                                Vector3 curPoint = (Vector3)pTargetPos[i];
                                mark[(int)curPoint.x, (int)curPoint.y] = 1;
                                var pTile = m_pTile[(int)curPoint.x, (int)curPoint.y];
                                if (pTile)
                                {
                                    pTile.GetComponent<BaseTile>().AttachItem(null);
                                }
                            }
                        }
                    }
                }
            }
        }
        return true;
    }
    ArrayList GetConnectPos(int x, int y, int color)
    {
        ArrayList targetList = new ArrayList();
        int[,] mark = new int[nWidth, nHeight];

        for (int nx = 0; nx < nWidth; nx++)
        {
            for (int ny = 0; ny < nHeight; ny++)
            {
                mark[nx, ny] = 0;
            }
        }
        getConnect(color, x, y, targetList, mark);
        return targetList;
    }
    void getConnect(int color, int x, int y, ArrayList vector3s, int[,] mark)
    {
        if (x < 0 || x >= nWidth || y < 0 || y >= nWidth)
        {
            return;
        }
        if (mark[x, y] == 1)
        {
            return;
        }
        Vector3 curPos = new Vector3(x, y);
        Vector3[] pPos = { new Vector3(0, 1), new Vector3(1, 0), new Vector3(-1, 0), new Vector3(0, -1)};
        mark[x, y] = 1;
        if (m_pTile[x, y].GetComponent<BaseTile>().getItem() != null)
        {
            GameObject pItem = m_pTile[x, y].GetComponent<BaseTile>().getItem();
            if (pItem.GetComponent<fruit>().getColor() == color)
            {
                Vector3 pos = new Vector3(x, y, 0);
                vector3s.Add(pos);
                for (int i = 0; i < 4; i++)
                {
                    Vector3 targetPos = curPos + pPos[i];
                    getConnect(color, (int)targetPos.x, (int)targetPos.y, vector3s, mark);
                }
            }
        }
    }
    ArrayList getPartternStart(ArrayList points, int Line)
    {
        ArrayList _targetList = new ArrayList();
        for (int i = 0; i < points.Count; i++)
        {
            Vector3 curPoint = (Vector3)points[i];
            ArrayList Ts = checkLinePattern(points, Line, curPoint);
            if (Ts.Count >= Line)
            {
                return Ts;
            }
        }
        return _targetList;
    }
    ArrayList checkLinePattern(ArrayList points, int Line, Vector3 curPoint)
    {
        Vector3[] pPos = { new Vector3(0, 1), new Vector3(1, 0), new Vector3(-1, 0), new Vector3(0, -1) };
        ArrayList _targetList = new ArrayList();
        _targetList.Add(curPoint);
        for (int i = 0; i < 4; i++)
        {
            bool haveTarget = true;
            for (int j = 1; j < Line; j++)
            {
                Vector3 Tf = curPoint + pPos[i] * j;
                if (!points.Contains(Tf))
                {
                    haveTarget = false;
                    break;
                }
            }
            if (haveTarget)
            {
                for (int j = 1; j < Line; j++)
                {
                    Vector3 Tf = curPoint + pPos[i] * j;
                    
                    _targetList.Add(Tf);
                }
                break;
            }
        }

        return _targetList;
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
        
    void startDrop()
    {

    }
}
