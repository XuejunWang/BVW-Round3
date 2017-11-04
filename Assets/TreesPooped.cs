using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesPooped : MonoBehaviour {

    [SerializeField] private MeshRenderer m_meshRenderer;
    [SerializeField] private Texture m_textureToChange;
    private string m_poopTag = "Poop";


    // Use this for initialization
    void Start () {
        if (!m_meshRenderer)
        {
            m_meshRenderer = GetComponent<MeshRenderer>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger Enter");
        if (other.tag == m_poopTag)
        {
            //Debug.Log(gameObject + "pooped");
            //change texture
            m_meshRenderer.material.mainTexture = m_textureToChange;
        }
    }
}
