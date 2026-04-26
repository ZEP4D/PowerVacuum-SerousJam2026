using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class showResource : MonoBehaviour
{
    public TextMeshProUGUI text;
    [SerializeField] ResourcesSystem.ResourceType resourceType;
    [SerializeField] Image image;
    [SerializeField] private bool isReveresed = false;
    [SerializeField] private int upperThreshold = 70;
    [SerializeField] private int lowerThreshold = 30;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        image = GetComponentInChildren<Image>();
    }

    void Update()
    {
        var value = ResourcesSystem.instance.GetResources()[resourceType];
        text.text = value.ToString();
        if (isReveresed)
        {
            if (value > upperThreshold)
            {
                image.color = Color.red;
            } 
            else if (value > lowerThreshold)
            {
                image.color = Color.yellow;
            }
            else
            {
                image.color = Color.green;
            }
        }
        else
        {
            if (value > upperThreshold)
            {
                image.color = Color.green;
            }
            else if (value > lowerThreshold)
            {
                image.color = Color.yellow;
            }
            else
            {
                image.color = Color.red;
            }
        }
    }
}
