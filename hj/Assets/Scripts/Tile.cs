using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class Tile:MonoBehaviour
{
    protected Item m_Item { get; set; }

    public int m_index { get; set; }

    test2 m_Partent;
    public void Init(int index, Vector2 pos, Vector2 size, test2 test)
    {
        m_index = index;
        GetComponent<RectTransform>().anchoredPosition = pos;
        GetComponent<RectTransform>().sizeDelta = size;

        m_Partent = test;
    }

    public void setItem(Item obj)
    {
        m_Item = obj;
        m_Item.setTile(this);
    }

    public void onDrag(Vector2 Offset)
    {
        if (m_Item != null)
        {
            Vector2 CurPos = m_Item.GetComponent<RectTransform>().anchoredPosition;
            Vector2 nextPos = CurPos + Offset;

            Tile LeftTile = null;
            Tile RightTile = null;
            if (Math.Abs(CurPos.x - GetComponent<RectTransform>().anchoredPosition.x) < 0.1)
            {
                if (Offset.x > 0)
                {
                    RightTile = m_Partent.GetTile(m_index + 1);
                    LeftTile = this;
                }
                else
                {
                    LeftTile = m_Partent.GetTile(m_index - 1);
                    RightTile = this;
                }

            }
            else if (CurPos.x > GetComponent<RectTransform>().anchoredPosition.x)
            {
                RightTile = m_Partent.GetTile(m_index + 1);
                LeftTile = this;   
            }
            else if (CurPos.x < GetComponent<RectTransform>().anchoredPosition.x)
            {
                RightTile = this;
                LeftTile = m_Partent.GetTile(m_index - 1);
            }

            m_Item.GetComponent<RectTransform>().anchoredPosition = getPosition(LeftTile, RightTile, CurPos.x + Offset.x);
            m_Item.GetComponent<RectTransform>().sizeDelta = getSize(LeftTile, RightTile, CurPos.x + Offset.x);
        }
    }

    Vector2 getPosition(Tile LeftTile, Tile RightTile, float fCurPosX)
    {
        if (LeftTile == null)
        {
            return new Vector2(fCurPosX, RightTile.GetComponent<RectTransform>().anchoredPosition.y);
        }
        else if (RightTile == null)
        {
            return new Vector2(fCurPosX, LeftTile.GetComponent<RectTransform>().anchoredPosition.y);
        }

        float fCurWidth = Math.Abs(fCurPosX - LeftTile.GetComponent<RectTransform>().anchoredPosition.x);
        Vector2 Target = new Vector2();
        float fAllWidth = Math.Abs(RightTile.GetComponent<RectTransform>().anchoredPosition.x - LeftTile.GetComponent<RectTransform>().anchoredPosition.x);
        float fHeight = RightTile.GetComponent<RectTransform>().anchoredPosition.y - LeftTile.GetComponent<RectTransform>().anchoredPosition.y;
        Target.x = fCurPosX;
        Target.y = (fCurWidth / fAllWidth) * fHeight + LeftTile.GetComponent<RectTransform>().anchoredPosition.y;

        return Target;
    }

    Vector2 getSize(Tile LeftTile, Tile RightTile, float fCurPosX)
    {
        if (LeftTile == null)
        {
            return RightTile.GetComponent<RectTransform>().sizeDelta;
        }
        else if (RightTile == null)
        {
            return LeftTile.GetComponent<RectTransform>().sizeDelta;
        }

        float fCurWidth = Math.Abs(fCurPosX - LeftTile.GetComponent<RectTransform>().anchoredPosition.x);
        Vector2 Target = new Vector2();
        float fAllWidth = Math.Abs(RightTile.GetComponent<RectTransform>().anchoredPosition.x - LeftTile.GetComponent<RectTransform>().anchoredPosition.x);
        float fHeight = RightTile.GetComponent<RectTransform>().sizeDelta.y - LeftTile.GetComponent<RectTransform>().sizeDelta.y;
        Target.x = (fCurWidth / fAllWidth) * fHeight + LeftTile.GetComponent<RectTransform>().sizeDelta.y;
        Target.y = (fCurWidth / fAllWidth) * fHeight + LeftTile.GetComponent<RectTransform>().sizeDelta.y;

        return Target;
    }

    public void removeItem()
    {
        Destroy(m_Item.gameObject);
        m_Item = null;
    }

    public Item GetItem()
    {
        return m_Item;
    }
}

