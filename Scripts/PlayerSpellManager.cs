using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{
    public class PlayerSpellManager : MonoBehaviour
    {

        public int SpellDamageModifier;
        public EnumsAndConstants.SpellDirection SpellDirection;

        public GameObject spellType;
        public GameObject spellTypeTwo;
        public GameObject spellTypeThree;

        private Animator _animator;
        private PlayerController _playerController;




        // Use this for initialization
        void Start()
        {
            _animator = GetComponent<Animator>();
            _playerController = FindObjectOfType<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public Quaternion SpellRotation(Vector2 move)
        {

            SpellDirection = EnumsAndConstants.SpellDirection.None;

            Quaternion spawnRotation = Quaternion.Euler(Vector3.zero);

            float xDirection = _animator.GetFloat("MoveX");
            float yDirection = _animator.GetFloat("MoveY");

            if (xDirection == 0f && yDirection == 0f)
            {
                xDirection = _animator.GetFloat("LastMoveX");
                yDirection = _animator.GetFloat("LastMoveY");
            }

            if (yDirection > 0f)
            {
                SpellDirection = EnumsAndConstants.SpellDirection.Up;
            }
            else if (yDirection < 0f)
            {
                SpellDirection = EnumsAndConstants.SpellDirection.Down;
            }

            if (SpellDirection != EnumsAndConstants.SpellDirection.None)
            {
                if (SpellDirection == EnumsAndConstants.SpellDirection.Up)
                {
                    if (xDirection < 0f)
                    {
                        SpellDirection = EnumsAndConstants.SpellDirection.UpLeft;
                    }
                    else if (xDirection > 0f)
                    {
                        SpellDirection = EnumsAndConstants.SpellDirection.UpRight;
                    }
                }
                else
                {
                    if (xDirection < 0f)
                    {
                        SpellDirection = EnumsAndConstants.SpellDirection.DownLeft;
                    }
                    else if (xDirection > 0f)
                    {
                        SpellDirection = EnumsAndConstants.SpellDirection.DownRight;
                    }
                }
            }
            else
            {
                if (xDirection < 0f)
                {
                    SpellDirection = EnumsAndConstants.SpellDirection.Left;
                }
                else if (xDirection > 0f)
                {
                    SpellDirection = EnumsAndConstants.SpellDirection.Right;
                }
            }

            switch (SpellDirection)
            {
                case EnumsAndConstants.SpellDirection.Right:
                    spawnRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case EnumsAndConstants.SpellDirection.UpRight:
                    spawnRotation = Quaternion.Euler(0, 0, 45);
                    break;
                case EnumsAndConstants.SpellDirection.Up:
                    spawnRotation = Quaternion.Euler(0, 0, 90);
                    break;
                case EnumsAndConstants.SpellDirection.UpLeft:
                    spawnRotation = Quaternion.Euler(0, 0, 135);
                    break;
                case EnumsAndConstants.SpellDirection.Left:
                    spawnRotation = Quaternion.Euler(0, 0, 180);
                    break;
                case EnumsAndConstants.SpellDirection.DownLeft:
                    spawnRotation = Quaternion.Euler(0, 0, 225);
                    break;
                case EnumsAndConstants.SpellDirection.Down:
                    spawnRotation = Quaternion.Euler(0, 0, 270);
                    break;
                case EnumsAndConstants.SpellDirection.DownRight:
                    spawnRotation = Quaternion.Euler(0, 0, 315);
                    break;
            }

            return spawnRotation;
        }

        public Vector2 SpellCastOffest()
        {
            float xValue = 0f;
            float yValue = 0f;
            float offestValueY = 0.25f;
            float offestValueX = 0.5f;



            switch (SpellDirection)
            {
                case EnumsAndConstants.SpellDirection.Down:
                    yValue -= offestValueX;
                    break;
                case EnumsAndConstants.SpellDirection.DownLeft:
                    yValue -= offestValueY;
                    xValue -= offestValueX;
                    break;
                case EnumsAndConstants.SpellDirection.Left:
                    xValue -= offestValueX;
                    break;
                case EnumsAndConstants.SpellDirection.UpLeft:
                    yValue += offestValueY;
                    xValue -= offestValueX;
                    break;
                case EnumsAndConstants.SpellDirection.Up:
                    yValue += offestValueY * 2;
                    break;
                case EnumsAndConstants.SpellDirection.UpRight:
                    yValue += offestValueY;
                    xValue += offestValueX;
                    break;
                case EnumsAndConstants.SpellDirection.Right:
                    xValue += offestValueX;
                    break;
                case EnumsAndConstants.SpellDirection.DownRight:
                    xValue += offestValueX;
                    yValue -= offestValueY;
                    break;
            }

            return new Vector2(xValue, yValue);
        }
    }
}