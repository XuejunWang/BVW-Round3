using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooping : MonoBehaviour {

    [SerializeField]
    private GameObject m_objectToSpawn;
    [SerializeField]
    private string m_poopingTag = "Target";
    [SerializeField]
    private int m_spawnNumber = 10;
    [SerializeField]
    private Vector3 m_positionOffset;
    [SerializeField]
    private float m_poopSpeedRate = 4;
    private AudioSource m_audioSource;
    private AudioSource m_audioSource2;

    // Use this for initialization
    void Start () {
        m_audioSource = GetComponent<AudioSource>();
        m_audioSource2 = GetComponents<AudioSource>()[1];

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Spawn(Vector3 spawnPosition, Quaternion spawnRotation, Vector3 spawnVelocity)
    {
        GameObject gameObject = Instantiate(m_objectToSpawn, spawnPosition + m_positionOffset, spawnRotation);
        gameObject.GetComponent<Rigidbody>().velocity = spawnVelocity * m_poopSpeedRate;
        //print("poopVelocity:" + gameObject.GetComponent<Rigidbody>().velocity);

        m_audioSource.Play();
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
        if (other.gameObject.tag == m_poopingTag)
        {
            //Vector3 colliderVelocity = gameObject.GetComponent<Rigidbody>().velocity;
            //Transform colliderTransform = gameObject.transform;
            //Spawn(colliderTransform.position, colliderTransform.rotation, colliderVelocity);
            AudioClip circleClip = Resources.Load<AudioClip>("eating_fast");
            if (m_audioSource2 && circleClip)
            {
                m_audioSource2.clip = circleClip;
                m_audioSource2.Play();
            }
        }
    }
}
