using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Document_Icon : MonoBehaviour
{
    [SerializeField] private GameObject description;
    [FormerlySerializedAs("pros")] [SerializeField] private GameObject approvalGains;
    [FormerlySerializedAs("cons")] [SerializeField] private GameObject denyGains;
    [SerializeField] private List<Decision> decisions = new ();
    [SerializeField] private List<DecisionListPreset> decisionsList = new();
    [SerializeField] private Decision currentDecision;
    [SerializeField] public int index;
    [SerializeField] private GameObject nextDayButton;
    [SerializeField] private List<GameObject> approvalobject = new();
    [SerializeField] private List<GameObject> denyobject = new();
    [SerializeField] private List<GameObject> BuildPowerplant = new ();
    [SerializeField] private List<GameObject> DemolishPowerplant = new();
    [SerializeField] private GameObject TextBuild;
    [SerializeField] private GameObject TextDemolish;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        decisions = decisionsList[0].GetDecisions();
        SetCurrentDecision(decisions[index]);
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
            denyGains.SetActive(true);
            IconShow(decision.GetApprovalCosts(),approvalobject);
            IconShow(decision.GetDisapprovalCosts(),denyobject);
            IconBuildShow(decision.GetTypeOfProject(),decision.GetPowerPlants());
        }
        nextDayButton.SetActive(AreDecisionsMade());
    }
    public void ChangePage(int value)
    {
        index = (index + value + decisions.Count) % (decisions.Count);
        SetCurrentDecision(decisions[index]);
        GetComponentInChildren<Stampable_icon>().showStamp(currentDecision.GetStampState());
    }

    public Decision GetCurrentDecision()
    {
        return currentDecision;
    }

    public List<Decision> GetDecisions()
    {
        return decisions;
    }

    public bool AreDecisionsMade()
    {
        return decisions.All(decision => decision.IsStamped());
    }

    public void loadDecisionList(int index)
    {
        if (index >= decisionsList.Count)
        {
            return;
        }
        decisions = decisionsList[index].GetDecisions();
        SetCurrentDecision(decisions[0]);
        GetComponentInChildren<Stampable_icon>().showStamp(currentDecision.GetStampState());
    }

    private void IconShow( List<int> ints, List<GameObject> icons)
    {   
        foreach(GameObject gameObject in icons)
        {
            gameObject.SetActive(false);
        }

        if(ints == null ){ return; }
       for(int i = 0; i < ints.Count; i++)
        {
            if(ints[i] != 0)
            {
                icons[i].SetActive(true);
                icons[i].GetComponentInChildren<TextMeshPro>().text = ints[i].ToString();
                if(ints[i] > 0)
                {
                    icons[i].GetComponentInChildren<TextMeshPro>().color = Color.green;
                }
                else
                {
                    icons[i].GetComponentInChildren<TextMeshPro>().color = Color.red;
                }
            }
        } 
    }

    private void IconBuildShow(Decision.TypeOfProject typeOfProject, List<PowerPlants_core> bulid)
    {
        TextBuild.SetActive(false);
        TextDemolish.SetActive(false);
        
        foreach(GameObject gameObj in BuildPowerplant)
        {
            gameObj.SetActive(false);
        }
        foreach(GameObject gameObj in DemolishPowerplant)
        {
            gameObj.SetActive(false);
        }
        if(typeOfProject == Decision.TypeOfProject.Build)
        {
            IconBuildShowCon(BuildPowerplant,bulid);
            TextBuild.SetActive(true);

        }
        else if(typeOfProject == Decision.TypeOfProject.Demolish)
        {
           IconBuildShowCon(DemolishPowerplant,bulid); 
           TextDemolish.SetActive(true);
        }
    }


    private void IconBuildShowCon(List<GameObject> gameObjects,List<PowerPlants_core> build)
    {
        int ppc = 0;
        int pps = 0;
        int ppw = 0;

        foreach(PowerPlants_core core in build)
        {
            if(core.Gettypeofpowerp() == type.coal)
            {
               ppc++; 
            }else if(core.Gettypeofpowerp() == type.solar)
            {
                pps++;
            }else if(core.Gettypeofpowerp() == type.wind)
            {
                ppw++;
            }
        
        }


        if(ppc > 0)
        {
            gameObjects[0].SetActive(true);
            gameObjects[0].GetComponentInChildren<TextMeshPro>().text = ppc.ToString();
        }else if(pps > 0)
        {
            gameObjects[1].SetActive(true);
            gameObjects[1].GetComponentInChildren<TextMeshPro>().text = pps.ToString();
        }else if(ppw > 0)
        {
            gameObjects[2].SetActive(true);
            gameObjects[2].GetComponentInChildren<TextMeshPro>().text = ppw.ToString();
        }
    }
}