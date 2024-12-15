using UnityEngine;

public class BetadineBottles : MonoBehaviour
{
    [SerializeField] int BottleIndex;
    private void OnMouseDown()
    {
            FindObjectOfType<InterriorManager>().SelectedBottle(BottleIndex);

    }
}
