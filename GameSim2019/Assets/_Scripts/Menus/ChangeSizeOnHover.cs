using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeSizeOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rt;
    private Vector3 orginal;
    private Vector3 hovered;
    public float Multiplier = 1.1f;

    // Start is called before the first frame update
    void Start ()
    {
        rt = GetComponent<RectTransform>();
        orginal = rt.localScale;
        hovered = orginal;
        hovered *= Multiplier;
    }

    public void OnPointerEnter ( PointerEventData eventData )
    {
        rt.localScale = hovered;
    }

    public void OnPointerExit ( PointerEventData eventData )
    {
        rt.localScale = orginal;
    }
}
