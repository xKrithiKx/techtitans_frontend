using UnityEngine.SceneManagement;
using UnityEngine;

public class CheckColliderTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        HandleCollisionOrTrigger(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleCollisionOrTrigger(collision.collider);
    }

    private void HandleCollisionOrTrigger(Collider collider)
    {
        if (collider.CompareTag("LeftHouse"))
        {
            UI_Manager.IneteriorIndex = 0;
            SceneManager.LoadScene("InterriorTest");
        }
        else if (collider.CompareTag("BackLeftHouse"))
        {
            UI_Manager.IneteriorIndex = 1;
            SceneManager.LoadScene("InterriorTest");
        }
        else if (collider.CompareTag("BackRightHouse"))
        {
            UI_Manager.IneteriorIndex = 2;
            SceneManager.LoadScene("InterriorTest");
        }
    }
}
