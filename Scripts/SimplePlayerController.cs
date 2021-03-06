﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{
    public class SimplePlayerController : MonoBehaviour {

    public float Speed;

    public Inventory inventory;

	// Use this for initialization
	void Start ()

    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleMovement();
	}

    private void HandleMovement()
    {
        float translation = Speed * Time.deltaTime;

        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * translation, 0, Input.GetAxis("Vertical")));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            inventory.AddItem(other.GetComponent<Item>());
        }
    }

}
}
