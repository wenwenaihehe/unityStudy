using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class main : EventTrigger
{

    // Start is called before the first frame update
    public override void OnPointerDown(PointerEventData eventData)
    {
        Vector3 worldPosiiotn = GameObject.Find("Camera").GetComponent<Camera>().ScreenToWorldPoint(eventData.position);
        Vector3 worldPos = new Vector3(worldPosiiotn.x, worldPosiiotn.y, 0);
        Collider2D test = Physics2D.OverlapPoint(worldPos);

        if (test != null)
        {
            Debug.Log(test.name);
        }
    }
}
