using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovement : MonoBehaviour {

    [SerializeField]
    private Animator humanAnimator;
    [SerializeField]
    private AnimationClip idleAnimationClip;
    [SerializeField]
    private Transform[] walkCycleTargets;
    [SerializeField]
    private float humanWalkSpeed;

    private Transform currTarget;
    private int currTargetIndex;
    private int maxTargetIndex;
    private bool isWalking;


	void Start () {
        currTargetIndex = 0;
        maxTargetIndex = walkCycleTargets.Length - 1;
        currTarget = walkCycleTargets[currTargetIndex];

        transform.LookAt(currTarget);
        isWalking = true;
	}
	

	void Update () {

        if (Vector3.Distance(transform.position, currTarget.position) < 0.3)
        {
            //Debug.Log("About to pause human movement");

            // When the human character reaches its target,
            // we pause its movement and give it a new target to walk towards

            StartCoroutine(PauseHumanMovement());
            SetNewWalkCycleTarget();
        }

        if (isWalking)
        {
            //Debug.Log("Moving human forward");

            transform.Translate(Vector3.forward * humanWalkSpeed * Time.deltaTime);
        }
	}


    IEnumerator PauseHumanMovement()
    {
        isWalking = false;
        //humanAnimator.SetBool("isWalking", false);
        yield return new WaitForSeconds(idleAnimationClip.length);
        transform.LookAt(currTarget);
        //humanAnimator.SetBool("isWalking", true);
        isWalking = true;
    }


    void SetNewWalkCycleTarget()
    {
        if (currTargetIndex == maxTargetIndex)
        {
            // Set new target to be first target in array
            currTargetIndex = 0;
        }

        else
        {
            currTargetIndex++;
        }

        currTarget = walkCycleTargets[currTargetIndex];
    }
}
