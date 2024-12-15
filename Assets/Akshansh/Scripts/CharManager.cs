using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharManager : MonoBehaviour
{
    public GameObject Character, MainCamera, AnimationCamera;
    public Animator Animator;
    public Collider Collider;
    public Rigidbody rb;
    public float Speed = 5.0f;
    public float RotationSpeed = 700.0f;
    public NewSceneSetup setup;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 cameraOffset;
    private Vector3 initialPosition; // To store initial position
    private float DistanceWalked = 0;
    public Animator GrandmaStick;

    void Start()
    {
        Character = setup.Characters[UI_Manager.Instance.SimulationIndex];
        Animator = Character.GetComponent<Animator>();
        Collider = Character.GetComponent<Collider>();
        rb = Character.GetComponent<Rigidbody>();
        MainCamera = Character.transform.Find("Camera").gameObject;
        AnimationCamera = Character.transform.Find("Animation_Cam").gameObject;
        if (Character.transform.Find("Props"))
        {
            Character.transform.Find("Props").gameObject.SetActive(false);
        }

        MainCamera.SetActive(true);
        AnimationCamera.SetActive(false);
        cameraOffset = MainCamera.transform.position - Character.transform.position;

        initialPosition = Character.transform.position; // Initialize initial position
    }

    void Update()
    {
        InputHandler();
    }

    void FixedUpdate()
    {
        Move();
    }

    void LateUpdate()
    {
        //CamController();
    }

    void InputHandler()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        /*Vector3 forward = new Vector3(Character.transform.forward.x, 0, Character.transform.forward.z);
        Vector3 right = new Vector3(Character.transform.right.x, 0, Character.transform.right.z);*/
        if (moveVertical < 0) moveVertical = 0;
        Vector3 forward = Character.transform.forward;
        Vector3 right = Character.transform.right;

        moveDirection = (forward * moveVertical + right * moveHorizontal).normalized;

        if (moveDirection != Vector3.zero)
        {
            Animator.SetInteger("Anim", 1);
            GrandmaStick.SetInteger("Anim", 1);
        }
        else
        {
            Animator.SetInteger("Anim", 2);
            GrandmaStick.SetInteger("Anim", 2);
        }
    }

    void Move()
    {
        if (moveDirection != Vector3.zero)
        {
            Vector3 newPosition = Character.transform.position + moveDirection * Speed * Time.deltaTime;
            Character.transform.position = newPosition;
            Character.transform.rotation = Quaternion.Slerp(Character.transform.rotation, Quaternion.LookRotation(moveDirection), RotationSpeed * Time.deltaTime);
            DistanceWalked += Vector3.Distance(initialPosition, newPosition);

            initialPosition = newPosition;
            // Calculate the distance covered and update the DistanceCoveredByGrandma variable
            if (UI_Manager.Instance.SimulationIndex == 0) // Only for Grandma
            {
                setup.DistanceCoveredByGrandma = (int)Vector3.Distance(initialPosition, Character.transform.position);
            }
        }
    }

    public float GetDistanceCovered()
    {
        return DistanceWalked;
    }

    void CamController()
    {
        Vector3 desiredPosition = Character.transform.position + cameraOffset;

        MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, desiredPosition, Time.deltaTime * Speed);

        MainCamera.transform.LookAt(Character.transform.position + Vector3.up * 1.5f);
    }
}