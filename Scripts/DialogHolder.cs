using ActionRPG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{
    public class DialogHolder : MonoBehaviour
    {

        //public string Dialog;
        private DialogManager dialogManager;
        public GameObject dialogWindow;
        private GameObject currentDialogWindow;
        private bool dialogWindowIsOpen;

        public string[] DialogLines;

        public bool PlayerInColliderZone
        {
            get;
            private set;
        }

        // Use this for initialization
        void Start()
        {
            PlayerInColliderZone = false;
            dialogManager = FindObjectOfType<DialogManager>();
            dialogWindowIsOpen = false;
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyUp(KeyCode.KeypadEnter) && DialogLines.Length > 0 && PlayerInColliderZone)
            {
                //dialogManager.ShowDialogBox(Dialog);
                if (!dialogManager.DialogActive)
                {
                    dialogManager.DialogActive = true;
                    dialogManager.DialogLines = DialogLines;
                    dialogManager.currentLine = 0;
                    dialogManager.ShowDialog();
                }

                if (transform.parent.GetComponent<NPCController>() != null)
                {
                    transform.parent.GetComponent<NPCController>().canMove = false;
                }

            }

            if (Input.GetKeyDown(KeyCode.Y) && PlayerInColliderZone)
            {
                if (!dialogWindowIsOpen)
                {
                    dialogWindowIsOpen = true;
                    currentDialogWindow = Instantiate(dialogWindow, FindObjectOfType<PlayerController>().transform);
                }
                else
                {
                    dialogWindowIsOpen = false;
                    Destroy(currentDialogWindow);
                }

            }

            PlayerInColliderZone = false;

        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                PlayerInColliderZone = true;
            }

           

        }


    }
}
