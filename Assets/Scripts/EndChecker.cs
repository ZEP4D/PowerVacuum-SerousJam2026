using UnityEngine;

public class EndChecker : MonoBehaviour
{
    [SerializeField] private int howmanyturn;
    private bool endisactive;


    void Start()
    {
        endisactive = false;
    }

    public void CheckForWinCondition()
    {
        if (endisactive)
        {
            return;
        }
        if(ResourcesSystem.instance.getApproval() < 0)
        {
            YouLose();
            return;
        }
        else if (ResourcesSystem.instance.getbudget() < 0)
        {
            YouLose();
            return;
        }
        else if (ResourcesSystem.instance.getclimate() >= 100)
        {
            YouLose();
            return;
        }
        else if (ResourcesSystem.instance.getnumbersofturn() >= howmanyturn)
        {
            if (ResourcesSystem.instance.getApproval() < 50)
            {
                YouLose();
                return;
            }
            else
            {
                Youwin();
                return;
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
