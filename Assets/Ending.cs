using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour {
    [SerializeField] private float m_timeToShine = 120f;
    private GlowObjectCmd m_glow;
    private bool m_isEnding = false;
    private string m_sceneName = "Credits";

	// Use this for initialization
	void Start () {
        m_glow = GetComponent<GlowObjectCmd>();
        if (m_glow)
        {
            m_glow.enabled = false;
        }
        StartCoroutine(WaitTheTowerToShine());

    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator WaitTheTowerToShine()
    {
        yield return new WaitForSeconds(m_timeToShine);

        if (m_glow)
        {
            m_glow.enabled = true;
            m_glow.GlowColor = Color.yellow;
        }
        if (gameObject.tag != "Environment")
        {
            gameObject.transform.localScale = new Vector3(20, 20, 20);
        }
        m_isEnding = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_isEnding)
        {
            if (other.gameObject.tag == "Bird")
            {
                ShowCredits();
            }
        }
    }

    private void ShowCredits()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(m_sceneName);
    }
}
