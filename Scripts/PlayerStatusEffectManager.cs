using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
    {
        public class PlayerStatusEffectManager : MonoBehaviour
        {

            private PlayerController playerController;
            private PlayerHealthManager playerHealthManager;
            private Rigidbody2D playerRigidBody;

            public float EffectDuration;
            private float EffectTimer;
            public EnumsAndConstants.EffectType effectType;
            public int dotDamage;

            internal bool flashActive;
            public float flashLength;
            private float flashCounter;

            private SpriteRenderer _playerSpriteRenderer;

            public GameObject DamageNumbers;

            public GameObject BurnStatusEffect;
            public GameObject FreezeStatusEffect;
            public GameObject StunnedStatusEffect;
            public GameObject HasteStatusEffect;

            private float InitialSpeed;

            public bool CurrentlyEffected;
            public GameObject CurrentStatusEffect;

            // Use this for initialization
            void Start()
            {
                CurrentlyEffected = false;
                playerController = gameObject.GetComponent<PlayerController>();
                playerHealthManager = gameObject.GetComponent<PlayerHealthManager>();
                playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
                _playerSpriteRenderer = GetComponent<SpriteRenderer>();
                InitialSpeed = playerController.moveSpeed;

            }

            // Update is called once per frame
            void Update()
            {
                EffectTimer -= Time.deltaTime;

                if ((EffectTimer < 0) && (EffectDuration > 0))
                {
                    EffectTimer = --EffectDuration;

                    //status effect applied in HurtPlayer
                    switch (effectType)
                    {
                        case EnumsAndConstants.EffectType.Freeze:
                            playerController.MovementSpeedModifier = 0;
                            if (!CurrentlyEffected) CurrentStatusEffect = Instantiate(FreezeStatusEffect, transform.position, Quaternion.Euler(Vector3.zero));
                            CurrentlyEffected = true;
                            break;
                        case EnumsAndConstants.EffectType.Burn:
                            playerHealthManager.HealPlayer(dotDamage);
                            if (!CurrentlyEffected) CurrentStatusEffect = Instantiate(BurnStatusEffect, transform.position, Quaternion.Euler(Vector3.zero));

                            var clone = (GameObject)Instantiate(DamageNumbers, transform.position, Quaternion.Euler(Vector3.zero));
                            clone.GetComponent<FloatingDamageNumbers>().damage = dotDamage;

                            CurrentlyEffected = true;
                            break;
                        case EnumsAndConstants.EffectType.Stunned:
                            playerController.MovementSpeedModifier = 0.25f;
                            if (!CurrentlyEffected) CurrentStatusEffect = Instantiate(StunnedStatusEffect, transform.position, Quaternion.Euler(Vector3.zero));
                            CurrentlyEffected = true;
                            break;
                        case EnumsAndConstants.EffectType.Poison:
                            playerHealthManager.HealPlayer(dotDamage);
                            //   playerHealthManager.CurrentHealth -= dotDamage;
                            if (DamageNumbers != null)
                            {
                                var clone2 = (GameObject)Instantiate(DamageNumbers, transform.position, Quaternion.Euler(Vector3.zero));
                                clone2.GetComponent<FloatingDamageNumbers>().damage = dotDamage;
                            }
                            else
                            {
                                print("ENEMYSTATUSMANAGER: DAMAGENUMBERS null");
                            }
                            _playerSpriteRenderer.color = new Color(0f, 1f, 0f, 1f);
                            CurrentlyEffected = true;
                            break;
                        case EnumsAndConstants.EffectType.Haste:
                            if (!CurrentlyEffected)
                            {
                               // CurrentStatusEffect = Instantiate(HasteStatusEffect, transform.position, Quaternion.Euler(Vector3.zero));
                                FindObjectOfType<PlayerController>().MoveSpeed = 5;
                                FindObjectOfType<PlayerController>().MovementSpeedModifier = 1;
                                CurrentlyEffected = true;
                            }
                            break;
                    }
                }
                else if ((EffectTimer <= 0) && (EffectDuration <= 0))
                {
                    ResetStatus();
                }

                if (CurrentStatusEffect != null) CurrentStatusEffect.transform.position = gameObject.transform.position;

            }

            private void ResetStatus()
            {
                CurrentlyEffected = false;
                dotDamage = 0;
                effectType = EnumsAndConstants.EffectType.Normal;
                playerController.moveSpeed = InitialSpeed;
                playerController.MovementSpeedModifier = 1;
                _playerSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                if (CurrentStatusEffect != null)
                {
                    CurrentStatusEffect.SetActive(false);
                    Destroy(CurrentStatusEffect);
                }
            }
        }
    }