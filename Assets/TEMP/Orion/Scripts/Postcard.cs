using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Postcard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _image;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _rectTransform.position -= new Vector3(0, 100, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _rectTransform.position += new Vector3(0, 100, 0);
    }

    public void InitCard(Sprite sprite)
    {
        _image.sprite = sprite;
    }
}
