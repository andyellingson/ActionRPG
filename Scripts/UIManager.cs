using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ActionRPG
{
    public class UIManager : MonoBehaviour
    {

        public Slider healthBar;
        public Slider manaBar;
        public Text HPText;
        public Text LevelText;
        public PlayerHealthManager playerHealthManager;
        public PlayerManaManager playerManaManager;

        private PlayerStatManager playerStatManager;
        private static bool uiExists;


        // Use this for initialization
        void Start()
        {

            if (!uiExists)
            {
                uiExists = true;
                DontDestroyOnLoad(transform.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            playerStatManager = GetComponent<PlayerStatManager>();
            playerHealthManager = FindObjectOfType<PlayerHealthManager>();

        }

        // Update is called once per frame
        void Update()
        {
            healthBar.maxValue = playerHealthManager.playerMaxHealth;
            healthBar.value = playerHealthManager.playerCurrentHealth;
            manaBar.maxValue = playerManaManager.MaxMana;
            manaBar.value = playerManaManager.CurrentMana;
            LevelText.text = "Level: " + playerStatManager.currentLevel;
        }
    }
}
