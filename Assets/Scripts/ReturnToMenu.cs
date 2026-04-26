using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string targetSceneName;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Returning");

        SceneManager.LoadScene(
            targetSceneName
        );
    }
}
