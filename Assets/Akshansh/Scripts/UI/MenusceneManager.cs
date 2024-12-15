using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenusceneManager : MonoBehaviour
{
    [SerializeField] Animator cameraAnim;
    [SerializeField] GameObject colliders;

    private void Start()
    {
        UI_Manager.Instance.SecondscreenBut.onClick.AddListener(Play);
    }

    public void Play()
    {
        cameraAnim.enabled = true;
        colliders.SetActive(true);
    }
}
