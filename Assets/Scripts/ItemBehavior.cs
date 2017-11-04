using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour {
    public string BirdTag = "Bird";
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == BirdTag)
        {
            Debug.Log("collide with bird");
            DestroyObject(gameObject);
        }
    }
}
