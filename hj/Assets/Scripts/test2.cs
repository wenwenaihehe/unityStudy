using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class test2 : MonoBehaviour
{
    public GameObject OnePrefab;
    public GameObject OneItem;
    public RectTransform content;

    protected List<Tile> tiles = new List<Tile>();
    float m_dragWidth = 0.0f;
    
    struct TileInfo
    {
        public TileInfo(Vector2 pos, Vector2 size)
        {
            m_pos = pos;
            m_size = size;
        }

        public Vector2 m_pos;
        public Vector2 m_size;
    }

    public void Awake()
    {
        //GetComponent<ScrollRect>().onValueChanged.AddListener(OnDrag);

        List<TileInfo> list = new List<TileInfo>();
        list.Add(new TileInfo(new Vector2(-100, -410), new Vector2(0, 0)));
        list.Add(new TileInfo(new Vector2(100, -346), new Vector2(100, 100)));
        list.Add(new TileInfo(new Vector2(300, -279), new Vector2(150, 150)));
        list.Add(new TileInfo(new Vector2(500, -207), new Vector2(200, 200)));
        list.Add(new TileInfo(new Vector2(700, -279), new Vector2(150, 150)));
        list.Add(new TileInfo(new Vector2(900, -348), new Vector2(100, 100)));
        list.Add(new TileInfo(new Vector2(1100, -410), new Vector2(0, 0)));

        for (int i = 0; i < list.Count; i++)
        {
            GameObject oTileEle = Instantiate(OnePrefab, content);
            oTileEle.GetComponent<Tile>().Init(i, list[i].m_pos, list[i].m_size, this);
            tiles.Add(oTileEle.GetComponent<Tile>());
        }

        for (int i = 0; i < list.Count; i++)
        {
            GameObject oTileEle = Instantiate(OneItem, content);
            tiles[i].setItem(oTileEle.GetComponent<Item>());
            oTileEle.GetComponent<Item>().Init();
        }
    }

    public void OnDrag(Vector2 offset)
    {
        m_dragWidth += offset.x;
   
        if (Math.Abs(m_dragWidth) >= 200)
        {
            updateLogic(m_dragWidth > 0);
            return;
        }

        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].onDrag(offset);
        }
    }

    public Tile GetTile(int i32Index)
    {
        if (i32Index < 0)
        {
            return null;
        }

        if (tiles.Count <= i32Index)
        {
            return null;
        }

        return tiles[i32Index];
    }
    
    public void updateLogic(bool bAdd)
    {
        if (bAdd)
        {
            tiles[tiles.Count - 1].removeItem();
            for (int i = tiles.Count - 1; i >= 1; i--)
            {
                Tile tile = tiles[i];
                tile.setItem(tiles[i - 1].GetItem());
            }


            GameObject oTileEle = Instantiate(OneItem, content);
            tiles[0].setItem(oTileEle.GetComponent<Item>());

        }
        else
        {
            tiles[0].removeItem();
            for (int i = 0; i < tiles.Count - 1; i++)
            {
                Tile tile = tiles[i];
                tile.setItem(tiles[i + 1].GetItem());
            }

            GameObject oTileEle = Instantiate(OneItem, content);
            tiles[tiles.Count - 1].setItem(oTileEle.GetComponent<Item>());
        }

        for (int i = 0; i < tiles.Count; i++)
        {
            Tile tile = tiles[i];
            tiles[i].GetItem().Init();
        }

        m_dragWidth = 0;
    }

    public void OnPointerUp()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            Tile tile = tiles[i];
            tiles[i].GetItem().Init();
        }
        m_dragWidth = 0;
    } 
}
