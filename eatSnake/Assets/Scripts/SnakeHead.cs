using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public float vlLocity = 0.35f;
    public int step; //蛇每次走多少
    private int x;
    private int y;  //蛇的速度

    private Vector3 headPos; //蛇头的坐标
    private void Start()
    {
        InvokeRepeating("Move", 0, vlLocity);
        x = 0;
        y = step;
    }
    void Move()
    {
        headPos = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(headPos.x + x, headPos.y + y, headPos.z);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, vlLocity / 2);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, vlLocity);
        }

        if (Input.GetKey(KeyCode.W) && y != -step )
        {
            gameObject.transform.localRotation = Quaternion.Euler(0 , 0 , 0);
            x = 0;
            y = step;
        }
        if (Input.GetKey(KeyCode.S) && y != step)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
            x = 0;
            y = -step;
        }
        if (Input.GetKey(KeyCode.D) && x != -step)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
            x = step;
            y = 0;
        }
        if (Input.GetKey(KeyCode.A) && x != step)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            x = -step;
            y = 0;
        }

    }
}
