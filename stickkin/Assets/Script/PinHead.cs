﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinHead : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //if (collision.tag == "PinHead")
        //{
        //    GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
        //}
        Debug.Log("Collision123");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
    }
}
