using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    private GameObject m_pItem;
    void Start()
    {
        m_pItem = null;
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
            Destroy(m_pItem);
            m_pItem = null;
        }
        m_pItem = pItem;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
