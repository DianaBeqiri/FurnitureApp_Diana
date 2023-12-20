using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceIndicator : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    public GameObject indicator;
    //public GameObject PlaceBtn;
    //public GameObject shakePanel;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();


    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        indicator = transform.GetChild(0).gameObject;
        indicator.SetActive(false);
        //PlaceBtn.SetActive(false);
        //shakePanel.SetActive(true);

    }


    void Update()
    {
        var ray = new Vector2(Screen.width / 2, Screen.height / 2);

        if (raycastManager.Raycast(ray, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;

            transform.position = hitPose.position;
            transform.rotation = hitPose.rotation;

            if (!indicator.activeInHierarchy)
            {
                indicator.SetActive(true);
                //PlaceBtn.SetActive(true);
                //shakePanel.SetActive(false);

            }
        }
    }
}