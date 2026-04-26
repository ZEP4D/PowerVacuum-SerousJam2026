using UnityEngine;

[CreateAssetMenu(fileName = "Country_core", menuName = "Scriptable Objects/Country_core")]
public class Country_core : ScriptableObject
{
    [SerializeField] private string Country_name;
    [SerializeField] private string ISO_Code;
    [SerializeField] private int relation; 
    [SerializeField] private int Liked;


    public string GetCountry()
    {
        return Country_name;
    }

    public string GetISO()
    {
        return ISO_Code;
    }
    public int getrelation()
    {
        return relation;
    }

    public int getliked()
    {
        return Liked;
    }
}
