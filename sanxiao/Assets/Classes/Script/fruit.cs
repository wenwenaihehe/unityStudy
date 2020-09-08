using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fruit : MonoBehaviour
{
    // Start is called before the first frame update
    private int m_nColor;

    void Start()
    {
        m_nColor = Random.Range(1 , 11);

        string address = "xiaoImage/star";
        address = address + m_nColor.ToString();
        Sprite Tb3 = (Sprite)Resources.Load(address, typeof(Sprite)) as Sprite;
        gameObject.GetComponent<Image>().sprite = Tb3;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
