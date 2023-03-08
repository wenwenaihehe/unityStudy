using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class XCenterZoom : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    ScrollRect scrollRect;
    enum Direction
    {
        Horizontal,
        Vertical
    }
    [SerializeField]
    Direction direction = Direction.Horizontal;

    [SerializeField]
    AnimationCurve animationCurve;//曲线

    [SerializeField]
    [Range(0.5f, 1)]
    float scaleSpace = 1;//调整缩放的速度

    int childCount = 0;//列表Content中子对象个数
    float spacingRatio = 0;//间距比，
    bool isDrag = false;

    //拖拽结束时，在哪个下标
    int GetEndIndex()
    {
        float pos = GetNormalizedPosition();
        if (pos < 0)
        {
            return 1;
        }
        else if (pos > 1)
        {
            return childCount;
        }
        for (int i = 0; i < childCount; i++)
        {
            float minPos = (i * spacingRatio) - (spacingRatio / 2);
            float maxPos = (i * spacingRatio) + (spacingRatio / 2);
            if (pos >= minPos && pos <= maxPos)
            {
                return i + 1;
            }
        }
        return -1;
    }
    //获取拖到进度
    float GetNormalizedPosition()
    {
        //scrollRect的进度(从上往下，从左往右)值是1->0，我们要给他翻转一下，得到0->1的值
        if (direction == Direction.Horizontal)
        {
            return -scrollRect.horizontalNormalizedPosition + 1;
        }
        else
        {
            return -scrollRect.verticalNormalizedPosition + 1;
        }
    }
    //修改拖到进度
    void SetNormalizedPosition(float value)
    {
        if (direction == Direction.Horizontal)
        {
            scrollRect.horizontalNormalizedPosition = -value + 1;
        }
        else
        {
            scrollRect.verticalNormalizedPosition = -value + 1;
        }
    }
    //获取移动速度
    float GetMoveSpeed()
    {
        if (direction == Direction.Horizontal)
        {
            return Mathf.Abs(scrollRect.velocity.x);
        }
        else
        {
            return Mathf.Abs(scrollRect.velocity.y);
        }
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
    }

    void Start()
    {
        scrollRect = gameObject.GetComponent<ScrollRect>();
        childCount = scrollRect.content.childCount;
        spacingRatio = 1.0f / (childCount - 1);
    }
    void Update()
    {
        if (childCount > 1)
        {
            UpdateScale();
            UpdatePos();
        }
        else if (childCount == 1)
        {
            //一个子对象要特殊处理一下
            var scale = animationCurve.Evaluate(0.5f);
            Transform cell = scrollRect.content.GetChild(0);
            cell.localScale = Vector3.one * (scale + 1);
        }
    }
    //增加拖拽结束后，位置回正
    void UpdatePos()
    {
        float endIndex = GetEndIndex();
        float moveSpeed = GetMoveSpeed();
        if (isDrag == false && moveSpeed < 100 && endIndex != -1)
        {
            float pos = GetNormalizedPosition();
            float endPos = (endIndex - 1) * spacingRatio;
            float space = Mathf.Abs(endPos - pos) / 5;
            float value = Mathf.Lerp(pos, endPos, space);
            if (value < 0.001f)
            {
                value = endPos;
                scrollRect.StopMovement();
            }
            SetNormalizedPosition(value);
        }
    }
    //子对象的缩放
    void UpdateScale()
    {
        float pos = GetNormalizedPosition();
        for (int i = 0; i < childCount; i++)
        {
            //此对象的中心点值
            float centrePos = i * spacingRatio;
            //此对象缩放开始的地方（进度值，当value走到这里后开始缩放）
            float startPos = centrePos - (spacingRatio * scaleSpace);
            float endPos = centrePos + (spacingRatio * scaleSpace);
            float t = pos - startPos;
            t = t / (endPos - startPos);
            var scale = animationCurve.Evaluate(t);
            Transform cell = scrollRect.content.GetChild(i);
            cell.localScale = Vector3.one * (scale + 1);
        }
    }
}