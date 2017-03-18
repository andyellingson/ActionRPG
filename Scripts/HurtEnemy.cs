using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{
    public class HurtEnemy : MonoBehaviour {

    public int damage;
    private int currentDamageBonus;
    public GameObject damageParticleEffect;
    public Transform contactPoint;
    public GameObject damageNumber;
    public EnumsAndConstants.EffectType statusEffect;
    public float statusEffectDuration;
    public int dotDamage;
    public bool projectile;


    private EnemyStatusManager enemyStatusManager;
    private Rigidbody2D enemyRigidBody;

    private PlayerStatManager playerStatManager;


	// Use this for initialization
	void Start ()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        playerStatManager = FindObjectOfType<PlayerStatManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    	
         
	}

    private void OnTriggerEnter2D(Collider2D other)
    {

        currentDamageBonus = playerStatManager.CurrentAttack;

        if(other.gameObject.tag == "Enemy")
        {
            
            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damage + currentDamageBonus);
            enemyStatusManager = other.gameObject.GetComponent<EnemyStatusManager>();

            //create damage burst animation object
            if(damageParticleEffect != null)
                Instantiate(damageParticleEffect, contactPoint.position, contactPoint.rotation);

            //create damage number object
            var clone = (GameObject)Instantiate(damageNumber, contactPoint.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingDamageNumbers>().damage = damage + currentDamageBonus;

            ApplyStatusEffect(other);

            //if the thing doing damage is a projectile destory it
            if (projectile)
            {
                Destroy(gameObject);
            }

        }
    }

    //Other side in EnemyStatusManager
    private void ApplyStatusEffect(Collider2D other)
    {
        var enemyStatus = other.GetComponent<EnemyStatusManager>();

        if (enemyStatus.effectType == EnumsAndConstants.EffectType.Normal)
        {
            switch (statusEffect)
            {
                case EnumsAndConstants.EffectType.Burn:
                    enemyStatusManager.effectType = EnumsAndConstants.EffectType.Burn;
                    enemyStatusManager.dotDamage = dotDamage;
                    enemyStatusManager.DamageNumbers = damageNumber;
                    enemyStatusManager.EffectDuration = statusEffectDuration;
                    break;
                case EnumsAndConstants.EffectType.Fear:
                    break;
                case EnumsAndConstants.EffectType.Freeze:
                    enemyStatusManager.effectType = EnumsAndConstants.EffectType.Freeze;
                    enemyStatusManager.EffectDuration = statusEffectDuration;
                    break;
                case EnumsAndConstants.EffectType.Stunned:
                    enemyStatusManager.effectType = EnumsAndConstants.EffectType.Stunned;
                    enemyStatusManager.EffectDuration = statusEffectDuration;
                    break;
                case EnumsAndConstants.EffectType.Normal:
                    break;
                case EnumsAndConstants.EffectType.Poison:
                    enemyStatusManager.effectType = EnumsAndConstants.EffectType.Poison;
                    enemyStatusManager.flashLength = statusEffectDuration;
                    enemyStatusManager.dotDamage = dotDamage;
                    enemyStatusManager.DamageNumbers = damageNumber;
                    enemyStatusManager.EffectDuration = statusEffectDuration;
                    break;
                case EnumsAndConstants.EffectType.Slow:
                    break;
            }
        }
    }
}
}