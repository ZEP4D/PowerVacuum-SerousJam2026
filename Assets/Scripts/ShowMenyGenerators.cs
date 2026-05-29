using TMPro;
using UnityEngine;

public class ShowMenyGenerators : MonoBehaviour
{
   [SerializeField] private GameObject colepower;
   [SerializeField] private GameObject solarpower;
   [SerializeField] private GameObject windpower;

    void Update()
    {
            int ppc = 0;
            int pps = 0;
            int ppw = 0;


        foreach(PowerPlants_core core in ResourcesSystem.instance.GetPowerPlants_s())
        {
            switch(core.Gettypeofpowerp())
            {
                case type.coal:
                    ppc += 1;
                    break;
                case type.solar:
                    pps +=1;
                    break;
                case type.wind:
                    ppw+=1;
                    break;
                default:
                break;
            }
        }


        colepower.GetComponentInChildren<TextMeshProUGUI>().text = ppc.ToString();        
        solarpower.GetComponentInChildren<TextMeshProUGUI>().text = pps.ToString();
        windpower.GetComponentInChildren<TextMeshProUGUI>().text = ppw.ToString(); 
    }
}