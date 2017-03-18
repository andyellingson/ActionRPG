using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{


    public class StatusEffectManager : MonoBehaviour
    {
        public GameObject BurnStatusEffect;
        public GameObject FreezeStatusEffect;
        public GameObject StunnedStatusEffect;
        public GameObject HasteStatusEffect;

        private static bool statusEffectManagerExists;

        // Use this for initialization
        void Start()
        {
            //if (!statusEffectManagerExists)
            //{
            //    statusEffectManagerExists = true;
            //    DontDestroyOnLoad(transform.gameObject);
            //}
            //else
            //{
            //    Destroy(gameObject);
            //}

        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}