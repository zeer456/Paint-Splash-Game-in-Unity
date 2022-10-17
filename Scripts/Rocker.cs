using UnityEngine;
using UnityEngine.UI;


public class Rocker : ScrollRect
{
    public GameObject gameobject;

    protected float mRadius = 0.0f;
    public float x, y;
    protected override void Start()
    {
        base.Start();
        //¼ÆËãÒ¡¸Ë¿éµÄ°ë¾¶
        mRadius = (transform as RectTransform).sizeDelta.x * 0.5f;
    }

    public override void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnDrag(eventData);
        var contentPostion = this.content.anchoredPosition;
        if (contentPostion.magnitude > mRadius)
        {
            contentPostion = contentPostion.normalized * mRadius;
            SetContentAnchoredPosition(contentPostion);
        }
        x = contentPostion.x;
        y = contentPostion.y;
    }
    public void update()
    {
        
    }
}