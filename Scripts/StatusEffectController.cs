using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{
    public class StatusEffectController : MonoBehaviour
    {


        private PlayerController playerController;
        private PlayerHealthManager playerHealthManager;
        private PlayerManaManager playerManaManager;

        public float EffectDuration;
        private float EffectTimer;
        public EnumsAndConstants.EffectType effectType;
        public int dotDamage;

        private float InitialSpeed;


        // Use this for initialization
        void Start()
        {
            playerController = GetComponent<PlayerController>();
            playerHealthManager = GetComponent<PlayerHealthManager>();
            playerManaManager = GetComponent<PlayerManaManager>();
            EffectTimer = EffectDuration;
            InitialSpeed = playerController.moveSpeed;

        }

        // Update is called once per frame
        void Update()
        {
            EffectTimer -= Time.deltaTime;
            if (EffectTimer < 0)
            {
                RemoveStatModifier(effectType);
            }
            else
            {
                switch (effectType)
                {
                    case EnumsAndConstants.EffectType.Haste:
                        FindObjectOfType<PlayerController>().MoveSpeed = 5;
                        FindObjectOfType<PlayerController>().MovementSpeedModifier = 2;
                        break;
                    case EnumsAndConstants.EffectType.Freeze:
                        FindObjectOfType<PlayerController>().MoveSpeed = 0;
                        FindObjectOfType<PlayerController>().MovementSpeedModifier = 0;
                        break;
                    case EnumsAndConstants.EffectType.Shield:
                        FindObjectOfType<PlayerHealthManager>().playerCurrentHealth = 1000000;
                        break;
                    case EnumsAndConstants.EffectType.Burn:
                        playerHealthManager.playerCurrentHealth -= dotDamage;
                        break;
                }
            }

        }

        private void RemoveStatModifier(EnumsAndConstants.EffectType statModifier)
        {

            switch (statModifier)
            {
                case EnumsAndConstants.EffectType.Haste:
                    print("Remove Haste");
                    FindObjectOfType<PlayerController>().moveSpeed = 2;
                    FindObjectOfType<PlayerController>().MovementSpeedModifier = 1;
                    break;
                case EnumsAndConstants.EffectType.Freeze:
                    FindObjectOfType<PlayerController>().MoveSpeed = 2;
                    FindObjectOfType<PlayerController>().MovementSpeedModifier = 1;
                    break;
                case EnumsAndConstants.EffectType.Shield:
                    FindObjectOfType<PlayerHealthManager>().playerCurrentHealth = FindObjectOfType<PlayerHealthManager>().playerMaxHealth;
                    break;
                default:
                    print("TimeStatusController: NO STATMODIFIER EFFECT ASSIGNED!!");
                    break;
            }
        }
    }
}