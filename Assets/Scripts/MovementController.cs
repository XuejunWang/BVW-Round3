using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
    //Needed Ref
    [SerializeField]
    private GameObject m_trackerL;
    [SerializeField]
    private GameObject m_trackerR;
    [SerializeField]
    private GameObject m_head;
    [SerializeField]
    private Transform m_transformToMove;
    private Rigidbody m_rigidBody;
    private Pooping m_pooping;

    //Status to watch
    [SerializeField]
    private Vector3 m_trackerTransformPositionL;
    [SerializeField]
    private Vector3 m_trackerTransformPositionR;
    [SerializeField]
    private Vector3 m_trackerTransformRotationL;
    [SerializeField]
    private Vector3 m_trackerTransformRotationR;
    [SerializeField]
    private Vector3 m_velovity;

    //Paras for move
    //turn left/right
    //add a torque to the rigidbody to turn lefe/right
    //torque = a *(delta - deadzone)^2 
    [SerializeField]
    private float m_deltaY;
    [SerializeField]
    private float m_turnRate;
    [SerializeField]
    private float m_turningDeadZone = 0.10f;
    [SerializeField]
    private float m_turnMin = 0f;
    [SerializeField]
    private float m_turnMax = 1f;

    //go up/down
    //add a torque to the rigidbody to turn up/down
    //need test to see if force will be better
    //torque = a *(delta - deadzone)^2 
    [SerializeField]
    private float m_xRotation;
    [SerializeField]
    private float m_upRate;
    [SerializeField]
    private float m_rotationDeadZone = 0.10f;
    [SerializeField]
    private float m_rotationMin = 0f;
    [SerializeField]
    private float m_rotationMax = 45f;

    //Speed up/down
    //the speed is decided by rotation.x
    //delta will be clamp to [-0.5, 0.5]
    //velocity = a * e^delta + b  Towards transform.forward
    [SerializeField]
    private float m_deltaZ;
    [SerializeField]
    private float m_velocityRate;
    [SerializeField]
    private float m_velocityBias = 1f;
    [SerializeField]
    private float m_velocityDeadZone = 0.10f;
    [SerializeField]
    private float m_velocityMin = 0f;
    [SerializeField]
    private float m_velocityMax = 1f;

    [SerializeField]
    private float flapHeightShift;
    [SerializeField]
    private float flapSpeed;

    private float waistHeightShift;
    private float flapPrepareHeight;
    private float flapExecuteHeight;
    private float maxFlapExecuteTimeDelta = 6;  // Might need to modify this value as well
    private float flapPrepareTime;
    private bool isFlapPrepared = false;

    [SerializeField]
    private bool m_isPlaying = false;
    [SerializeField]
    private bool m_enteredPoopHeight = false;

    //paras to remember
    [SerializeField]
    private float m_originalHMDHeight;
    [SerializeField]
    private float m_chrouchPercent = 0.8f;

    // Use this for initialization
    void Start()
    {
        if (!m_transformToMove)
        {
            m_transformToMove = GetComponent<Transform>();
        }
        m_rigidBody = m_transformToMove.gameObject.GetComponent<Rigidbody>();
        m_pooping = GetComponent<Pooping>();
        m_rigidBody.velocity = m_transformToMove.forward * m_velocityBias;

        waistHeightShift = m_head.transform.position.y / 2;
        flapPrepareHeight = m_head.transform.position.y;
        flapExecuteHeight = m_head.transform.position.y - waistHeightShift;
        m_originalHMDHeight = m_head.transform.localPosition.y;
        //StartCoroutine(SetOriginalHMDHeight());
        //SetOriginalHMDHeight();
    }

    // Update is called once per frame
    void Update () {
        UpdateStatus();
        if (m_isPlaying)
        {
            UpdateTurning();
            UpdateVelocity();
            UpdateUpAndDown();
            //UpdateFlap();
            CheckForPoop();
        }
        else
        {
            m_rigidBody.velocity = transform.forward * 1f;
        }
    }

    IEnumerator SetOriginalHMDHeight()
    {
        yield return new WaitForSeconds(2f);
        m_originalHMDHeight = m_head.transform.localPosition.y;
    }

    void UpdateStatus()
    {
        m_trackerTransformPositionL = m_trackerL.transform.position;
        m_trackerTransformPositionR = m_trackerR.transform.position;
        m_trackerTransformRotationL = m_trackerL.transform.localEulerAngles;
        m_trackerTransformRotationR = m_trackerR.transform.localEulerAngles;

        m_deltaY = m_trackerL.transform.position.y - m_trackerR.transform.position.y;
        m_velovity = m_transformToMove.GetComponent<Rigidbody>().velocity;

        float tempL = 0;
        float tempR = 0;
        if (m_trackerTransformRotationL.x > 180)
        {
            tempL = m_trackerTransformRotationL.x - 360;
        }
        else
        {
            tempL = m_trackerTransformRotationL.x;
        }
        if (m_trackerTransformRotationR.x > 180)
        {
            tempR = m_trackerTransformRotationR.x - 360;
        }
        else
        {
            tempR = m_trackerTransformRotationR.x;
        }
        m_xRotation = (tempL + tempR) / 2;
    }

    void UpdateTurning()
    {
        //float deltaX = m_trackerL.transform.position.x - m_trackerR.transform.position.x;
        //float deltaZ = m_trackerL.transform.position.z - m_trackerR.transform.position.z;
        //m_rigidBody.AddTorque(transform.up * m_transformRate * m_deltaY);
        //m_rigidBody.AddForce(transform.up * m_transformRate * delta);

        if (Mathf.Abs(m_deltaY) > m_turningDeadZone)  
        {
            float turn;
            if (m_deltaY > 0)
            {
                turn = m_deltaY - m_turningDeadZone;
                turn = Mathf.Clamp(turn, m_turnMin, m_turnMax);
                turn = m_turnRate * turn * turn;
            }
            else
            {
                turn = m_deltaY + m_turningDeadZone;
                turn = Mathf.Clamp(turn, - m_turnMax, -m_turnMin);
                turn = - m_turnRate * turn * turn;
            }
            Vector3 torque = transform.up *  turn;
            m_rigidBody.AddTorque(torque, ForceMode.Impulse);
        }
    }

    void UpdateUpAndDown()
    {
        float rotation = 0;
        if (Mathf.Abs(m_xRotation) > m_rotationDeadZone)
        {
            if (m_xRotation > 0)
            {
                rotation = m_xRotation - m_rotationDeadZone;
                rotation = Mathf.Clamp(rotation, m_rotationMin, m_rotationMax);
                rotation = rotation * m_upRate;
            }
            else if (m_xRotation < 0)
            {
                rotation = m_xRotation + m_rotationDeadZone; 
                rotation = Mathf.Clamp(rotation, - m_rotationMax, - m_rotationMin);
                rotation = rotation * m_upRate;
            }
        }
        Vector3 velocity_y = -transform.up * rotation;
        m_rigidBody.velocity += velocity_y;
        //m_rigidBody.AddTorque(velocity_y);
    }

    //void UpdateFlap()
    //{
    //    flapPrepareHeight = m_head.transform.position.y;
    //    flapExecuteHeight = m_head.transform.position.y - waistHeightShift;

    //    if ((m_trackerL.transform.position.y >= flapPrepareHeight) && (m_trackerR.transform.position.y >= flapPrepareHeight))
    //    {
    //        flapPrepareTime = Time.time;
    //        isFlapPrepared = true;
    //    }

    //    else if ((m_trackerL.transform.position.y <= flapExecuteHeight) && (m_trackerR.transform.position.y <= flapExecuteHeight))
    //    {
    //        Debug.Log("isFlapPrepared: " + isFlapPrepared.ToString());

    //        if (isFlapPrepared && (Time.time - flapPrepareTime <= maxFlapExecuteTimeDelta))
    //        {
    //            StartCoroutine(RaiseFlightHeight());
    //        }

    //        isFlapPrepared = false;
    //    }
    //}

    void UpdateVelocity()
    {
        m_deltaZ = m_head.transform.localPosition.z - (m_trackerL.transform.localPosition.z + m_trackerR.transform.localPosition.z)/2;
        float speed = 0f;
        if (Mathf.Abs(m_deltaZ) > m_velocityDeadZone)
        {
            speed = m_deltaZ ;
            //print(speed);
            if (m_deltaZ > 0)
            {
                speed = Mathf.Clamp(speed, m_velocityMin, m_velocityMax);
            }
            else if(m_deltaZ < 0)
            {
                speed = Mathf.Clamp(speed, - m_velocityMax, - m_velocityMin);
            }
        }
        else
        {
            speed = 0f;
        }
        speed = m_velocityRate * Mathf.Exp(speed) + m_velocityBias;
        Vector3 velocity = transform.forward * speed;
        m_rigidBody.velocity = velocity;
    }

    //IEnumerator RaiseFlightHeight()
    //{
    //    Debug.Log("Coroutine starts");

    //    float startHeight = transform.position.y;
    //    float targetHeight = transform.position.y + flapHeightShift;
    //    float t = 0;

    //    while (true)
    //    {
    //        Debug.Log("Raising bird height");

    //        transform.position = new Vector3(transform.position.x, Mathf.Lerp(startHeight, targetHeight, t), transform.position.z);
    //        t += flapSpeed * Time.deltaTime;

    //        if (t > 1)
    //        {
    //            Debug.Log("Coroutine ends");
    //            break;
    //        }
    //        yield return null;
    //    }
    //}

    void CheckForPoop()
    {
        //Debug.Log("Checking head height");
        //Debug.Log(m_head.transform.localPosition.y);
        if (m_head.transform.localPosition.y < m_originalHMDHeight * m_chrouchPercent)
        {
            //Debug.Log("Enter Pooping Height");
            if (!m_enteredPoopHeight)
            {
                m_enteredPoopHeight = true;
                m_pooping.Spawn(m_head.transform.position, m_head.transform.rotation, m_rigidBody.velocity);
            }
        }
        else
        {
            m_enteredPoopHeight = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("ObTriggerEnter");
        if (other.gameObject.tag == "TutorialEnds")
        {
            //Debug.Log("Tutorials");
            m_isPlaying = true;
        }
    }
}
