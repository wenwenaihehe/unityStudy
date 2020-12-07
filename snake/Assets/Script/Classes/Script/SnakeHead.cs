using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SnakeHead : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.position);
        Vector3 endpos = transform.localPosition + new Vector3(0, 200);
        
        Tween Act = this.transform.DOLocalMove(endpos, 3.0f);
        Act.SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
