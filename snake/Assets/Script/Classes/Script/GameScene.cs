using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    // Start is called before the first frame update

    public int i32Width;
    public int i32Height;
    public int i32StartX;
    public int i32StartY;

    public GameObject[,] m_mapTile;
    private void Awake()
    {

    }

    void Start()
    {
        initTile();
        GameObject oSnakeHead = Function.getInstance().LoadPrefab("Prefab/SnakeHead");
        oSnakeHead = Instantiate(oSnakeHead);
        oSnakeHead.transform.localScale = Vector3.one;
        oSnakeHead.transform.SetParent(transform);

        
    }
    void initTile()
    {
        m_mapTile = new GameObject[i32Width, i32Height];
        for (int x = 0; x < i32Width; x++)
        {
            for (int y = 0; y < i32Height; y++)
            {
                GameObject oTile = Function.getInstance().LoadPrefab("Prefab/BaseTile");
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
