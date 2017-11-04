using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject m_objectToSpawn;
    [SerializeField]
    private int m_spawnNumber = 10;
    [SerializeField]
    private Vector3 m_positionOffset;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    void Spawn(Transform spawnTransform, Vector3 spawnVelocity)
    {
        GameObject gameObject = Instantiate(m_objectToSpawn, spawnTransform.position + m_positionOffset, spawnTransform.rotation);
        gameObject.GetComponent<Rigidbody>().velocity = spawnVelocity * 3f;
        //print("poopVelocity:" + gameObject.GetComponent<Rigidbody>().velocity);

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.tag == "Bird")
    //    {
    //        Vector3 colliderVelocity =  collision.collider.gameObject.GetComponent<Rigidbody>().velocity;
    //        Transform colliderTransform = collision.collider.gameObject.transform;
    //        Spawn(colliderTransform, colliderVelocity);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger Enter");
        //print(other.gameObject.tag);
        if (other.gameObject.tag == "Bird")
        {
            Vector3 colliderVelocity = other.gameObject.GetComponent<Rigidbody>().velocity;
            //print("colliderVelocity:" + colliderVelocity);
            //colliderVelocity = Vector3.zero;
            Transform colliderTransform = other.gameObject.gameObject.transform;
            Spawn(colliderTransform, colliderVelocity);
        }
    }
}
