using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PinchZoom : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
     public float ZoomSpeedPinch = 0.001f;
    public float ZoomSpeedMouseScrollWheel = 0.05f;
    public float ZoomMin = 0.1f;
    public float ZoomMax = 1f;
    RectTransform rectTransform;
    public int type = 1; // for device testing type 1 use LateUpdate; type 2 use Update

    public ScrollRect TheScrollRect;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Zoom()
    {
        var mouseScrollWheel = Input.mouseScrollDelta.y;
        float scaleChange = 0f;
        Vector2 midPoint = Vector2.zero;
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = touchDeltaMag - prevTouchDeltaMag;

            scaleChange = deltaMagnitudeDiff * ZoomSpeedPinch;

            midPoint = (touchOne.position + touchZero.position) / 2;
        }

        if (mouseScrollWheel != 0)
        {
            scaleChange = mouseScrollWheel * ZoomSpeedMouseScrollWheel;
            midPoint = Input.mousePosition;
        }

        if (scaleChange != 0)
        {
            var scaleX = transform.localScale.x;
            scaleX += scaleChange;
            scaleX = Mathf.Clamp(scaleX, ZoomMin, ZoomMax);
            var size = rectTransform.rect.size;
            size.Scale(rectTransform.localScale);
            var parentRect = ((RectTransform)rectTransform.parent);
            var parentSize = parentRect.rect.size;
            parentSize.Scale(parentRect.localScale);

            if (size.x > parentSize.x && size.y > parentSize.y)
            {
                var p1 = Camera.main.ScreenToWorldPoint(midPoint);
                var p2 = transform.InverseTransformPoint(p1);
                var pivotP = rectTransform.pivot * rectTransform.rect.size;
                var p3 = (Vector2)p2 + pivotP;
                var newPivot = p3 / rectTransform.rect.size;
                newPivot = new Vector2(Mathf.Clamp01(newPivot.x), Mathf.Clamp01(newPivot.y));
                SetPivot(rectTransform, newPivot);
            }
            else
            {
                SetPivot(rectTransform, new Vector2(0.5f, 0.5f));
            }

            transform.localScale = new Vector3(scaleX, scaleX, transform.localScale.z);
        }
    }

    private void LateUpdate()
    {
        Zoom();
    }

    public static void SetPivot(RectTransform rectTransform, Vector2 pivot)
    {
        Vector3 deltaPosition = rectTransform.pivot - pivot;    // get change in pivot
        deltaPosition.Scale(rectTransform.rect.size);           // apply sizing
        deltaPosition.Scale(rectTransform.localScale);          // apply scaling
        deltaPosition = rectTransform.rotation * deltaPosition; // apply rotation

        rectTransform.pivot = pivot;                            // change the pivot
        rectTransform.localPosition -= deltaPosition;           // reverse the position change
    }

    public void OnDrag(PointerEventData eventData)
    {
        Zoom();
        if (Input.touchCount <= 1)
        {
            TheScrollRect.OnDrag(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        TheScrollRect.OnEndDrag(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.touchCount <= 1)
        {
            TheScrollRect.OnBeginDrag(eventData);
        }
    }
}
