using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwapDecisions_icon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Document_Icon document;
    [SerializeField] private int value;
    [SerializeField] AudioClip audioClip;
    [SerializeField] AudioSource audioSource;
    
    private void Start()
    {
        document = GetComponentInParent<Document_Icon>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this.audioSource.PlayOneShot(
            this.audioClip
        );
        document.ChangePage(value);
    }
}
