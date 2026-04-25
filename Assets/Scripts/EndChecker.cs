using UnityEngine;

public class EndChecker : MonoBehaviour
{
    [SerializeField] private int howmanyturn;
    private bool endisactive;


    void Start()
    {
        endisactive = false;
    }


    void Update()
    {


        if (!endisactive)
        {
            if(ResourcesSystem.instance.getApproval() < 0)
            {
                Invoke(nameof(YouLose),0);    
            }
            else if(ResourcesSystem.instance.getbudget() < 0)
            { 
                Invoke(nameof(YouLose),0);
            }
            else if(ResourcesSystem.instance.getclimate() == 100)
            {
                Invoke(nameof(YouLose),0);
            }

            else if(ResourcesSystem.instance.getnumbersofturn() > howmanyturn)
            {
                if(ResourcesSystem.instance.getApproval() < 50)
                {
                    Invoke(nameof(YouLose),0);
                }
                else
                {
                    Invoke(nameof(Youwin),0);
                }
            }
        }
       
    }


    void YouLose()
    {   
        Debug.Log("Lost");
        Debug.Log("hehehehehe");
        endisactive = true;
    }

    void Youwin()
    {
        Debug.Log("Win");
        Debug.Log("Sad face,Sad Face");
        endisactive = true;
    }







}
