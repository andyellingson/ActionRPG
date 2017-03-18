using System;
using UnityEngine;

namespace ActionRPG
{
    public class EnemyStatusManager : MonoBehaviour
    {

        private EnemyController enemyController;
        private EnemyHealthManager enemyHealthManager;
        private Rigidbody2D enemyRigidBody;

        public float EffectDuration;
        private float EffectTimer;
        public EnumsAndConstants.EffectType effectType;
        public int dotDamage;

        internal bool flashActive;
        public float flashLength;
        private float flashCounter;

        private SpriteRenderer _enemySpriteRenderer;

        public GameObject DamageNumbers;

        public GameObject BurnStatusEffect;
        public GameObject FreezeStatusEffect;
        public GameObject StunnedStatusEffect;

        private float InitialSpeed;

        public bool CurrentlyEffected;
        public GameObject CurrentStatusEffect;

        // Use this for initialization
        void Start()
        {
            CurrentlyEffected = false;
            enemyController = gameObject.GetComponent<EnemyController>();
            enemyHealthManager = gameObject.GetComponent<EnemyHealthManager>();
            enemyRigidBody = gameObject.GetComponent<Rigidbody2D>();
            _enemySpriteRenderer = GetComponent<SpriteRenderer>();
            InitialSpeed = enemyController.moveSpeed;

        }

        // Update is called once per frame
        void Update()
        {
            EffectTimer -= Time.deltaTime;

            if ((EffectTimer < 0) && (EffectDuration > 0))
            {
                EffectTimer = --EffectDuration;

                //status effect applied in HurtEnemy
                switch (effectType)
                {
                    case EnumsAndConstants.EffectType.Freeze:
                        enemyController.MovementSpeedModifier = 0;
                        if (!CurrentlyEffected) CurrentStatusEffect = Instantiate(FreezeStatusEffect, transform.position, Quaternion.Euler(Vector3.zero));
                        CurrentlyEffected = true;
                        break;
                    case EnumsAndConstants.EffectType.Burn:
                        enemyHealthManager.CurrentHealth -= dotDamage;
                        if (!CurrentlyEffected) CurrentStatusEffect = Instantiate(BurnStatusEffect, transform.position, Quaternion.Euler(Vector3.zero));

                        var clone = (GameObject)Instantiate(DamageNumbers, transform.position, Quaternion.Euler(Vector3.zero));
                        clone.GetComponent<FloatingDamageNumbers>().damage = dotDamage;

                        CurrentlyEffected = true;
                        break;
                    case EnumsAndConstants.EffectType.Stunned:
                        enemyController.MovementSpeedModifier = 0.25f;
                        if (!CurrentlyEffected) CurrentStatusEffect = Instantiate(StunnedStatusEffect, transform.position, Quaternion.Euler(Vector3.zero));
                        CurrentlyEffected = true;
                        break;
                    case EnumsAndConstants.EffectType.Poison:
                        enemyHealthManager.CurrentHealth -= dotDamage;
                        if (DamageNumbers != null)
                        {
                            var clone2 = (GameObject)Instantiate(DamageNumbers, transform.position, Quaternion.Euler(Vector3.zero));
                            clone2.GetComponent<FloatingDamageNumbers>().damage = dotDamage;
                        }
                        else
                        {
                            print("ENEMYSTATUSMANAGER: DAMAGENUMBERS null");
                        }
                        _enemySpriteRenderer.color = new Color(0f, 1f, 0f, 1f);
                        CurrentlyEffected = true;
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
            enemyController.moveSpeed = InitialSpeed;
            enemyController.MovementSpeedModifier = 1;
            _enemySpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            if (CurrentStatusEffect != null)
            {
                CurrentStatusEffect.SetActive(false);
                Destroy(CurrentStatusEffect);
            }
        }
    }
}