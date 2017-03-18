using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ActionRPG
{
    public class PlayerItemManager : MonoBehaviour
    {


        public Text cointText;
        public Text healthPotionText;
        public int HealthPotionCount;
        public int CoinCount;
        public GameObject text;

        // Use this for initialization
        void Start()
        {
            CoinCount = 0;
            HealthPotionCount = 0;
        }

        // Update is called once per frame
        void Update()
        {
            cointText.text = CoinCount.ToString();
            healthPotionText.text = HealthPotionCount.ToString();
        }
    }
}
