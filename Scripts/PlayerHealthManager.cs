using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ActionRPG
{
    public class PlayerHealthManager : MonoBehaviour
    {


        public int playerMaxHealth;
        public int playerCurrentHealth;

        public float regenTimer;
        private float regenTimerCounter;

        private bool flashActive;
        public float flashLength;
        private float flashCounter;
        private SpriteRenderer _playerSpriteRenderer;

        public float waitToReload;

        //private GameObject thePlayer;
        //private bool reloading;

        // Use this for initialization
        void Start()
        {
            playerCurrentHealth = playerMaxHealth;
            _playerSpriteRenderer = GetComponent<SpriteRenderer>();
            waitToReload = 5;
        }

        // Update is called once per frame
        void Update()
        {
            if (playerCurrentHealth < 0)
            {
                FindObjectOfType<SFXManager>().Die.Play();

                gameObject.SetActive(false);
            }


            regenTimerCounter -= Time.deltaTime;

            if (playerCurrentHealth < 0) playerCurrentHealth = 0;

            if (playerCurrentHealth < playerMaxHealth && regenTimerCounter <= 0)
            {
                regenTimerCounter = regenTimer;
                playerCurrentHealth += 1;
            }

            if (flashActive)
            {

                if (flashCounter > flashLength * 0.66f)
                {
                    _playerSpriteRenderer.color = new Color(_playerSpriteRenderer.color.r, _playerSpriteRenderer.color.g, _playerSpriteRenderer.color.b, 0f);
                }
                else if (flashCounter > flashLength * 0.33f)
                {
                    _playerSpriteRenderer.color = new Color(_playerSpriteRenderer.color.r, _playerSpriteRenderer.color.g, _playerSpriteRenderer.color.b, 1f);
                }
                else if (flashCounter > flashLength * 0f)
                {
                    _playerSpriteRenderer.color = new Color(_playerSpriteRenderer.color.r, _playerSpriteRenderer.color.g, _playerSpriteRenderer.color.b, 0f);
                }
                if (flashCounter < 0f)
                {
                    _playerSpriteRenderer.color = new Color(_playerSpriteRenderer.color.r, _playerSpriteRenderer.color.g, _playerSpriteRenderer.color.b, 1f);
                    flashActive = false;

                }

                flashCounter -= Time.deltaTime;

            }

            //if(reloading)
            //      {
            //          waitToReload -= Time.deltaTime;
            //          if(waitToReload < 0)
            //          {
            //              SceneManager.LoadScene("Home Town");
            //              thePlayer.SetActive(true);
            //          }
            //      }
        }

        public void HurtPlayer(double damage)
        {
            playerCurrentHealth -= Convert.ToInt32(damage);
            flashActive = true;
            flashCounter = flashLength;
        }

        public void HealPlayer(int healAmount)
        {
            playerCurrentHealth += healAmount;
            if (playerCurrentHealth > playerMaxHealth)
                playerCurrentHealth = playerMaxHealth;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            //if (other.gameObject.tag == "Player")
            //{
            //    other.gameObject.SetActive(false);
            //    reloading = true;
            //    thePlayer = other.gameObject;
            //}

        }
    }
}