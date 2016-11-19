using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public static bool isHover = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHover = false;
    }
}
