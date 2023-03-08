using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class click : EventTrigger
{
    protected Vector2 m_lateUpdatePos = Vector2.zero;
    public override void OnDrag(PointerEventData eventData)
    {
        if (m_lateUpdatePos == Vector2.zero)
        {
            m_lateUpdatePos = eventData.pressPosition;
        }


        Vector2 pressPosition = m_lateUpdatePos;
        Vector2 CurPosition = eventData.position;

        Vector2 offset = CurPosition - pressPosition;


        GetComponent<test2>().OnDrag(offset);
        m_lateUpdatePos = CurPosition;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<test2>().OnPointerUp();
        m_lateUpdatePos = Vector2.zero;
    }
}
