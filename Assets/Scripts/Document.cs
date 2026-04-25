using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Document : MonoBehaviour
{
    [SerializeField] private string text;
    [SerializeField] private DocumentText documentText;
    [SerializeField] private List<Decision> decisions = new List<Decision>();
    private int index = 0;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        decisions.Add(gameObject.AddComponent<Decision>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("MouseDown");
        index = (index + 1) % decisions.Count;
        foreach (Decision decision in decisions)
        {
            decision.enabled = decision == decisions[index];
        }
    }
}
