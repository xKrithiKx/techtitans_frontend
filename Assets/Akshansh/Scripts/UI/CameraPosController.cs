using AkshanshKanojia.Controllers.ObjectManager;
using UnityEngine;

public class CameraPosController : MonoBehaviour
{
    ObjectController objCont;
    [SerializeField] string PopupText;
    [SerializeField] float trackPos = 3f,RotSpeed = 5f;
    [SerializeField] bool isLocal = false;
    [SerializeField] Animator anim;
    SceneSetup setup;

    bool pos, rot;
    public void Init()
    {
        anim.enabled = false;
        setup = FindObjectOfType<SceneSetup>();
        var _Temp = setup.CharacterConts[UI_Manager.Instance.SimulationIndex].PlayerCam;
        objCont = FindObjectOfType<ObjectController>();
        objCont.AddEvent(gameObject, _Temp.rotation, RotSpeed, isLocal,ObjectController.AvailableUpdateModes.Fixed);
        objCont.AddEvent(gameObject, _Temp.position, RotSpeed, isLocal, ObjectController.AvailableUpdateModes.Fixed);
        objCont.OnMovementEnd += (obj) => { 
            pos = true;
            if (pos && rot)
            {
                ActivateChar();
            }
        };
        objCont.OnRotationEnd += (obj) => {
            rot = true;
            if (pos && rot)
            {
                ActivateChar();
            }
        };
    }

    void ActivateChar()
    {
        transform.parent.gameObject.SetActive(false);
        setup.SetCharacter();
    }
}
