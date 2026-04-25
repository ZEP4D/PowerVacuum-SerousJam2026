using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Country_core", menuName = "Scriptable Objects/Country_core")]
public class Country_core : ScriptableObject
{
    [SerializeField] private string Country_name;
    [SerializeField] private string ISO_Code;
    [SerializeField] private int relation; 
    [SerializeField] private int Liked;

    [SerializeField] private List<type> resourcesexport;

    public List<type> GetResourceExports() {
        return this.resourcesexport;
    }
}
