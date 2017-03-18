using UnityEngine;
using System.Collections;
using System;

namespace ActionRPG
{
    public class EnemyController : MonoBehaviour
    {


        public float xSpawn;
        public float ySpawn;
        public int wanderDistance;
        public float moveSpeed;
        public int ExperiencePoints;
        public EnumsAndConstants.Mood mood;
        private bool moving;

        public GameObject Spell;

        public float timeBetweenMove;
        private float timeBetweenMoveCounter;

        public float timeToMove;
        private float timeToMoveCounter;

        private Animator batAnimator;
        private Rigidbody2D myRigidBody;
        private Vector3 moveDirection;

        private Transform target;                           //Transform to attempt to move toward each turn.

        private EnemyStatusManager enemyStatusManager;

        public float MovementSpeedModifier;


        // Use this for initialization
        void Start()
        {
            myRigidBody = GetComponent<Rigidbody2D>();
            enemyStatusManager = GetComponent<EnemyStatusManager>();

            xSpawn = myRigidBody.position.x;
            ySpawn = myRigidBody.position.y;

            batAnimator = GetComponent<Animator>();
            //timeBetweenMoveCounter = timeBetweenMove;
            //timeToMoveCounter = timeToMove;

            timeBetweenMoveCounter = UnityEngine.Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
            timeToMoveCounter = UnityEngine.Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

        }

        // Update is called once per frame
        void Update()
        {

            if (gameObject.GetComponent<EnemyHealthManager>().CurrentHealth < gameObject.GetComponent<EnemyHealthManager>().MaxHealth) mood = EnumsAndConstants.Mood.Angry;

            moveSpeed = MovementSpeedModifier * moveSpeed;

            timeToMoveCounter -= Time.deltaTime;

            //target = GameObject.FindGameObjectWithTag("Player").transform;
            target = FindObjectOfType<PlayerController>().transform;


            float step = moveSpeed * Time.deltaTime;

            if (moving)
            {

                timeToMoveCounter -= Time.deltaTime;
                myRigidBody.velocity = moveDirection;

                if (timeToMoveCounter < 0f)
                {
                    moving = false;
                    //timeBetweenMoveCounter = timeBetweenMove;
                    timeBetweenMoveCounter = UnityEngine.Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);

                }
            }
            else
            {
                timeBetweenMoveCounter -= Time.deltaTime;

                myRigidBody.velocity = Vector2.zero;

                if (timeBetweenMoveCounter < 0f)
                {
                    moving = true;
                    //timeToMoveCounter = timeToMove;
                    timeToMoveCounter = UnityEngine.Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

                    if (Spell != null)
                    {

                        Quaternion spawnRotation = Quaternion.Euler(Vector3.zero);

                        Instantiate(Spell, myRigidBody.position, spawnRotation);

                        //Instantiate(Spell, transform);
                    }

                    //if (mood == EnumsAndConstants.Mood.Angry/*Vector3.Distance(gameObject.transform.position, target.position) < 5.0f*/)
                    //{

                    //    moveDirection = Vector3.MoveTowards(transform.position, target.position, moveSpeed);
                    //    batAnimator.SetFloat("MoveX", transform.position.x);
                    //    batAnimator.SetFloat("MoveY", transform.position.y);
                    //    batAnimator.SetBool("Moving", moving);
                    //}
                    //else
                    //{

                    //if (mood != EnumsAndConstants.Mood.Angry)
                    //   // transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                    //    moveDirection = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                    //else
                    //    moveDirection = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);


                    if (mood == EnumsAndConstants.Mood.Angry)
                        moveDirection = ChaseTarget(target);
                    else
                        moveDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f) * moveSpeed, UnityEngine.Random.Range(-1f, 1f) * moveSpeed, 0f);

                    batAnimator.SetFloat("MoveX", moveDirection.x);
                    batAnimator.SetFloat("MoveY", moveDirection.y);
                    batAnimator.SetBool("Moving", moving);
                    //}
                }
                else
                {

                }
            }


        }

        private Vector3 ChaseTarget(Transform target)
        {
            int xMove, yMove = 0;

            if(target.position.x > transform.position.x)
            {
                xMove = 1;
            }
            else if (target.position.x < transform.position.x)
            {
                xMove = -1;
            }
            else
            {
                xMove = 0;
            }


            if(target.position.y > transform.position.y)
            {
                yMove = 1;
            }
            else if (target.position.y < transform.position.y)
            {
                yMove = -1;
            }
            else
            {
                yMove = 0;
            }

            return new Vector3(xMove * moveSpeed, yMove * moveSpeed, 0f);
        }
    }
}