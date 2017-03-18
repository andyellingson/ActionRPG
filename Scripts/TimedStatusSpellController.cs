using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{

    public class TimedStatusSpellController : MonoBehaviour
    {


        private PlayerController playerController;
        private Transform playerTransform;
        public float EffectDuration;
        private float _effectDurationTimer;
        public StatModifier statModifier;
        public string SpellSFXName;

        public enum StatModifier
        {
            HASTE,
            SLOW,
            STRONG,
            WEAK,
            SHIELD
        }


        // Use this for initialization
        void Start()
        {
            playerController = FindObjectOfType<PlayerController>();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            FindObjectOfType<SFXManager>().PlaySound(SpellSFXName);
            _effectDurationTimer = EffectDuration;
            ApplyStatModifier();
        }

        private void ApplyStatModifier()
        {
            switch (statModifier)
            {
                case StatModifier.HASTE:
                    print("Apply Haste");
                    FindObjectOfType<PlayerStatusEffectManager>().EffectDuration = EffectDuration;
                    FindObjectOfType<PlayerStatusEffectManager>().effectType = EnumsAndConstants.EffectType.Haste;
                    //FindObjectOfType<PlayerController>().MoveSpeed = 6;
                    //FindObjectOfType<PlayerController>().MovementSpeedModifier = 2;
                    break;
                case StatModifier.SHIELD:
                    FindObjectOfType<PlayerStatusEffectManager>().EffectDuration = EffectDuration;
                    FindObjectOfType<PlayerStatusEffectManager>().effectType = EnumsAndConstants.EffectType.Shield;
                    break;
                default:
                    print("TimeStatusController: NO STATMODIFIER EFFECT ASSIGNED!!");
                    break;
            }
        }

        private void RemoveStatModifier()
        {
            switch (statModifier)
            {
                case StatModifier.HASTE:
                    print("Remove Haste");

                    //FindObjectOfType<PlayerController>().moveSpeed = 2;
                    //FindObjectOfType<PlayerController>().MovementSpeedModifier = 1;
                    break;
                case StatModifier.SHIELD:
                    break;
                default:
                    print("TimeStatusController: NO STATMODIFIER EFFECT ASSIGNED!!");
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {

            if (_effectDurationTimer > 0)
            {
                _effectDurationTimer -= Time.deltaTime;
                float translation = Time.deltaTime;

                switch (statModifier)
                {
                    case StatModifier.HASTE:
                        transform.position = new Vector3(playerController.transform.position.x, playerController.transform.position.y, playerController.transform.position.z);
                        break;
                    case StatModifier.SHIELD:
                        transform.position.Set(playerController.transform.position.x, playerController.transform.position.y, playerController.transform.position.z);
                        break;
                    default:
                        print("TimeStatusController: NO STATMODIFIER EFFECT ASSIGNED!!");
                        break;
                }
                //move
                //transform.Translate(new Vector3(playerController.transform.position.x, playerController.transform.position.y, playerController.transform.position.z));
                //gameObject.transform.position.Set(playerController.transform.position.x, playerController.transform.position.y, playerController.transform.position.z); 
                // transform.position.Set(playerController.transform.position.x, playerController.transform.position.y, playerController.transform.position.z);
                //transform.position = new Vector3(playerController.transform.position.x, playerController.transform.position.y, playerController.transform.position.z);

            }
            else
            {
                RemoveStatModifier();

                Destroy(gameObject);
            }

        }
    }
}
