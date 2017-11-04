using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditSceneMovementController : MonoBehaviour {

    private Rigidbody m_rigidBody;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_time;
    private AudioSource m_audioSource;


    // Use this for initialization
    void Start () {
        m_rigidBody = GetComponent<Rigidbody>();
        m_audioSource = GetComponent<AudioSource>();
        m_rigidBody.velocity = Vector3.forward * m_speed;
        StartCoroutine(Stop());

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(m_time);
        m_rigidBody.velocity = Vector3.zero;
        m_audioSource.Play();
    }
}
