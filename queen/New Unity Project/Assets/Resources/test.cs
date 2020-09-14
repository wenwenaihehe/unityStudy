using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject m_pTest;
    void Start()
    {
        GameObject pTest0 = new GameObject();
        for (int i = 0; i < 4; i++)
        {
            GameObject pTest = getTestImage();
            pTest.transform.SetParent(gameObject.transform);
            pTest.transform.localPosition = new Vector2(i * 30, 0);

            pTest.transform.localScale = Vector3.one;
           // pTest.GetComponent<>().color = new Color(10 * i, 10 * i, 100 + 30 * i);
            if (i == 0)
            {
                // pTest.GetComponent<Renderer>().sortingOrder = 10;
                m_pTest = pTest;

            
            }
            //if (i == 1)
            //{
            //    pTest.transform.SetParent(pTest0.transform);
            //}

        }
    }

    GameObject getTestImage()
    {
        GameObject pTest = (GameObject)Resources.Load("prefab/Image");
        GameObject pRet = Instantiate(pTest);
        return pRet;
    }
    // Update is called once per frame
    void Update()
    {
        m_pTest.transform.SetSiblingIndex(10);
    }
}
