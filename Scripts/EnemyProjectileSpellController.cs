using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ActionRPG
{
    public class EnemyProjectileSpellController : MonoBehaviour
    {


        public GameObject spell;
        public int spellSpeed;
        private Transform target;
        public string SpellSFXName;

        // Use this for initialization
        void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            FindObjectOfType<SFXManager>().Explosion.Play();
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, spellSpeed * Time.deltaTime);
        }




    }
}