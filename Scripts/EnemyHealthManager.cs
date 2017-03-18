using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{

    public class EnemyHealthManager : MonoBehaviour
    {

        public int MaxHealth;
        public int CurrentHealth;
        private PlayerStatManager playerStatManager;
        private EnemyStatusManager myStatus;

        // Use this for initialization
        void Start()
        {
            CurrentHealth = MaxHealth;
            myStatus = gameObject.GetComponent<EnemyStatusManager>();
            playerStatManager = FindObjectOfType<PlayerStatManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (CurrentHealth < 0)
            {
                if (myStatus.CurrentStatusEffect != null)
                    Destroy(myStatus.CurrentStatusEffect);

                if (gameObject.GetComponent<ItemDropController>() != null)
                    gameObject.GetComponent<ItemDropController>().DropItems();

                playerStatManager.AddExperience(gameObject.GetComponent<EnemyController>().ExperiencePoints);

                gameObject.SetActive(false);

                Destroy(gameObject);
            }
        }

        public void HurtEnemy(double damage)
        {
            CurrentHealth -= Convert.ToInt32(damage);
        }

        public void Healenemy(int healAmount)
        {
            CurrentHealth += healAmount;
            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            //if (other.gameObject.tag == "enemy")
            //{
            //    other.gameObject.SetActive(false);
            //    reloading = true;
            //    theenemy = other.gameObject;
            //}

        }
    }
}