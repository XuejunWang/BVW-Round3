using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopBehavior : MonoBehaviour {

    private AudioSource m_audioSource;
    private bool m_isSoundPlayer = false;

	// Use this for initialization
	void Start () {
        m_audioSource = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!m_isSoundPlayer)
        {
            if (!m_audioSource.isPlaying)
            {
                m_audioSource.Play();
                m_isSoundPlayer = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {


        if (!m_audioSource)
        {
            if (!m_audioSource.isPlaying)
            {
                m_audioSource.Play();
                m_isSoundPlayer = true;
            }
        }
    }
}
