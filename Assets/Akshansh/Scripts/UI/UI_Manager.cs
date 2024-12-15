using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public GameObject secondScene;
    public Button medicineContinue, SecondscreenBut, ContinueToWalkAnimButton;
    public static UI_Manager Instance;
    [SerializeField] GameObject[] AvailableSelections;
    [SerializeField] GameObject SelectionPanel,ThoughtPanel,intPanel;
    [SerializeField] TMP_Text thoughtText,intTxt;
    public int SimulationIndex;
    [SerializeField] string SimulationSceneName,HomeName;
    [SerializeField] AudioSource bgAud;
    public Vector3 PopupPos;

    public static int IneteriorIndex;

    private void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
   
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Screen.fullScreen = true;
        }
        if (PopupPos != null)
        {
            if (Camera.main == null)
                return;
            ThoughtPanel.transform.position = Camera.main.WorldToScreenPoint(PopupPos);
        }
    }
    public void SetSelection(int _index)
    {
        SelectionPanel.SetActive(true);
        AvailableSelections[_index].SetActive(true);
    }
    public void SetSimulation(int _index)
    {
        SimulationIndex = _index;
        SceneManager.LoadSceneAsync(SimulationSceneName);
        HideUI();
    }

    public void HideUI()
    {
        foreach (var v in AvailableSelections)
        {
            v.SetActive(false);
        }
        SelectionPanel.SetActive(false);
        ThoughtPanel.SetActive(false);
        intPanel.SetActive(false);
    }
    public void LoadScene(string _name)
    {
        SceneManager.LoadScene(_name);
        HideUI();
    }
    public void LoadHome()
    {
        SceneManager.LoadScene(HomeName);
        HideUI();
        secondScene.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        HideUI();
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            secondScene.SetActive(true);
        }
    }

    public void SetThought(bool _State, string val)
    {

        ThoughtPanel.SetActive(_State);
        thoughtText.text = val;
        RectTransform rectTransform = ThoughtPanel.GetComponent<RectTransform>();

        // Set the anchor to middle center
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);

        // Set the pivot to middle center
        rectTransform.pivot = new Vector2(0.5f, 0.5f);

        // Set the position to be in the middle of the screen
        rectTransform.anchoredPosition = Vector2.zero;
    }
    public void SetInt(bool _State, string val)
    {

        intPanel.SetActive(_State);
        intTxt.text = val;

    }
    public void ToggleMute()
    {
        bgAud.mute = !bgAud.mute;
    }
    
}
