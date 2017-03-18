using ActionRPG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ActionRPG
{
    public class PlayerStatManager : MonoBehaviour
    {
        public int currentLevel;
        public int currentExp;
        public int[] XPLevelThresholds;

        public int[] HPLevels;
        public int[] ManaLevels;
        public int[] AttackLevels;
        public int[] DefenseLevels;

        public int CurrentHp;
        public int CurrentMana;
        public int CurrentAttack;
        public int CurrendDefense;

        public GameObject LevelUpParticleEffect;
        private PlayerController playerController;
        private PlayerHealthManager playerHealthManager;
        private PlayerManaManager playerManaManger;
        private SFXManager sfxManager;


        // Use this for initialization
        void Start()
        {
            CurrentHp = HPLevels[1];
            CurrentMana = ManaLevels[1];
            CurrentAttack = AttackLevels[1];
            CurrendDefense = DefenseLevels[1];

            playerHealthManager = FindObjectOfType<PlayerHealthManager>();
            playerManaManger = FindObjectOfType<PlayerManaManager>();
            sfxManager = FindObjectOfType<SFXManager>();
            playerController = FindObjectOfType<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (currentExp >= XPLevelThresholds[currentLevel])
            {
                LevelUp();

                sfxManager.PlaySound("Explosion");
                Instantiate(LevelUpParticleEffect, playerController.transform.position, Quaternion.Euler(Vector3.zero));
            }
        }

        public void LevelUp()
        {
            currentLevel++;

        }

        public void AddExperience(int experience)
        {
            currentExp += experience;
            CurrentHp = HPLevels[currentLevel];
            CurrentMana = ManaLevels[currentLevel];
            CurrentAttack = AttackLevels[currentLevel];
            CurrendDefense = DefenseLevels[currentLevel];

            playerHealthManager.playerCurrentHealth = CurrentHp;
            playerHealthManager.playerMaxHealth = CurrentHp;

            playerManaManger.CurrentMana = CurrentMana;
            playerManaManger.MaxMana = CurrentMana;



        }
    }
}
