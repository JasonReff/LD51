using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TextOnlyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text textComponent;
    private Color defaultColor;
    public Color tint;

    void Start()
    {
        textComponent = gameObject.GetComponentInChildren<TMP_Text>();
        defaultColor = textComponent.color;
    }

    void Update()
    {
        
    }
 
     public void OnPointerEnter(PointerEventData eventData)
     {
         textComponent.color = tint;
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
         textComponent.color = defaultColor;
     }
}
