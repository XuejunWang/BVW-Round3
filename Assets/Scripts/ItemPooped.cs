using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPooped : MonoBehaviour {


    private Rigidbody m_rigibody;
    [SerializeField] private SkinnedMeshRenderer m_meshRenderer;
    [SerializeField] private Texture m_textureToChange;
    [SerializeField] private string[] m_actionAnimTrigger;
    [SerializeField] private string[] m_reactionAudioClip;
    private Animator m_animator;
    private AudioSource m_audioSource;
    private string m_poopTag = "Poop";

	// Use this for initialization
	void Start () {
        m_audioSource = GetComponent<AudioSource>();
        m_animator = GetComponent<Animator>();
        //int j = Random.Range(0, m_reactionAudioClip.Length);
        //AudioClip audioClipPop = Resources.Load<AudioClip>(m_reactionAudioClip[j]);
        //m_audioSource.clip = audioClipPop;
        //if (m_audioSource.clip)
        //{
        //    m_audioSource.Play();
        //}
        //load sound for 
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.collider.tag == m_poopTag)
    //    {
    //        Debug.Log(gameObject + "pooped");
    //        //change texture
    //        m_meshRenderer.material.mainTexture = m_textureToChange;

    //        //sound
    //        int j = Random.Range(0, m_reactionAudioClip.Length);
    //        AudioClip audioClipPop = Resources.Load<AudioClip>(m_reactionAudioClip[j]);
    //        m_audioSource.clip = audioClipPop;
    //        if (!m_audioSource.isPlaying)
    //        {
    //            m_audioSource.Play();
    //        }

    //        //animation
    //        int i = Random.Range(0, m_actionAnimTrigger.Length);
    //        m_animator.SetTrigger(m_actionAnimTrigger[i]);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger Enter");
        if (other.tag == m_poopTag)
        {
            //Debug.Log(gameObject + "pooped");
            //change texture
            m_meshRenderer.material.mainTexture = m_textureToChange;

            //sound
            int j = Random.Range(0, m_reactionAudioClip.Length);
            AudioClip audioClipPop = Resources.Load<AudioClip>(m_reactionAudioClip[j]);
            m_audioSource.clip = audioClipPop;
            if (m_audioSource.clip)
            {
                if (!m_audioSource.isPlaying)
                {
                    m_audioSource.Play();
                }
            }
                
            //animation
            int i = Random.Range(0, m_actionAnimTrigger.Length);
            //Debug.Log(m_actionAnimTrigger[i]);
            m_animator.SetTrigger(m_actionAnimTrigger[i]);
        }
    }
}
