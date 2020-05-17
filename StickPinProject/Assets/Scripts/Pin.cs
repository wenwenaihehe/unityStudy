using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float speed = 5;
    private bool isFly = false;
    private bool isReach = false;
    private Transform startPoint;

    private Vector3 targetCirclePos;
    private Transform circle;


	// Use this for initialization
	void Start () {
        startPoint = GameObject.Find("StartPoint").transform;
        circle = GameObject.FindGameObjectWithTag("Circle").transform;
        targetCirclePos = circle.position;
        targetCirclePos.y -= 1.55f;
        //circle = GameObject.Find("Circle").transform;
    }

    // Update is called once per frame
    void Update () {
        if (isFly == false)
        {
            if (isReach == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, startPoint.position) < 0.05f)
                {
                    isReach = true;
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetCirclePos, speed * Time.deltaTime);
            if(Vector3.Distance( transform.position,targetCirclePos) < 0.05f)
            {
                transform.position = targetCirclePos;
                transform.parent = circle;
                isFly = false;
            }
        }
	}

    public void StartFly()
    {
        isFly = true;
        isReach = true;
    }
}
