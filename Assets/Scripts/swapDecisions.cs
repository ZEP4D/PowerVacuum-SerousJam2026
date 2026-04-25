using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwapDecisions : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Document document;
    [SerializeField] private int value;
    
    private void Start()
    {
        document = GetComponentInParent<Document>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        document.ChangePage(value);
    }
}
