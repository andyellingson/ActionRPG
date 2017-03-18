using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ActionRPG
{
    public class FloatingDamageNumbers : MonoBehaviour
    {

        public float moveSpeed;
        public int damage;
        public Text displayNumberTextBox;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (displayNumberTextBox != null)
                displayNumberTextBox.text = "" + damage;
            else
                print("FLOATINGDAMAGENUMBERS: NO TEXT ASSIGNED!!!");

            transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), transform.position.z);
        }
    }
}