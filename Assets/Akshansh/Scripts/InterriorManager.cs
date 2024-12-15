using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InterriorManager : MonoBehaviour
{
    [SerializeField] GameObject[] characters;
    [SerializeField] GameObject[] InterriorScenes;
    [SerializeField] GameObject granny1, granny2,boi2,gurl2;
    [SerializeField] int[] CorrectBottleIndex;
    [SerializeField] float[] EndDurations;
    [SerializeField] Animator[] animators;
    public GameObject[] interiorPrefabs;
    public GameObject grandFather; 
    UI_Manager mang;
    bool selected;

    void Start()
    {
        mang = UI_Manager.Instance;
        mang.SetInt(true, "<size=40>Select the Betadine medicine which you think is suitable</size>");
        mang.medicineContinue.onClick.AddListener(EndGame);
        interiorPrefabs[UI_Manager.IneteriorIndex].SetActive(true);
        if (mang.SimulationIndex >= InterriorScenes.Length)
            return;
        InterriorScenes[mang.SimulationIndex].SetActive(true);
    }

    public void SelectedBottle(int _index)
    {
        if (selected)
            return;
        if (_index == CorrectBottleIndex[mang.SimulationIndex])
        {
            Debug.Log(_index);
            Debug.Log(CorrectBottleIndex[mang.SimulationIndex]);
            Debug.Log(mang.SimulationIndex);    
            CorrectSelected();
            //
            //StartCoroutine(EndGame());

            selected = true;
        }
        else
        {
            IncorrectSelect();
        }
    }
    void IncorrectSelect()
    {
        mang.medicineContinue.gameObject.SetActive(false);
        mang.SetInt(true, "<b>Sem dúvida a so" +
            "lução cutânea pode ser uma boa opção, mas a pomada é a opção mais indicada para este tipo de ferida, visto que é necessário colocar um penso a cobrir a ferida.</b> \nPs.: primeiro que tudo, limpa a tua ferida com Betadine Espuma Cutânea 125ml");
    }

    void CorrectSelected()
    {
        granny1.SetActive(false);

        characters[mang.SimulationIndex].SetActive(true);

        StartCoroutine(HappyAnimation());
        
        //animators[0].SetInteger("EndIndex", UI_Manager.Instance.SimulationIndex);
        //animators[0].SetTrigger("End");
    }
    IEnumerator HappyAnimation()
    {
        animators[0].SetInteger("EndIndex", 1);
        yield return new WaitForSeconds(0.4f);
        characters[UI_Manager.Instance.SimulationIndex].GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        mang.medicineContinue.gameObject.SetActive(true);
        mang.SetInt(true, "<b>Boa!!</b> \n<b>Sem dúvida que a pomada é a opção mais indicada para este tipo de ferida. Mas não te esqueças de colocar um penso a cobrir a ferida!</b> \nPs.: primeiro que tudo, limpa a tua ferida com Betadine Espuma Cutânea 125ml");
    }

    public void EndGame()
    {
        //yield return new WaitForSeconds(EndDurations[mang.SimulationIndex]);
        mang.LoadHome();
        mang.HideUI();
        Debug.Log("Calling");
    }
}