using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{
    public class ProjectileSpellController : MonoBehaviour
    {


        private PlayerSpellManager _playerSpellManager;
        // public GameObject spell;
        private SFXManager _sFXManager;
        public string SpellSoundName;
        public int ManaCost;

        // private int spellDirection;
        public int spellSpeed;
        private Rigidbody2D myRigidBody;
        private EnumsAndConstants.SpellDirection spellDirection;

        // Use this for initialization
        void Start()
        {
            myRigidBody = GetComponent<Rigidbody2D>();
            _playerSpellManager = FindObjectOfType<PlayerSpellManager>();
            spellDirection = _playerSpellManager.SpellDirection;

            if (!String.IsNullOrEmpty(SpellSoundName))
            {
                _sFXManager = FindObjectOfType<SFXManager>();
                _sFXManager.PlaySound(SpellSoundName);
            }

        }

        // Update is called once per frame
        void Update()
        {

            switch (spellDirection)
            {

                case EnumsAndConstants.SpellDirection.Up:
                    myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 1 * spellSpeed);
                    break;
                case EnumsAndConstants.SpellDirection.UpRight:
                    myRigidBody.velocity = new Vector2(1 * spellSpeed, 1 * spellSpeed);
                    break;
                case EnumsAndConstants.SpellDirection.Right:
                    myRigidBody.velocity = new Vector2(1 * spellSpeed, myRigidBody.velocity.y);
                    break;
                case EnumsAndConstants.SpellDirection.DownRight:
                    myRigidBody.velocity = new Vector2(1 * spellSpeed, -1 * spellSpeed);
                    break;
                case EnumsAndConstants.SpellDirection.Down:
                    myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, -1 * spellSpeed);
                    break;
                case EnumsAndConstants.SpellDirection.DownLeft:
                    myRigidBody.velocity = new Vector2(-1 * spellSpeed, -1 * spellSpeed);
                    break;
                case EnumsAndConstants.SpellDirection.Left:
                    myRigidBody.velocity = new Vector2(-1 * spellSpeed, myRigidBody.velocity.y);
                    break;
                case EnumsAndConstants.SpellDirection.UpLeft:
                    myRigidBody.velocity = new Vector2(-1 * spellSpeed, 1 * spellSpeed);
                    break;
            }
        }
    }
}
