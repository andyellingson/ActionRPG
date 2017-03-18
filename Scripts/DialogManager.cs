using ActionRPG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ActionRPG
{
    public class DialogManager : MonoBehaviour
    {
        public GameObject DialogBox;
        public GameObject ShopUI;

        public Text DialogText;
        public bool DialogActive;

        public string[] DialogLines;
        public int currentLine;

        private bool ShopWindowOpen;
        private GameObject currentShopWindow;

        // Use this for initialization
        void Start()
        {
            ShopWindowOpen = false;
        }

        // Update is called once per frame
        void Update()
        {
            //if(DialogLines != null)
            //if(DialogLines.Length > 0 && currentLine < DialogLines.Length)
            //    DialogText.text = DialogLines[currentLine];

            if (DialogActive && Input.GetKeyDown(KeyCode.KeypadEnter))
            {

               if (currentLine >= DialogLines.Length)
                {
                    DialogBox.SetActive(false);
                    DialogActive = false;
                    currentLine = 0;
                }
                try
                {
                    DialogText.text = String.Format(DialogLines[currentLine++] + "\n" + DialogLines[currentLine++] + "\n" + DialogLines[currentLine++]);
                }
                catch(Exception ex)
                {
                    print("DialogManager: You have to add three lines of text for each dialog window");
                }
            }
        }

        public void ShowDialogBox(string dialog)
        {
            DialogActive = true;
            DialogBox.SetActive(true);
        }

        public void ShowDialog()
        {
            //load first window of text
            try
            {
                DialogText.text = String.Format(DialogLines[currentLine++] + "\n" + DialogLines[currentLine++] + "\n" + DialogLines[currentLine++]);
            }
            catch (Exception ex)
            {
                print("DialogManager: You have to add three lines of text for each dialog window");
            }
            DialogActive = true;
            DialogBox.SetActive(true);
        }
    }
}