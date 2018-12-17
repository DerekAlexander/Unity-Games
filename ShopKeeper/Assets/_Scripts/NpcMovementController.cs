using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NpcMovementController : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    public Transform target;
    public Transform originalTarget;
    public LayerMask myLayerMask;
    private Animator myAnimator;

    private GameObject patrolTo;

    public int speed;
    private int moveDirection;
    public bool isWalking;
    private bool isWandering;
    public bool hasPatroled;

    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();


        patrolTo = GameObject.Find("PatrolPoint");
        hasPatroled = false;
        waitCounter = waitTime;
        walkCounter = walkTime;

        isWandering = false;

        isWalking = true;

    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        Animation();
    }

    public void Animation()
    {
        if (myRigidBody.velocity.y > 0)
        {
            myAnimator.SetInteger("Direction", 2);
        }
        else if (myRigidBody.velocity.y < 0)
        {
            myAnimator.SetInteger("Direction", 0);
        }
        else if (myRigidBody.velocity.x < 0)
        {
            myAnimator.SetInteger("Direction", 1);
        }
        else if (myRigidBody.velocity.x > 0)
        {
            myAnimator.SetInteger("Direction", 3);
        }
        else
        {
            myAnimator.SetInteger("Direction", -1);
        }
    }

    public void Movement()
    {

        if (isWalking)
        {

            walkCounter -= Time.deltaTime;

            if (walkCounter < 0)
            {

                isWalking = false;
                waitCounter = waitTime;
                moveDirection = Random.Range(0, 4);
                isWandering = false;

                if (Random.value > 0.5f)
                {
                    
                    //isWandering = true; 
                }
            }

            //switch (RayCast())
            //{
            //    //hit up 
            //    case 0:

            //        MoveDown();

            //        break;

            //    //hit right
            //    case 1:

            //        MoveLeft();

            //        break;

            //    //hit down
            //    case 2:

            //        MoveUp();

            //        break;

            //    //hit left
            //    case 3:

            //        MoveRight();

            //        break;

            //}

            if(hasPatroled == false)
            {
                originalTarget = target;
                target = patrolTo.transform;
                MoveToTarget();
            }
            else if (isWandering)
            {
                Wandering();
            }
            else if (target == null)
            {
                Wandering();
            }
            else
            {
                MoveToTarget();
            }
        }

        else
        {
            waitCounter -= Time.deltaTime;
            myRigidBody.velocity = Vector2.zero;

            if (waitCounter < 0)
            {
                isWalking = true;
                walkCounter = walkTime;
            }
        }

    }

    public void TargetGo(Transform transform)
    {
        target = transform;
    }
    public void MoveToTarget()
    {


        if(GetComponent<NpcLogic>().desireFulfilled == false)
        {

            if (Mathf.Abs((myRigidBody.position.y - target.position.y)) >= 1)
            {
                if (myRigidBody.position.y < target.position.y)
                {
                    MoveUp();
                }

                else
                {
                    MoveDown();
                }
            }

            if (Mathf.Abs((myRigidBody.position.x - target.position.x)) >= 1)
            {

                if (myRigidBody.position.x < target.position.x)
                {
                    MoveRight();
                }

                else
                {
                    MoveLeft();
                }
            }

        }
        else
        {
            if (Mathf.Abs((myRigidBody.position.x - target.position.x)) >= 1)
            {

                if (myRigidBody.position.x < target.position.x)
                {
                    MoveRight();
                }

                else
                {
                    MoveLeft();
                }
            }
            if (Mathf.Abs((myRigidBody.position.y - target.position.y)) >= 1)
            {
                if (myRigidBody.position.y < target.position.y)
                {
                    MoveUp();
                }

                else
                {
                    MoveDown();
                }
            }
        }
            //this line is to make them give up after one movement encase an the item is gone.
            target = null;
        
    }

    public void Wandering()
    {
        switch (moveDirection)
        {
            case 0:
                MoveUp();
                break;

            case 1:
                MoveRight();

                break;

            case 2:
                MoveDown();

                break;

            case 3:
                MoveLeft();

                break;

        }
    }

    public void MoveLeft()
    {
        myRigidBody.velocity = new Vector2(-speed, 0);
    }
    public void MoveRight()
    {
        myRigidBody.velocity = new Vector2(speed, 0);
    }
    public void MoveUp()
    {
        myRigidBody.velocity = new Vector2(0, speed);
    }
    public void MoveDown()
    {
        myRigidBody.velocity = new Vector2(0, -speed);
    }


    public int RayCast()
    {
        Rect box = new Rect(GetComponent<BoxCollider2D>().bounds.min.x, GetComponent<BoxCollider2D>().bounds.min.y,
                            GetComponent<BoxCollider2D>().bounds.size.x, GetComponent<BoxCollider2D>().bounds.size.y);

        Vector3 left = new Vector3(box.xMin, box.center.y, transform.position.z);
        Vector3 right = new Vector3(box.xMax, box.center.y, transform.position.z);
        Vector3 up = new Vector3(box.center.x, box.yMax, transform.position.z);
        Vector3 down = new Vector3(box.center.x, box.yMin, transform.position.z);

        RaycastHit2D hitL = Physics2D.Raycast(left, Vector2.left, speed, myLayerMask);
        Debug.DrawRay(left, Vector2.left, Color.green);
        if (hitL.collider != null)
        {
            Debug.Log("hitL");
            return 3;
        }

        RaycastHit2D hitR = Physics2D.Raycast(right, Vector2.right, speed, myLayerMask);
        Debug.DrawRay(right, Vector2.right, Color.green);
        if (hitR.collider != null)
        {
            Debug.Log("hitR");
            return 1;
        }

        RaycastHit2D hitU = Physics2D.Raycast(up, Vector2.up, speed, myLayerMask);
        Debug.DrawRay(up, Vector2.up, Color.green);
        if (hitU.collider != null)
        {
            Debug.Log("hitU");
            return 0;

        }
        RaycastHit2D hitD = Physics2D.Raycast(down, Vector2.down, speed, myLayerMask);
        Debug.DrawRay(down, Vector2.down, Color.green);
        if (hitD.collider != null)
        {
            Debug.Log("hitD");
            return 2;
        }

        return -1;
    }

}
