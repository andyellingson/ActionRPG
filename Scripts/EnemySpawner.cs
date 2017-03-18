using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{
    public class EnemySpawner : MonoBehaviour
    {
        public List<GameObject> EnemiesToSpawn;

        public int EnemiesLeftToSpawn;

        public float timeBetweenSpawn;
        private float timeBetweenSpawnCounter;

        public float timeToSpawn;
        private float timeToSpawnCounter;

        public bool spawning;

        private Animator spawningObjectAnimator;


        // Use this for initialization
        void Start()
        {
            spawningObjectAnimator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (spawning && EnemiesLeftToSpawn > 0)
            {

                timeToSpawnCounter -= Time.deltaTime;

                if (timeToSpawnCounter < 0F)
                {
                    spawning = false;

                    if (spawningObjectAnimator != null)
                        spawningObjectAnimator.SetBool("Spawning", false);

                    if (EnemiesToSpawn.Count > 1)
                        Instantiate(EnemiesToSpawn[Random.Range(0, EnemiesToSpawn.Count - 1)], transform.position, transform.rotation);
                    else
                        Instantiate(EnemiesToSpawn[0], transform.position, transform.rotation);

                    EnemiesLeftToSpawn--;
                    timeBetweenSpawnCounter = Random.Range(timeBetweenSpawn * 0.75f, timeBetweenSpawn * 1.25f);
                }
            }
            else
            {
                timeBetweenSpawnCounter -= Time.deltaTime;

                if (timeBetweenSpawnCounter < 0f)
                {
                    spawning = true;

                    if (spawningObjectAnimator != null)
                        spawningObjectAnimator.SetBool("Spawning", true);

                    timeToSpawnCounter = Random.Range(timeToSpawn * 0.75f, timeToSpawn * 1.25f);
                }

            }

            if (EnemiesLeftToSpawn == 0) Destroy(gameObject);

        }
    }
}