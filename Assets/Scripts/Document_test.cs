using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class Document_test : MonoBehaviour
{
    [SerializeField] private GameObject description;
    [SerializeField] private GameObject pros;
    [SerializeField] private GameObject cons;
    [SerializeField] private GameObject cost;
    [SerializeField] private List<Decision_Core> decisions = new List<Decision_Core>();
    [SerializeField, CanBeNull] private Decision_Core currentDecision;
    [SerializeField] public int index;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetCurrentDecision(index > 0 ? decisions[index - 1] : null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentDecision(Decision_Core decision)
    {
        currentDecision = decision;
        if (decision == null)
        {
            description.SetActive(false);
            pros.SetActive(false);
            cons.SetActive(false);
            cost.SetActive(false);
        }
        else
        {
            description.SetActive(true);
            description.GetComponent<TextMeshPro>().text = decision.GetDescription();
            pros.SetActive(true);
            pros.GetComponent<TextMeshPro>().text = decision.GetPros();
            cons.SetActive(true);
            cons.GetComponent<TextMeshPro>().text = decision.GetCons();
            cost.SetActive(true);
            cost.GetComponent<TextMeshPro>().text = decision.GetCons();
        }
    }
    public void ChangePage(int value)
    {
        index = (index + value + decisions.Count + 1) % (decisions.Count + 1);
        SetCurrentDecision(index > 0 ? decisions[index - 1] : null);
        Debug.Log(currentDecision?.GetStampState());
    }

    public Decision_Core GetCurrentDecision()
    {
        return currentDecision;
    }

    public List<Decision_Core> GetDecisions()
    {
        return decisions;
    }
}
