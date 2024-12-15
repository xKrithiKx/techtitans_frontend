using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPController : MonoBehaviour
{
    public bool CanMove = true,CanLook =true;
    [SerializeField] FppSCO ControllerValues;
    [SerializeField] Transform Cam;

    Vector2 mouseLook = Vector2.zero;

    private void Update()
    {
        if(CanMove)
        {
            MovementManager();
        }
    }

    void MovementManager()
    {
        //move
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0,
            Input.GetAxis("Vertical")) * ControllerValues.MoveSpeed*Time.deltaTime);

        //look
        mouseLook.x += -Input.GetAxis("Mouse Y");
        mouseLook.y += Input.GetAxis("Mouse X");
        mouseLook *= ControllerValues.MouseSenstivity;
        mouseLook.x = Mathf.Clamp(mouseLook.x, ControllerValues.MinClamp, ControllerValues.MaxClamp);
        transform.eulerAngles =new Vector3(0,mouseLook.y,0);
        Cam.transform.eulerAngles = new Vector3(mouseLook.x, mouseLook.y, 0);
    }
}
