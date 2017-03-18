using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ActionRPG
{
    public class LoadNewArea : MonoBehaviour
    {

        public string SceneToLoad;
        public string exitPoint;

        private PlayerController player;

        // Use this for initialization
        void Start()
        {
            player = FindObjectOfType<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == "Player")
            {
                SceneManager.LoadScene(SceneToLoad);
                player.startPoint = exitPoint;
            }
        }

    }
}
