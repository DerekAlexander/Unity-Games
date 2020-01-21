using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHouse : MonoBehaviour
{
    private Animator anim;
    private Camera cam;
    private Vector3 moveDir;

    private float horizontal;
    private float vertical;
    private bool jumpPressed;
    public float jumpForce = 5f;
    private bool canGetInput = true;

    // Start is called before the first frame update
    void Start ()
    {
        cam = Camera.main;
        anim = GetComponent<Animator>();

    }
    void Update ()
    {
        if ( canGetInput )
            GetInput();
        else
        {
            anim.SetFloat("Vertical", 0);
            anim.SetFloat("Horizontal", 0);
        }
    }

    public void InputState ( bool b )
    {
        canGetInput = b;
    }

    void FixedUpdate()
    {
        RootMotion();   
    }

    private void RootMotion()
    {
        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Horizontal", horizontal);
    }


    private void GetInput ()
    {

        vertical = Input.GetAxis("Vertical");

        horizontal = Input.GetAxis("Horizontal");
    }
}
