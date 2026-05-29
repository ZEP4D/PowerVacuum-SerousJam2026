using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hoverinformation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   [SerializeField] private GameObject paneltoShow;
    public void OnPointerEnter(PointerEventData eventData)
    {
        paneltoShow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        paneltoShow.SetActive(false);
    }
}