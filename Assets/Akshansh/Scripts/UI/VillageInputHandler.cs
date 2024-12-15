using System.Collections;
using UnityEngine;

public class VillageInputHandler : MonoBehaviour
{
    [SerializeField] int villageIndex;
    [SerializeField] float panelDelay = 1.5f;
    [SerializeField] GameObject[] houseTouchColliders;
    private void Start()
    {

    }
    private void OnMouseDown()
    {
        StartCoroutine(OnVillageTapped(villageIndex));
    }
    IEnumerator OnVillageTapped(int _index)
    {
        TurningOffColiders();
        var _TEmp = Camera.main.GetComponent<Animator>();
        _TEmp.SetTrigger("Zoom");
        _TEmp.SetInteger("House", _index);
        yield return new WaitForSeconds(panelDelay);
        UI_Manager.Instance.SetSelection(_index);
    }

    void TurningOffColiders()
    {
        if(villageIndex == 0)
        {
            houseTouchColliders[0].SetActive(true);
            houseTouchColliders[1].SetActive(false);
            houseTouchColliders[2].SetActive(false);
        }
        if (villageIndex == 1)
        {
            houseTouchColliders[1].SetActive(true);
            houseTouchColliders[0].SetActive(false);
            houseTouchColliders[2].SetActive(false);
        }
        if (villageIndex == 2)
        {
            houseTouchColliders[2].SetActive(true);
            houseTouchColliders[0].SetActive(false);
            houseTouchColliders[1].SetActive(false);
        }
    }
}
