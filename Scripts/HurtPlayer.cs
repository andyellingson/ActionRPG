using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{
    public class HurtPlayer : MonoBehaviour
    {

        public double damage;
        private int CurrentDamage;
        public bool projectile;
        public GameObject DamageNumber;
        private PlayerStatManager playerStatManager;

        // Use this for initialization
        void Start()
        {
            playerStatManager = FindObjectOfType<PlayerStatManager>();
        }

        // Update is called once per frame
        void Update()
        {


        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                CurrentDamage = Convert.ToInt32(damage) - playerStatManager.CurrendDefense;
                if (CurrentDamage < 0) CurrentDamage = 0;

                other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(CurrentDamage);

                //create damage number object
                if (DamageNumber != null)
                {
                    var clone = (GameObject)Instantiate(DamageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));
                    clone.GetComponent<FloatingDamageNumbers>().damage = CurrentDamage;
                }
                else
                {
                    print("HurtPlayer: Assign DamageNumber object to " + this.name);
                }

                //if the thing doing damage is a projectile destory it
                if (projectile)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}