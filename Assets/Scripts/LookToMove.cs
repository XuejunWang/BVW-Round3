using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToMove : MonoBehaviour {

    [SerializeField] private SteamVR_TrackedObject m_trackerL;
    [SerializeField] private SteamVR_TrackedObject m_trackerR;
    [SerializeField] private SteamVR_TrackedObject m_head;
    [SerializeField] private Transform m_transformToMove;
    [SerializeField] private float m_transformRate;
    [SerializeField] private float m_deadZone = 0.10f;
    private Rigidbody m_rigidBody;
    [SerializeField] float m_velocity = 1f;

    // Use this for initialization
    void Start () {
        if (!m_transformToMove)
        {
            m_transformToMove = GetComponent<Transform>();
        }
        m_rigidBody = m_transformToMove.gameObject.GetComponent<Rigidbody>();
        m_rigidBody.velocity = Vector3.forward * m_velocity;
    }
	
	// Update is called once per frame
	void Update () {
        // m_rigidbody.velocity = m_head.forward * m_velocity;
    }
}
