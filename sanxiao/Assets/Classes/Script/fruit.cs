using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fruit : MonoBehaviour
{
    // Start is called before the first frame update
    private int m_nColor;

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

    public void onDisposed()
    {
        Destroy(gameObject);
    }
    public int getColor()
    {
        return m_nColor;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
