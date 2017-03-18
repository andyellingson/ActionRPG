using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{
    public class PlayerManaManager : MonoBehaviour
    {

        public int MaxMana;
        public int CurrentMana;
        public float regenTimer;
        private float regenTimerCounter;

        // Use this for initialization
        void Start()
        {
            regenTimerCounter = regenTimer;
            CurrentMana = MaxMana;
        }

        // Update is called once per frame
        void Update()
        {
            regenTimerCounter -= Time.deltaTime;

            if (CurrentMana < 0) CurrentMana = 0;

            if (CurrentMana < MaxMana && regenTimerCounter <= 0)
            {
                regenTimerCounter = regenTimer;
                CurrentMana += 1;
            }
        }

        public void SpendMana(int mana)
        {
            CurrentMana -= mana;
        }

        public void HealPlayer(int manaAmount)
        {
            CurrentMana += manaAmount;
            if (CurrentMana > MaxMana)
                CurrentMana = MaxMana;
        }

    }
}