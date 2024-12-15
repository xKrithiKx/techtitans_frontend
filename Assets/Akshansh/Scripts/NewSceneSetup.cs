using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSceneSetup : MonoBehaviour
{
    public GameObject[] Characters;
    public float[] DelaysForWalkCycle;
    public CharManager charManager;

    public List<Animator> Animator;
    public string[] Dialogs;

    [Header("Grandma")]
    public float DistanceCoveredByGrandma;
    public GameObject CameraWalk, CameraBlisters;
    public GameObject Blisters1, Blisters2;
    public bool animIsPlayed = false;

    [Header("Grandpa")]
    public Vector3 walkPositionStart;

    [Header("Rose")]
    public GameObject roseInhand;

    [Header("Skate Girl")]
    public Vector3 walkPositionStartSkateGirl;

    void Start()
    {
        foreach(GameObject go in Characters)
        {
            Animator animator = go.GetComponent<Animator>();
            Animator.Add(animator);
        }
        Characters[UI_Manager.Instance.SimulationIndex].SetActive(true);

        StartSceneWithoutGrandma();
    }
    private void Update()
    {
        if(UI_Manager.Instance.SimulationIndex == 0)
        {
            DistanceCoveredByGrandma = charManager.GetDistanceCovered();
            if (DistanceCoveredByGrandma >= 10f && !animIsPlayed)
            {
                Console.WriteLine("A");
                CameraWalk.SetActive(false);
                CameraBlisters.SetActive(true);
                Blisters1.SetActive(true);
                Blisters2.SetActive(true);
                StartCoroutine(ShowPopupAfterDelay());
                animIsPlayed = true;
            }
        }
    }
    void StartSceneWithoutGrandma()
    {
        if(UI_Manager.Instance.SimulationIndex != 0) //Other Characters
        {
            StartCoroutine(ShowPopupAfterDelay());
        }
        else
        {
            charManager.enabled = true;
        }
    }
    IEnumerator ShowPopupAfterDelay()
    {
        yield return new WaitForSeconds(DelaysForWalkCycle[UI_Manager.Instance.SimulationIndex]);
        ShowPopup();
    }
    public void ShowPopup()
    {
        UI_Manager.Instance.SetThought(true, Dialogs[UI_Manager.Instance.SimulationIndex]);
        UI_Manager.Instance.ContinueToWalkAnimButton.onClick.AddListener(() => charManager.enabled = true);
        UI_Manager.Instance.ContinueToWalkAnimButton.onClick.AddListener(() => UI_Manager.Instance.SetThought(false, ""));
        UI_Manager.Instance.ContinueToWalkAnimButton.onClick.AddListener(() => ContinueButtonLeadToWalkCycle());
    }

    public void ContinueButtonLeadToWalkCycle()
    {
        if(UI_Manager.Instance.SimulationIndex == 6)
        {
            GrandpaLocationAfterAnimation();
        }
        if (UI_Manager.Instance.SimulationIndex == 3)
        {
            SkateGirlPosition();
        }
        if (UI_Manager.Instance.SimulationIndex == 0)
        {
            CameraWalk.SetActive(true);
            CameraBlisters.SetActive(false);
        }
        Animator[UI_Manager.Instance.SimulationIndex].SetInteger("Anim",1);
        //roseInhand.SetActive(false);
    }

    public void GrandpaLocationAfterAnimation()
    {
        Characters[6].transform.position = walkPositionStart;
    }
    public void SkateGirlPosition()
    {
        Characters[3].transform.position = walkPositionStartSkateGirl;
    }
}