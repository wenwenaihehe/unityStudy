using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Item : MonoBehaviour
{
    protected Tile m_Tile;
    public Text m_Text;

    public void Init()
    {
        GetComponent<RectTransform>().anchoredPosition = m_Tile.GetComponent<RectTransform>().anchoredPosition;
        GetComponent<RectTransform>().sizeDelta = m_Tile.GetComponent<RectTransform>().sizeDelta;
    }

    public void setTile(Tile tile)
    {
        m_Tile = tile;
        m_Text.text = m_Tile.m_index.ToString();
    }

    public void OnDrag(Vector2 offset)
    {
        GetComponent<RectTransform>().anchoredPosition += offset;
    }
}

