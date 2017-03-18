using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{
    public class OpenShopUI : MonoBehaviour
    {

        public GameObject ShopUi;
        private bool screenActive;

        // Use this for initialization
        void Start()
        {
            screenActive = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.KeypadEnter))
            {
                screenActive = !screenActive;

                this.gameObject.SetActive(screenActive);
            }

        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (Input.GetKeyUp(KeyCode.KeypadEnter))
                {
                    screenActive = true;

                    if (transform.parent != null)
                    {
                        if (transform.parent.GetComponent<NPCController>() != null)
                        {
                            transform.parent.GetComponent<NPCController>().canMove = false;
                        }
                    }

                }
            }
        }


    }
}
