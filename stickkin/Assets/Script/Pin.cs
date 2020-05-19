using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isFly = false;
    private bool isReach = false;
    private Transform StartPoint;
    public float speed = 5;
    private Transform circle;

    private Vector3 targetCirclePos;
    void Start()
    {
        StartPoint = GameObject.Find("StartPoint").transform;
        circle = GameObject.Find("Circle").transform;
        targetCirclePos = circle.position;
        targetCirclePos.y = targetCirclePos.y - 1.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFly == false )
        {
            if(isReach == false)
            {
                transform.position = Vector2.MoveTowards(transform.position,StartPoint.position,speed*Time.deltaTime);
                if(Vector2.Distance(transform.position,StartPoint.position) < 0.05f)
                {
                    isReach = true;
                }
            }
            else
            {

            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position,targetCirclePos,speed * Time.deltaTime);
            if(Vector2.Distance(transform.position,targetCirclePos) < 0.05f)
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
