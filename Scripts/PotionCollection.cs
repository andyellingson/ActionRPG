﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ActionRPG
{
    public class PotionCollection : MonoBehaviour
    {
        private SFXManager sfxManager;

        // Use this for initialization
        void Start()
        {
            sfxManager = FindObjectOfType<SFXManager>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<PlayerItemManager>().HealthPotionCount++;
                sfxManager.Heal.Play();
                Destroy(gameObject);
            }
        }
    }
}
