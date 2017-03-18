using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{
    public class ItemDropController : MonoBehaviour
    {

        private SFXManager sfxManager;
        public int ItemsToDrop;
        public List<GameObject> Items;

        // Use this for initialization
        void Start()
        {
            sfxManager = FindObjectOfType<SFXManager>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DropItems()
        {

            var location = gameObject.transform.position;

            sfxManager.CoinPickup.Play();

            Quaternion spawnRotation = Quaternion.Euler(Vector3.zero);
            Vector3 newPosition = new Vector3();

            while (ItemsToDrop > 0)
            {
                int direction = Random.Range(1, 8);
                switch (direction)
                {

                    case 1:     //UP
                        newPosition = new Vector3(transform.position.x, transform.position.y + 1f);
                        break;
                    case 2:     //UpRight
                        newPosition = new Vector3(transform.position.x + 1f, transform.position.y + 1f);
                        break;
                    case 3:     //Right
                        newPosition = new Vector3(transform.position.x + 1f, transform.position.y);
                        break;
                    case 4:     //DownRight
                        newPosition = new Vector3(transform.position.x + 1f, transform.position.y - 1f);
                        break;
                    case 5:     //Down
                        newPosition = new Vector3(transform.position.x, transform.position.y - 1f);
                        break;
                    case 6:     //DownLeft
                        newPosition = new Vector3(transform.position.x - 1f, transform.position.y - 1f);
                        break;
                    case 7:     //Left
                        newPosition = new Vector3(transform.position.x - 1f, transform.position.y);
                        break;
                    case 8:     //UpLeft
                        newPosition = new Vector3(transform.position.x - 1f, transform.position.y + 1f);
                        break;

                }
                ItemsToDrop--;
                Instantiate(Items[Random.Range(0, Items.Count - 1)], newPosition, spawnRotation);
            }
        }
    }
}