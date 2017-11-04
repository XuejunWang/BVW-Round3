using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorDisappear : MonoBehaviour {

    private GlowObjectCmd m_glow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bird")
        {
            if (m_glow)
            {
                m_glow.GlowColor = Color.black;
                m_glow.enabled = false;
            }
            gameObject.SetActive(false);
            Destroy(gameObject);
        }        
    }
}
