using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SnakeHead : MonoBehaviour
{
    // Start is called before the first frame update
    public KeyCode m_emCode { get; set; }//= KeyCode.UpArrow;
    void Start()
    {
        Debug.Log(transform.position);
    }
    public void onRecieveKeyCode(KeyCode emCode)
    {
        m_emCode = emCode;
    }
    // Update is called once per frame
    void Update()
    {
        if (m_emCode == KeyCode.UpArrow)
        {

        }
        if (m_emCode == KeyCode.DownArrow)
        {

        }
        if (m_emCode == KeyCode.LeftArrow)
        {

        }
        if (m_emCode == KeyCode.RightArrow)
        {

        }
    }
}
