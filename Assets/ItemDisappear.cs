using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisappear : MonoBehaviour {

    private GlowObjectCmd m_glow;
    private AudioSource m_audioSource;
    private AudioSource[] m_audioSources;
    private float m_stopDistance = 1.5f;

    // Use this for initialization
    void Start () {
        m_glow = GetComponent<GlowObjectCmd>();
        if (m_glow)
        {
            m_glow.GlowColor = Color.yellow;
        }
        m_audioSource = GetComponent<AudioSource>();
        m_audioSources = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        m_audioSource.Play();

        if (gameObject.tag == "TutorialEnds")
        {
            if (m_audioSources.Length > 1)
            {
                if (m_audioSources[1].isPlaying)
                {
                    m_audioSources[1].Stop();
                }
            }
        }
        if (m_glow)
        {
            m_glow.GlowColor = Color.black;
            m_glow.enabled = false;
        }
        gameObject.SetActive(false);
    }
}
