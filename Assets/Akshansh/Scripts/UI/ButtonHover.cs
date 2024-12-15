using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text targetText;
    [SerializeField ] private Button hoverButton;
    [SerializeField] Color HoverButtCol, HovertextCol;
    [SerializeField] bool Ispause = false;

    Color origbuttCol, origTextCol;
    private void Start()
    {
        origbuttCol = hoverButton.image.color;
        origTextCol = targetText.color;
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (s, l) =>
            {
                ResetButtcol();
            };
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        targetText.color = HovertextCol;
        hoverButton.image.color = HoverButtCol;
        if (Ispause) 
        targetText.text = "Play";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetButtcol();
    }

    private void ResetButtcol()
    {
        targetText.color = origTextCol;
        hoverButton.image.color = origbuttCol;
        if (Ispause)

            targetText.text = "Continue";
    }
    public void PauseGame(int val)
    {
        Time.timeScale = val;
    }
    
}
