using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopTerrainCollision : MonoBehaviour {

    [SerializeField]
    private GameObject poopSplatterPrefab;
    [SerializeField]
    private float outerPoopCenterShift;
    [SerializeField]
    private float minOuterPoopScale;
    [SerializeField]
    private float maxOuterPoopScale;

    private const string TERRAIN_TAG = "Environment";
    private float poopSpawnHeight;
    private GameObject upperLeftSplatter;
    private GameObject upperRightSplatter;
    private GameObject lowerLeftSplatter;
    private GameObject lowerRightSplatter;
    private float upperLeftSplatterScale;
    private float upperRightSplatterScale;
    private float lowerLeftSplatterScale;
    private float lowerRightSplatterScale;

    private bool m_pooped;


    /*
    void Start()
    {
        upperLeftSplatterScale = Random.Range(minOuterPoopScale, maxOuterPoopScale);
        upperRightSplatterScale = Random.Range(minOuterPoopScale, maxOuterPoopScale);
        lowerLeftSplatterScale = Random.Range(minOuterPoopScale, maxOuterPoopScale);
        lowerRightSplatterScale = Random.Range(minOuterPoopScale, maxOuterPoopScale);
    }
    */


    void OnCollisionEnter(Collision collision)
    {
        if (!m_pooped)
        {
            if (collision.gameObject.tag == TERRAIN_TAG)
            {
                // Before spawning any poop splatters, we get the height at which we want to spawn them (need to increase this value to prevent clipping?)
                poopSpawnHeight = (transform.position.y - (transform.localScale.y / 2)) + 0.01f;
                SpawnPoopSplatter();
                //Destroy(gameObject);
                m_pooped = true;
            }
        }
    }


    void SpawnPoopSplatter()
    {
        // Instantiate first poop splatter

        Instantiate(poopSplatterPrefab, new Vector3(transform.position.x, poopSpawnHeight, transform.position.z), poopSplatterPrefab.transform.rotation);

        // Instantiate remaining four poop prefabs, making sure to offset them from the original splatter's center

        upperLeftSplatter = Instantiate(poopSplatterPrefab, new Vector3(transform.position.x - outerPoopCenterShift, poopSpawnHeight, transform.position.z + outerPoopCenterShift), poopSplatterPrefab.transform.rotation);
        upperRightSplatter = Instantiate(poopSplatterPrefab, new Vector3(transform.position.x + outerPoopCenterShift, poopSpawnHeight, transform.position.z + outerPoopCenterShift), poopSplatterPrefab.transform.rotation);
        lowerLeftSplatter = Instantiate(poopSplatterPrefab, new Vector3(transform.position.x - outerPoopCenterShift, poopSpawnHeight, transform.position.z - outerPoopCenterShift), poopSplatterPrefab.transform.rotation);
        lowerRightSplatter = Instantiate(poopSplatterPrefab, new Vector3(transform.position.x + outerPoopCenterShift, poopSpawnHeight, transform.position.z - outerPoopCenterShift), poopSplatterPrefab.transform.rotation);

        // Finally, we re-size the outer splatters randomly to be between 1/2 and 3/4 of the original splatter's size

        /*
        upperLeftSplatter.transform.localScale = new Vector3(transform.localScale.x * upperLeftSplatterScale, transform.localScale.y * upperLeftSplatterScale, transform.localScale.z * upperLeftSplatterScale);
        upperRightSplatter.transform.localScale = new Vector3(transform.localScale.x * upperRightSplatterScale, transform.localScale.y * upperRightSplatterScale, transform.localScale.z * upperRightSplatterScale);
        lowerLeftSplatter.transform.localScale = new Vector3(transform.localScale.x * lowerLeftSplatterScale, transform.localScale.y * lowerLeftSplatterScale, transform.localScale.z * lowerLeftSplatterScale);
        lowerRightSplatter.transform.localScale = new Vector3(transform.localScale.x * lowerRightSplatterScale, lowerRightSplatter.transform.localScale.y, transform.localScale.z * lowerRightSplatterScale);
        */
    }
}
