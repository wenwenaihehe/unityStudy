using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoateSelf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public float Speed = 90;
    void Update()
    {
        transform.Rotate(new Vector3(0,0,Speed * Time.deltaTime));
    }
}
