using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG

{
    public class ChannelSpellController : MonoBehaviour
    {

        public List<ParticleSystem> particleEffects;
        public List<ParticleCollisionEvent> collisionEvents;
        public GameObject channelSpell;        

        // Use this for initialization
        void Start()
        {
            Instantiate(channelSpell, FindObjectOfType<PlayerController>().transform.position, Quaternion.Euler(Vector3.zero));

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
