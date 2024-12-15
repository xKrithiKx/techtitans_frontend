using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CharacterController
    : MonoBehaviour
{
    public enum CharacterType { Granny,Others}
    public CharacterType CurtType;
    public bool CanMove;

    [SerializeField] Transform PopupPos;
    [SerializeField] Animator anim;
    [SerializeField] string InterriorName;
    [SerializeField] float walkSpeed = 1f, camRotSpeed = 2f, rotSpeed = 1f;
    public Transform PlayerCam, CamPivot,PlayerBody;
    [SerializeField]
    string[] PopupTexts;

    [SerializeField] float GrannyWalkTime = 10f,grannyExtiriorDelay =4f;
    float curtWalkTime = 0;
    bool checkWalkTime =true,dailogueVisible=false,animDone;
    UI_Manager uiMang;

    Vector3 moveDir;

    private void Start()
    {
        anim = anim ? anim : GetComponent<Animator>();
        uiMang = UI_Manager.Instance;
        if(CurtType!=CharacterType.Granny)
        {
            animDone = true;
        }
    }

    private void Update()
    {
        if (CanMove)
        {
            InputManger();
        }
        else
        {
            if (dailogueVisible)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    CanMove = true;
                    dailogueVisible = false;
                    uiMang.SetThought(false, "");
                    animDone = true;
                }
            }
        }
        if (CurtType == CharacterType.Granny&&checkWalkTime)
        {
            if (curtWalkTime > GrannyWalkTime)
            {
                CanMove = false;
                checkWalkTime = false;
                StartCoroutine(StartAnimActions(grannyExtiriorDelay));
            }
        }
    }
    private void FixedUpdate()
    {
        if (CanMove)
        {
            MovementHandler();
        }
    }
    IEnumerator StartAnimActions(float _delay)
    {
        switch (CurtType)
        {
            case CharacterType.Granny:
                anim.SetTrigger("S1");
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(_delay);
        switch(CurtType)
        {
            case CharacterType.Granny:
                uiMang.PopupPos = PopupPos.position;
                uiMang.SetThought(true, PopupTexts[0]);
                dailogueVisible = true;
                anim.SetBool("IsWalking", false);
                break;
            default:
                break;
        }
    }
    void MovementHandler()
    {
        if (moveDir != Vector3.zero)
        {
            anim.SetBool("IsWalking", true);
            transform.position += Time.deltaTime * walkSpeed * moveDir;

            //rotate player in moving dir
            var _temp = Quaternion.Slerp(Quaternion.Euler(0, PlayerBody.transform.eulerAngles.y,0), Quaternion.LookRotation(moveDir),
                rotSpeed * Time.deltaTime);
            PlayerBody.rotation = _temp;
            curtWalkTime += Time.deltaTime;
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }
    void InputManger()
    {
        //movement inputs
        moveDir = PlayerCam.right * Input.GetAxis("Horizontal") + PlayerCam.forward * Input.GetAxis("Vertical");
        moveDir.y = 0;
        moveDir = moveDir.normalized;

        if(Input.GetAxis("Horizontal")!=0)
        {
            CamPivot.eulerAngles += Time.deltaTime * new Vector3(0, Input.GetAxis("Horizontal") * camRotSpeed, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name =="House"&&animDone)
        {
            SceneManager.LoadScene(InterriorName);
        }
    }
}
