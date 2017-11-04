using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLookToPlayer : MonoBehaviour {

    [SerializeField]
    private float m_speed = 3.0f;
    [SerializeField]
    private float m_stopDistance = 3f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(GameObject.Find("Bird").transform.position, transform.position);
        if (distance >= m_stopDistance)
        {
            Vector3 dir = transform.position - GameObject.Find("Bird").transform.position;
            dir.y = 0;
            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, m_speed * Time.deltaTime * m_speed);
        }
    }
}
