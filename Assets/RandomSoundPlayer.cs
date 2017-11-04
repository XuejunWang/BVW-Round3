using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour {
    private AudioSource m_audioSource;
    [SerializeField] int m_rangeStarts = 15;
    [SerializeField] int m_rangeEnds = 20;
    [SerializeField] string[] m_SoundToChoose;
    private bool m_isWaiting = false;

    // Use this for initialization
    void Start () {
        m_audioSource = GetComponents<AudioSource>()[1];
	}
	
	// Update is called once per frame
	void Update () {
        if (!m_isWaiting)
        {
            StartCoroutine(PlaySoundAtRandomSound());
        }
	}

    IEnumerator PlaySoundAtRandomSound()
    {
        int i = Random.Range(m_rangeStarts, m_rangeEnds);
        m_isWaiting = true;
        yield return new WaitForSeconds(i);
        int j = Random.Range(0, m_SoundToChoose.Length);
        AudioClip clip = Resources.Load<AudioClip>(m_SoundToChoose[j]);
        m_audioSource.clip = clip;
        m_audioSource.Play();
        m_isWaiting = false;
    }
}
