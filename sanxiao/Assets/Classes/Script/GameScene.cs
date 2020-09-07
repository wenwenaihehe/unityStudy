using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    // Start is called before the first frame update
    public int nHeight = 9;
    public int nWidth = 9;
    private GameObject pFruitBack;
    void Start()
    {
        pFruitBack = GameObject.Find("Back");
        GameObject pFruit = (GameObject)Resources.Load("Prefab/Image");
        GameObject pRet = Instantiate(pFruit);
        pRet.transform.SetParent(pFruitBack.transform);
        pRet.transform.localPosition = Vector3.zero;
        pRet.transform.localScale = Vector3.one;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
