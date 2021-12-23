using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneDetection : MonoBehaviour
{
    ARRaycastManager raycastManager;
    [SerializeField] GameObject obj;
    AudioSource audioSource;

    public AudioClip sound1;

    // Start is called before the first frame update
    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = sound1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended || obj == null)
        {
            return;
        }

        var hits = new List<ARRaycastHit>();
        if(raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.Planes))
        {
            var hitPose = hits[0].pose;
            Instantiate(obj , hitPose.position, hitPose.rotation);
            audioSource.Play();
        }
    }
}
