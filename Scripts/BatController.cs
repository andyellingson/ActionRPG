using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour {




    public float moveSpeed;
    public int hitPoints;
    private bool moving;

    public float timeBetweenMove;
    private float timeBetweenMoveCounter;

    public float timeToMove;
    private float timeToMoveCounter;

    private Animator batAnimator;
    private Rigidbody2D myRigidBody;
    private Vector3 moveDirection;

    private Transform target;                           //Transform to attempt to move toward each turn.


    // Use this for initialization
    void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        batAnimator = GetComponent<Animator>();
        timeBetweenMoveCounter = timeBetweenMove;
        timeToMoveCounter = timeToMove;

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {

        //These values allow us to choose between the cardinal directions: up, down, left and right.
        int xDir = 0;
        int yDir = 0;

        if (moving)
        {
            timeToMoveCounter -= Time.deltaTime;

            myRigidBody.velocity = moveDirection;

            if (timeToMoveCounter < 0f)
            {
                moving = false;
                timeBetweenMoveCounter = timeBetweenMove;

            }

        }
        else
        {
            timeBetweenMoveCounter -= Time.deltaTime;

            myRigidBody.velocity = Vector2.zero;

            if (timeBetweenMoveCounter < 0f)
            {
                moving = true;
                timeToMoveCounter = timeToMove;

                if (target.position.x <= transform.position.x)
                    xDir = -1;
                else
                    xDir = 1;

                if (target.position.y <= transform.position.y)
                    yDir = -1;
                else
                    yDir = 1;

                moveDirection = new Vector3(xDir * moveSpeed, yDir * moveSpeed, 0f);

                //moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
            }
        }

        batAnimator.SetFloat("MoveX", moveDirection.x);
        batAnimator.SetFloat("MoveY", moveDirection.y);
        batAnimator.SetBool("Moving", moving);
    }
}
