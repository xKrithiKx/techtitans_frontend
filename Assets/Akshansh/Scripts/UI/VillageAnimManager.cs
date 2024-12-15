using UnityEngine;
using System.Collections;
using TMPro;

public class PopupManager : MonoBehaviour
{
    public GameObject[] AvailablePopups;
    GameObject activePopup;
    public void ShowHidingPopup(int _index, float _duration, string _text)
    {
        activePopup = GetPopup(_index);
        activePopup.GetComponent<Animator>().SetBool("Entry",true);
        activePopup.GetComponent<TextMeshPro>().text = _text;
        StartCoroutine(HidePopup(_duration,activePopup));
    }
    IEnumerator HidePopup(float _delay,GameObject _obj)
    {
        yield return new WaitForSeconds(_delay);
        _obj.GetComponent<Animator>().SetBool("Entry", false);
    }
    GameObject GetPopup(int _index)
    {
        if (AvailablePopups.Length >= _index || _index < 0)
        {
            return null;
        }
        else
        {
            return AvailablePopups[_index];
        }
    }
}
