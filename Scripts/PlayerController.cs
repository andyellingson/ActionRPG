using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{

    public class PlayerController : MonoBehaviour
    {


        private bool attacking;
        public float attackTime;
        private float attackTimeCounter;
        public float moveSpeed;
        public float MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }
        public float MovementSpeedModifier;
        public EnumsAndConstants.SpellDirection direction;

        public string startPoint;
        private bool playerMoving;
        private Vector2 lastMove;
        private static bool playerExists;
        private Animator anim;
        private Rigidbody2D myRigidBody;
        private SFXManager sfxManager;
        private PlayerHealthManager playerHealthManager;
        private PlayerManaManager playerManaManager;
        private PlayerSpellManager playerSpellManager;

        public Inventory inventory;
        public Inventory spellBook;
        public Inventory hotBar;

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
            myRigidBody = GetComponent<Rigidbody2D>();
            sfxManager = FindObjectOfType<SFXManager>();
            playerHealthManager = GetComponent<PlayerHealthManager>();
            playerManaManager = GetComponent<PlayerManaManager>();
            playerSpellManager = GetComponent<PlayerSpellManager>();

            direction = EnumsAndConstants.SpellDirection.None;

            if (!playerExists)
            {
                playerExists = true;
                DontDestroyOnLoad(transform.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }


        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.tag == "Item")
        //    {
        //        bool result = inventory.AddItem(other.GetComponent<Item>());
        //        print("result = " + result + " ItemType = " + other.gameObject.GetComponent<Item>().type);
        //        if (other.gameObject.GetComponent<Item>().type == ItemType.HEALTH_POTION && result)
        //        {
        //            FindObjectOfType<SFXManager>().Potion.Play();
        //            FindObjectOfType<PlayerItemManager>().HealthPotionCount++;
        //        }
        //        inventory.AddItem(other.GetComponent<Item>());
        //    }
        //    print("In collider");
        //}

        // Update is called once per frame
        void Update()
        {
            playerMoving = false;

            //movement speed modified down
            if (MovementSpeedModifier < 1)
            {
                moveSpeed = MovementSpeedModifier;
            }
            else if (MovementSpeedModifier > 1)
            {
                moveSpeed = MovementSpeedModifier * moveSpeed;

            }

            print("PLAYER CONTROLLER : MOVE SPEED = " + moveSpeed);

            if (!attacking)
            {
                if ((Input.GetAxisRaw("Horizontal") > 0.5f) || (Input.GetAxisRaw("Horizontal") < -0.5f))
                {
                    //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
                    myRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidBody.velocity.y);
                    playerMoving = true;
                    lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

                }

                if ((Input.GetAxisRaw("Vertical") > 0.5f) || (Input.GetAxisRaw("Vertical") < -0.5f))
                {
                    //transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
                    myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
                    playerMoving = true;
                    lastMove = new Vector2(0, Input.GetAxisRaw("Vertical"));
                }

                if ((Input.GetAxisRaw("Horizontal") < 0.5f) && (Input.GetAxisRaw("Horizontal") > -0.5f))
                {
                    myRigidBody.velocity = new Vector2(0, myRigidBody.velocity.y);
                }

                if ((Input.GetAxisRaw("Vertical") < 0.5f) && (Input.GetAxisRaw("Vertical") > -0.5f))
                {
                    myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    sfxManager.SwingSword.Play();
                    attacking = true;
                    attackTimeCounter = attackTime;
                    anim.SetBool("PlayerAttacking", true);
                    //stop the player's movement
                    myRigidBody.velocity = Vector2.zero;
                }

            }

            if (attackTimeCounter > 0)
            {
                attackTimeCounter -= Time.deltaTime;
            }

            if (attackTimeCounter <= 0)
            {
                attacking = false;
                anim.SetBool("PlayerAttacking", false);
            }

            float xDirection = Input.GetAxisRaw("Horizontal");
            float yDirection = Input.GetAxisRaw("Vertical");

            int manaCost = playerSpellManager.spellType.GetComponent<ProjectileSpellController>().ManaCost;

            //cast spell one
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (playerManaManager.CurrentMana > manaCost)
                {
                    playerManaManager.SpendMana(manaCost);

                    Quaternion spawnRotation = playerSpellManager.SpellRotation(lastMove);
                    Instantiate(playerSpellManager.spellType, transform.position, spawnRotation);

                    //stop the player's movement
                    myRigidBody.velocity = Vector2.zero;
                }

            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (playerManaManager.CurrentMana > manaCost)
                {
                    playerManaManager.SpendMana(manaCost);

                    Quaternion spawnRotation = playerSpellManager.SpellRotation(lastMove);
                    Instantiate(playerSpellManager.spellTypeTwo, transform.position, spawnRotation);

                    //stop the player's movement
                    myRigidBody.velocity = Vector2.zero;
                }

            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (playerManaManager.CurrentMana > manaCost)
                {
                    playerManaManager.SpendMana(manaCost);

                    Quaternion spawnRotation = playerSpellManager.SpellRotation(lastMove);
                    Instantiate(playerSpellManager.spellTypeThree, transform.position, spawnRotation);

                    //stop the player's movement
                    myRigidBody.velocity = Vector2.zero;
                }

            }

            //use potion
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (gameObject.GetComponent<PlayerItemManager>().HealthPotionCount > 0)
                {
                    //update text by poition
                    gameObject.GetComponent<PlayerItemManager>().HealthPotionCount--;

                    //remove potion from inventory
                    inventory.UseHealthPotion();

                    //heal player
                    playerHealthManager.HealPlayer(100);
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (playerManaManager.CurrentMana > manaCost)
                {
                    UseHotBarSkill(manaCost, 0);
                }

            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (playerManaManager.CurrentMana > manaCost)
                {
                    UseHotBarSkill(manaCost, 1);
                }

            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (playerManaManager.CurrentMana > manaCost)
                {
                    UseHotBarSkill(manaCost, 2);
                }

            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (playerManaManager.CurrentMana > manaCost)
                {
                    UseHotBarSkill(manaCost, 3);
                }

            }
            //ToggleInventoryVisibility
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventory.isActiveAndEnabled)
                {
                    inventory.gameObject.SetActive(false);
                    inventory.OpenOrCloseInventory(true);

                }
                else
                {
                    inventory.gameObject.SetActive(true);
                    inventory.OpenOrCloseInventory(false);
                }

            }
            //ToggleSpellBookVisibility
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (spellBook.isActiveAndEnabled)
                {
                    spellBook.gameObject.SetActive(false);
                    spellBook.OpenOrCloseInventory(true);

                }
                else
                {
                    spellBook.gameObject.SetActive(true);
                    spellBook.OpenOrCloseInventory(false);
                }

            }

            anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
            anim.SetFloat("LastMoveX", lastMove.x);
            anim.SetFloat("LastMoveY", lastMove.y);
            anim.SetBool("PlayerMoving", playerMoving);
        }

        private void UseHotBarSkill(int manaCost, int hotBarIndex)
        {
            Vector2 offest = playerSpellManager.SpellCastOffest();

            Quaternion spawnRotation = playerSpellManager.SpellRotation(lastMove);

            if (hotBar.GetHotBarSlotAtIndex(hotBarIndex) != null)
            {
                playerManaManager.SpendMana(manaCost);

                if (hotBar.GetHotBarSlotAtIndex(hotBarIndex).HotBarSpell.tag != "StatusEffectSpell")
                {
                    var temp = new Vector3(transform.position.x + offest.x, transform.position.y + offest.y);
                    Instantiate(hotBar.GetHotBarSlotAtIndex(hotBarIndex).HotBarSpell, temp, spawnRotation);
                }
            }
            //Instantiate(hotBar.GetHotBarSlotAtIndex(hotBarIndex).HotBarSpell, transform.position, spawnRotation);
            else
                sfxManager.MagicFail.Play();

            //stop the player's movement
            myRigidBody.velocity = Vector2.zero;
        }

      

    }
}