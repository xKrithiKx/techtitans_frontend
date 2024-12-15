using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneSetup : MonoBehaviour
{
    bool showedPopup;
    public GameObject[] scenes,Characters;
    [SerializeField] Transform[] PopupPos;
    public CharacterController[] CharacterConts;
    [SerializeField]
    string[] popups;
    [SerializeField] CameraPosController[] cameraCont;
    [SerializeField] string SceneName;
    [SerializeField] float[] delays;
    bool CanLoad = false;
    private void Start()
    {
        scenes[UI_Manager.Instance.SimulationIndex].SetActive(true);
        if (UI_Manager.Instance.SimulationIndex == 0)
            return;
        StartCoroutine(Load());
    }
    private void Update()
    {
        if(showedPopup && !CanLoad)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject(-1))
                    return;
                cameraCont[UI_Manager.Instance.SimulationIndex].Init();
                showedPopup = false;
                UI_Manager.Instance.SetThought(false, "");
            }
        }
        if(CanLoad)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject(-1))
                    return;
                UI_Manager.Instance.SetThought(false, "bruh");
                SceneManager.LoadScene(SceneName);
            }
        }
    }
    IEnumerator Load()
    {
        //yield return new WaitForSeconds(delays[UI_Manager.Instance.SimulationIndex]);
        if (UI_Manager.Instance.SimulationIndex != 4)
        {
            if (UI_Manager.Instance.SimulationIndex == 6)
            {
                ShowPopup(popups[6]);
                UI_Manager.Instance.PopupPos = PopupPos[6].position;
                CanLoad = true;
                yield return null;
            }

            //normal
            ShowPopup(popups[UI_Manager.Instance.SimulationIndex]);
        }
        else
        {
            UI_Manager.Instance.SetThought(true, popups[4]);
            UI_Manager.Instance.PopupPos = PopupPos[4].position;
            CanLoad = true;
        }
    }
    public void ShowPopup(string _txt)
    {
        UI_Manager.Instance.PopupPos = PopupPos[UI_Manager.Instance.SimulationIndex].position;
        UI_Manager.Instance.SetThought(true,_txt);
        showedPopup = true;
    }
    public void SetCharacter()
    {
        Characters[UI_Manager.Instance.SimulationIndex].SetActive(true);
    }
}
