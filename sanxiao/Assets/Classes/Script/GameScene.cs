using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Vector3 startPoint;
    void Start()
    {
        pFruitBack = GameObject.Find("Back");
        Rect backSize = pFruitBack.GetComponent<RectTransform>().rect;
        startPoint = new Vector3(backSize.width / 2 * -1, backSize.height / 2 * -1, 0);
        initTile();
        initItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void initTile()
    {
        m_pTile = new GameObject[nWidth, nHeight];

        for (int x = 0; x < nWidth; x++)
        {
            for (int y = 0; y < nHeight; y++)
            {
                GameObject pTile = getTile();
                pTile.transform.SetParent(pFruitBack.transform);
                float nWidth = pTile.GetComponent<RectTransform>().rect.width;
                float nHeight = pTile.GetComponent<RectTransform>().rect.height;

              
                pTile.transform.localScale = Vector3.one;
                Vector3 pos = new Vector3(beginX + x * (nWidth + SpacingX), beginY + y * (nHeight + SpacingY), 0);
                pTile.transform.localPosition = pos + startPoint;

                m_pTile[x, y] = pTile;
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
            }
        }
    }
    GameObject getItem()
    {
        GameObject pItem = (GameObject)Resources.Load("Prefab/Image");
        GameObject pRet = Instantiate(pItem);
        pRet.transform.localPosition = Vector3.zero;
        return pRet;
    }

}
