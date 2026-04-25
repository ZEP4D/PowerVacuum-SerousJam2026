using System;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class Decision : MonoBehaviour
{
    public enum StampState
    {
        None, Approved, Disapproved
    }

    public enum TypeOfProject
    {
        Build, Demolish, Replace
    }
    
    [SerializeField] private StampState stampState = StampState.None;
    [SerializeField] private string text;
    [SerializeField] private Collider2D trigger;
    [SerializeField] private List<string> powerPlants;
    [SerializeField] private TypeOfProject typeOfProject;
    
    public void Start()
    {
        // _textMesh = GetComponentInChildren<TextMeshPro>();
        // _textMesh.text = text;
    }
    public bool IsStamped()
    {
        return stampState != StampState.None;
    }

    void OnMouseDown()
    {
        switch (stampState)
        {
            case StampState.Approved:
                Debug.Log("Approved");
                break;
            case StampState.Disapproved:
                Debug.Log("Disapproved");
                break;
            default:
                Debug.Log("Choice not made");
                break;
        }
    }

    void OnMouseOver()
    {
        // Debug.Log("Is hovering");
    }
}
