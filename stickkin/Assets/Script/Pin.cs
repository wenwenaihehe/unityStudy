using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isFly = false;
    private bool isReach = false;
    private Transform StartPoint;
    void Start()
    {
        StartPoint = GameObject.Find("StartPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
