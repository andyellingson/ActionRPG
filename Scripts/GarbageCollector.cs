using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{
    public class GarbageCollector : MonoBehaviour {

    public float CollectionTimer;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        CollectionTimer -= Time.deltaTime;
        if(CollectionTimer < 0)
        {
            Destroy(gameObject);
        }        	
	}
}
}
