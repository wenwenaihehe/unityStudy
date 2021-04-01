using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xuePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        rb2D.drag = 1;
        rb2D.mass = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
            rb2D.AddForce(new Vector2(-5, 0), ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
            if (rb2D)
            {
                rb2D.AddForce(new Vector2(5, 0), ForceMode2D.Impulse);
            }
        }
    }
}
