using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class swapDecisionsTest : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Document_test document;
    [SerializeField] private int value;
    
    private void Start()
    {
        document = GetComponentInParent<Document_test>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        document.ChangePage(value);
    }
}
