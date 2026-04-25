using TMPro;
using UnityEngine;

public class showResource : MonoBehaviour
{
    public TextMeshProUGUI text;
    [SerializeField] ResourcesSystem.ResourceType resourceType;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = ResourcesSystem.instance.GetResources()[resourceType].ToString();
    }
}
