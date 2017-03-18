using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{
    public class SFXManager : MonoBehaviour
    {

        public AudioSource CoinPickup;
        public AudioSource SwingSword;
        public AudioSource Potion;
        public AudioSource Explosion;
        public AudioSource Freeze;
        public AudioSource Heal;
        public AudioSource Die;
        public AudioSource Zap;
        public AudioSource Wind;
        public AudioSource Poison;
        public AudioSource Teleport;
        public AudioSource Arcane;
        public AudioSource MagicFail;


        private static bool sfxmanExists;

        // Use this for initialization
        void Start()
        {
            if (!sfxmanExists)
            {
                sfxmanExists = true;
                DontDestroyOnLoad(transform.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlaySound(string soundName)
        {
            switch (soundName.ToUpper())
            {
                case "WIND":
                    Wind.Play();
                    break;
                case "FREEZE":
                    Freeze.Play();
                    break;
                case "EXPLOSION":
                    Explosion.Play();
                    break;
                case "DIE":
                    Die.Play();
                    break;
                case "COIN PICKUP":
                    CoinPickup.Play();
                    break;
                case "SWING SWORD":
                    SwingSword.Play();
                    break;
                case "HEAL":
                    Heal.Play();
                    break;
                case "POTION":
                    Potion.Play();
                    break;
                case "POISON":
                    Poison.Play();
                    break;
                case "TELEPORT":
                    Teleport.Play();
                    break;
                case "ZAP":
                    Zap.Play();
                    break;

                default:
                    print("SFXMANAGER: NO SPELL SOUND ASSIGNED FOR THE " + soundName.ToUpper() + " DUMMY!!");
                    break;

            }
        }

    }
}