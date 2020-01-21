using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private CharacterController cc;
    private Animator anim;
    private Camera cam;
    private Vector3 moveDir;

    private float horizontal;
    private float vertical;
    private bool jumpPressed;
    public float jumpForce = 5f;

    [SerializeField] GameObject groundCheck;

    private Vector3 myForward;

    private bool isJumping = false;


    // Use this for initialization
    void Start ()
    {
        cam = Camera.main;
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update ()
    {
        GetInput();

    }

    private void FixedUpdate ()
    {

        RootMotion();


        if (jumpPressed)
        {
            jumpPressed = false;
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }

    private void RootMotion()
    {
        if(horizontal != 0  || vertical != 0)
        {
            transform.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y, 0);
        }
        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Horizontal", horizontal);

    }


    public void ReapplyRootMotion ( )
    {
        anim.applyRootMotion = true;
    }


    IEnumerator InAir ( )
    {
        while ( !CheckIsGrounded() )
        {
            yield return null;
        }
        anim.SetBool("BackToBlendTree", true);
        yield return null;
        yield return null;
        anim.SetBool("BackToBlendTree", false);
        ReapplyRootMotion();
        yield return new WaitForSeconds(.25f);
        isJumping = false;
    }

    public void SrartInAir ( )
    {
        StartCoroutine(InAir());
    }

    private void GetInput ()
    {

        vertical = Input.GetAxis("Vertical");

        horizontal = Input.GetAxis("Horizontal");


        if ( !isJumping && Input.GetKeyDown(KeyCode.Space) && CheckIsGrounded())
        {
            isJumping = true;
            jumpPressed = true;
            anim.Play("Jump");
            anim.applyRootMotion = false;
            
        }

        if ( Sprinting() )
        {
            vertical *= 2;
        }
        else
        {
            vertical *= 1;
        }

        if ( this.GetComponent<KeyBindings>().isStopped )
        {
            horizontal = 0;
            vertical = 0;
        }
    }

    private bool CheckIsGrounded ( )
    {


        if( GetComponentInChildren<Toy>() )
        {
            return false;
        }

        Collider[] hitColliders = Physics.OverlapSphere(groundCheck.transform.position  , .5f);


        if ( SceneController.ActiveSceneName() == "TestLayout" &&
             GameObject.Find("CreatureStatSheet") ||
             GameObject.Find("InventoryPanel") ||
             GameObject.Find("ShopView")  || 
             TutorialManager.isDisplaying )
            return false;

        for ( int i = 0; i < hitColliders.Length; i++ )
        {
            if ( hitColliders[i].tag == "NotGround" ||
                 hitColliders[i].tag == "Player" || 
                 hitColliders[i].tag == "Audio" || 
                 hitColliders[i].tag == "Blobisaur" || 
                 hitColliders[i].tag == "Tutorial" )
            {
                continue;
            }
            else
            {
                return true;
            }
        }


        return false;
    }


    private bool Sprinting ()
    {
        if ( Input.GetKey(KeyCode.LeftShift) )
        {
            return true;
        }
        else
            return false;
    }


    public void PlayAnimation ( string animName )
    {
        anim.Play(animName);
    }

    public void SetRelativeForward ( )
    {
        myForward = transform.TransformDirection(Vector3.forward);
    }

    public void ThrowStick ( )
    {
        GetComponentInChildren<Toy>().ThrowToy(myForward);
        anim.SetBool("HoldingStick", false);
        //FindObjectOfType<Toy>().ThrowToy(myForward);
        
    }

}
