using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingFlapController : MonoBehaviour {

    // Is the name of this script okay?

    // Do I really need a variable for tracking whether or not both the guests' wings are held down?

    // This script could go on the CameraRig
    // Could references to the Vive trackers through public game objects and use their position/height/velocity to move the CameraRig

    [SerializeField]
    private GameObject hmd;
    [SerializeField]
    private GameObject leftWing;
    [SerializeField]
    private GameObject rightWing;
    [SerializeField]
    private float flapHeightShift;
    [SerializeField]
    private float flapSpeed;

    private float waistHeightShift;

    // The following two variables keep track of whether or not a) the guest is holding both wings above flapPrepareHeight
    // and b) the guest is holding both wings below flapExecuteHeight (respectively)

    private float flapPrepareHeight;    // Is this a good name for this variable?
    private float flapExecuteHeight;

    // The following variable holds the maximum amount of time the world will
    // allow the guest to hold their wings above their head and bring them down
    // in order to execute a flap

    private float maxFlapExecuteTimeDelta;

    // The following variable holds the time at which the guest raises both wings above flapPrepareHeight

    private float flapPrepareTime;
    private bool isFlapPrepared;
    // private bool isHoldingWingsDown;


    void Start () {

        // Now, we need to calculate an appropriate flapPrepareHeight and an appropriate flapExecuteHeight,
        // depending on what the guest's height is

        // Note: we might need to modify these values so that flapping feels good for at least a vast majority
        // of the people who will be playing through this world (also, if we're setting these values in update, do we need to set them here?)

        waistHeightShift = hmd.transform.position.y / 2;
        flapPrepareHeight = hmd.transform.position.y;
        flapExecuteHeight = hmd.transform.position.y - waistHeightShift;

        maxFlapExecuteTimeDelta = 6;    // Might need to modify this value as well

        isFlapPrepared = false;
        // isHoldingWingsDown = false;
	}
	

	void Update () {

        // Since the height of the guest's head will be shifting over the course of the experience,
        // we need to calculate an appropriate flapPrepareHeight and an appropriate flapExecuteHeight in Update

        // Note: we might need to modify these values so that flapping feels good for at least a vast majority
        // of the people who will be playing through this world

        flapPrepareHeight = hmd.transform.position.y;
        flapExecuteHeight = hmd.transform.position.y - waistHeightShift;

        if ((leftWing.transform.position.y >= flapPrepareHeight) && (rightWing.transform.position.y >= flapPrepareHeight))
        {
            // Need to store the time at which the guest raise both of their wings above flapPrepareHeight

            flapPrepareTime = Time.time;
            isFlapPrepared = true;
        }

         else if ((leftWing.transform.position.y <= flapExecuteHeight) && (rightWing.transform.position.y <= flapExecuteHeight))
        {
            // Check if wings were held up previously and if the delta between the current time and the time at which
            // the guest raised their wings is low enough

            //Debug.Log("isFlapPrepared: " + isFlapPrepared.ToString());

            if (isFlapPrepared && (Time.time - flapPrepareTime <= maxFlapExecuteTimeDelta))
            {
                StartCoroutine(RaiseFlightHeight());
            }

            isFlapPrepared = false;
        }
    }


    IEnumerator RaiseFlightHeight ()
    {
        //Debug.Log("Coroutine starts");

        float startHeight = transform.position.y;
        float targetHeight = transform.position.y + flapHeightShift;
        float t = 0;

        while (true)
        {
            //Debug.Log("Raising bird height");

            transform.Translate(0f, (targetHeight - startHeight) * flapSpeed * Time.deltaTime, 0f);
            //transform.position = new Vector3(transform.position.x, Mathf.Lerp(startHeight, targetHeight, t), transform.position.z);
            t += flapSpeed * Time.deltaTime;
            
            if (t > 1)
            {
                //Debug.Log("Coroutine ends");
                break;
            }

            yield return null;
        }
    }
}
