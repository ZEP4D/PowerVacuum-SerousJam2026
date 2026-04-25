using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class Document : MonoBehaviour
{
    [SerializeField] private GameObject description;
    [SerializeField] private GameObject pros;
    [SerializeField] private GameObject cons;
    [SerializeField] private GameObject cost;
    [SerializeField] private List<string> texts;
    [SerializeField] private List<Decision> decisions = new List<Decision>();
    [SerializeField, CanBeNull] private Decision currentDecision = null;
    [SerializeField] private int index = 0;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setCurrentDecision(Decision decision)
    {
        if (decision == null)
        {
            
        }
    }
    void OnMouseDown()
    {
        Debug.Log("MouseDown");
        index = (index + 1) % (decisions.Count + 1);
        if (index > 0)
        {
            currentDecision = decisions[index - 1];
        }
        else
        {
            currentDecision = null;
        }
        Debug.Log(currentDecision + " " + index);
    }
}
