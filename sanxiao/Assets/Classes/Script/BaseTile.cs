using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    private GameObject m_pItem;
    private GameObject BOARD;
    private int x;
    private int y;
    private void Awake()
    {
        m_pItem = null;
    }
    void Start()
    {
        BOARD = GameObject.Find("Back");
        //m_pItem = null;
    }

    public void setPoint(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public GameObject getItem()
    {
        return m_pItem;
    }
    public void SetItem(GameObject pItem)
    {
        m_pItem = pItem;
    }
    public void AttachItem(GameObject pItem)
    {
        if (m_pItem)
        {
            m_pItem.GetComponent<fruit>().onDisposed();
            m_pItem = null;
        }
   
        m_pItem = pItem;
        if (pItem)
        {
            pItem.GetComponent<fruit>().setTile(gameObject);
        }
    }
    //判断是否进入掉落状态
    public void check()
    {
        if (m_pItem == null)
        {
            return;
        }
        Vector3 nDropDirection = new Vector3(0, -1, 0);
        Vector3 curPoint = new Vector3(x, y, 0);
        Vector3 targetPoint = curPoint + nDropDirection;
        GameObject pTargetTile = BOARD.GetComponent<GameScene>().getTile((int)targetPoint.x, (int)targetPoint.y);
        if (pTargetTile)
        {
            GameObject pTileCom = pTargetTile.GetComponent<BaseTile>().getItem();
            if (pTileCom == null)
            {
                
                m_pItem.GetComponent<fruit>().startDrop(pTargetTile);
                SetItem(null);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
