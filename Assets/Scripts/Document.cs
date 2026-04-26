using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Document : MonoBehaviour
{
    [SerializeField] private GameObject description;
    [FormerlySerializedAs("pros")] [SerializeField] private GameObject approvalGains;
    [FormerlySerializedAs("cons")] [SerializeField] private GameObject denyGains;
    [SerializeField] private List<Decision> decisions = new ();
    [SerializeField, CanBeNull] private Decision currentDecision;
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

    public void SetCurrentDecision(Decision decision)
    {
        currentDecision = decision;
        if (decision == null)
        {
            description.SetActive(false);
            approvalGains.SetActive(false);
            denyGains.SetActive(false);
        }
        else
        {
            description.SetActive(true);
            description.GetComponent<TextMeshPro>().text = decision.GetDescription();
            approvalGains.SetActive(true);
            approvalGains.GetComponent<TextMeshPro>().text = decision.GetPros();
            denyGains.SetActive(true);
            denyGains.GetComponent<TextMeshPro>().text = decision.GetCons();
        }
    }
    public void ChangePage(int value)
    {
        index = (index + value + decisions.Count + 1) % (decisions.Count + 1);
        SetCurrentDecision(index > 0 ? decisions[index - 1] : null);
        if (currentDecision == null)
        {
            GetComponentInChildren<Stampable>().showStamp(Decision.StampState.None);
        }
        else
        {
            GetComponentInChildren<Stampable>().showStamp(currentDecision.GetStampState());
        }
            
    }

    public Decision GetCurrentDecision()
    {
        return currentDecision;
    }

    public List<Decision> GetDecisions()
    {
        return decisions;
    }
}
