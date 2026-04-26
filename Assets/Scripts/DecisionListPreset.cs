using System.Collections.Generic;
using UnityEngine;

public class DecisionListPreset : MonoBehaviour
{
    [SerializeField] List<Decision> decisions;

    public List<Decision> GetDecisions()
    {
        return decisions;
    } 
}
