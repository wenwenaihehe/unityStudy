using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fruit : MonoBehaviour
{
    // Start is called before the first frame update
    private int m_nColor;
    private GameObject m_pTile;
    private bool m_bDrop = false;
    private bool m_bIsAct = false;
    private void Awake()
    {
        m_nColor = Random.Range(1, 5);
    }
    void Start()
    {
        string address = "xiaoImage/star";
        address = address + m_nColor.ToString();
        Sprite Tb3 = (Sprite)Resources.Load(address, typeof(Sprite)) as Sprite;
        gameObject.GetComponent<Image>().sprite = Tb3;
    }
    public void startDrop(GameObject pTile)
    {
        if (m_bIsAct == true)
        {
            return;
        }
        if (m_pTile)
        {
            m_pTile.GetComponent<BaseTile>().SetItem(null);
        }
        m_bIsAct = true;
        setTile(pTile);
        pTile.GetComponent<BaseTile>().SetItem(gameObject);

        transform.SetAsLastSibling();
        transform.parent = m_pTile.transform;
        transform.localPosition = Vector3.zero + new Vector3(0, m_pTile.GetComponent<RectTransform>().rect.height);
        transform.localScale = Vector3.one;

        InvokeRepeating("updateDrop", 0, Time.deltaTime);
      

        m_bDrop = true;
    }
    public void onDisposed()
    {
        Destroy(gameObject);
    }
    public int getColor()
    {
        return m_nColor;
    }
    public void setTile(GameObject pTile)
    {
        m_pTile = pTile;
    }

    public void setDrop(bool bDrop)
    {
        m_bDrop = bDrop;
    }
    public bool getDrop()
    {
        return m_bDrop;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void updateDrop()
    {
        Vector3 targetPos = m_pTile.transform.TransformPoint(0, 0, 0);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 1 * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
            transform.position = targetPos;
            m_bDrop = false;
            m_bIsAct = false;
           // m_pTile.GetComponent<BaseTile>().SetItem(gameObject)
            CancelInvoke("updateDrop");
            m_pTile.GetComponent<BaseTile>().check();
        }
     
    }
}
