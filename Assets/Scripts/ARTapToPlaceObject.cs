using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject ghost;
    public GameObject objectToPlace;

    /*
    // furniture
    public GameObject chair;
    public GameObject table;
    public GameObject pouf;
    public GameObject shelf;
    public GameObject sofa;
    public GameObject sofa1;
    public GameObject sofa2;
    public GameObject bed2;
    public GameObject table2;
    public GameObject table3;
    public GameObject chairrr;
    public GameObject eggchair;
    public GameObject bedNtok;
    public GameObject tableGat;
    public GameObject carpet11;
    public GameObject carpet12;
    */
    public GameObject confirmBtn;
    public GameObject rotateBtn;
    //public GameObject MoveBtn;
    public GameObject DeleteBtn;
    public GameObject placeBtn;

    
    private Pose PlacementPose; // contains a Vector3 for a position and a quaternion for rotation
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;
    private Transform indicator;
    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid) // works
        {
            placementIndicator.SetActive(true);
            confirmBtn.SetActive(true);

            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
            confirmBtn.SetActive(false);
        }
    }
    
    // does raycast to center of screen, looks for planes, and stores the results in hits.
    // then if there are hits, set placementPose to that pose.
    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;
        }
    }

    // Next section
    public void PlaceObject()
    {
        confirmBtn.SetActive(false);

        if (placementPoseIsValid)
        {
            ghost.GetComponent<Recolour>().SetOriginalMaterial();
            ghost.transform.parent = null;
            ghost = Instantiate(objectToPlace, PlacementPose.position, PlacementPose.rotation);
            ghost.GetComponent<Recolour>().SetValid();
            ghost.transform.parent = placementIndicator.transform;
            confirmBtn.SetActive(false);
            placementIndicator.SetActive(false);
            rotateBtn.SetActive(true);
            DeleteBtn.SetActive(true);
            //MoveBtn.SetActive(true);
            placeBtn.SetActive(true);

        }
    }
    
    private void UseObject(GameObject o)
    {
        objectToPlace = o;
        Destroy(ghost);
        ghost = Instantiate(o, PlacementPose.position, PlacementPose.rotation);
        ghost.GetComponent<Recolour>().SetValid();
        ghost.transform.parent = placementIndicator.transform;
    }

    public void ObjectFromServer1()
    {
         StartCoroutine(Object1FromServer());
        //StartCoroutine(Object1FromServer());
    }



    // Update is called once per frame
    private IEnumerator Object1FromServer()
    {
        GameObject go = null;
        string url = "https://drive.google.com/u/0/uc?id=1T3mjgAzTn7A7d7wTdiggK8H0hp__Z7t8&export=download";

        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("Error on the get request at: " + url + " " + www.error);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                go = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                bundle.Unload(false);
                yield return new WaitForEndOfFrame();
            }
            www.Dispose();
        }
        InstantiateGameObjectFromAssetBundle(go);
        UseObject(go);
    }

    private void InstantiateGameObjectFromAssetBundle(GameObject go)
    {
        if (go != null && placementIndicator != null)
        {
            Debug.Log("Indicator position: " + indicator.position);
            // Instantiate the prefab at the same position as the indicator.
            GameObject instanceGo = Instantiate(go, indicator.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Your asset bundle GameObject or indicator transform is null.");
        }
    }








    /* public void UseSofa2()
     {
         StartCoroutine(DownloadAssetBundleFromServer2());
     }*/
    /*
    public void UseChair()
    {
        UseObject(chair);
    }
    
    public void UseTable()
    {
        UseObject(table);
    }
    
    public void UsePouf()
    {
        UseObject(pouf);
    }
     public void UseBed2()
    {
        UseObject(bed2);
    }
    public void UseTable2()
    {
        UseObject(table2);
    }
    public void UseTable3()
    {
        UseObject(table3);
    }
    
    public void UseShelf()
    {
        UseObject(shelf);
    }
    
    public void UseSofa()
    {
        UseObject(sofa);
    }
    public void UseSofa1()
    {
        UseObject(sofa1);
    }

    

    public void UseSofa3()
    {
        UseObject(chairrr);
    }

    public void UseEggChair()
    {
        UseObject(eggchair);
    }


    public void UseBedNtok()
    {
        UseObject(bedNtok);
    }
    public void UseTableGat()
    {
        UseObject(tableGat);
    }
    public void Carpet1()
    {
        UseObject(carpet11);
    }
    public void Carpet2()
    {
        UseObject(carpet12);
    }
    */


    /*   private IEnumerator DownloadAssetBundleFromServer()
       {

           GameObject go = null;

           string url = "https://drive.google.com/u/0/uc?id=1hJGmRbM1QQnFIjWdDFdVlGL4L3jpNwF3&export=download";

           using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
           {
               yield return www.SendWebRequest();
               if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
               {
                   Debug.LogWarning("Errro on the get request at: " + url + "" + www.error);
               }
               else
               {
                   AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                   go = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                   bundle.Unload(false);
                   yield return new WaitForEndOfFrame();
               }


               www.Dispose();
           }
           InstantiateGameObjectFromAssetBundle(go);

       }

      *//* public void Place2()
       {

       }*//*
       private IEnumerator DownloadAssetBundleFromServer2()
       {

           GameObject go2 = null;

           string url2 = "https://drive.google.com/u/0/uc?id=1Xgn4RL6lbz0YU5A1uMDm1yKMndvdIXQI&export=download";

           using (UnityWebRequest www2 = UnityWebRequestAssetBundle.GetAssetBundle(url2))
           {
               yield return www2.SendWebRequest();
               if (www2.result == UnityWebRequest.Result.ConnectionError || www2.result == UnityWebRequest.Result.ProtocolError)
               {
                   Debug.LogWarning("Errro on the get request at: " + url2 + "" + www2.error);
               }
               else
               {
                   AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www2);
                   go2 = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                   bundle.Unload(false);
                   yield return new WaitForEndOfFrame();
               }


               www2.Dispose();
           }
           InstantiateGameObjectFromAssetBundle2(go2);
           UseObject(go2);

       }

       public void Place3()
       {
           StartCoroutine(DownloadAssetBundleFromServer3());
       }
       private IEnumerator DownloadAssetBundleFromServer3()
       {

           GameObject go3 = null;

           string url3 = "https://drive.google.com/u/0/uc?id=1oH88buBd9LH07n3napaINcd-GQMY_MOB&export=download";

           using (UnityWebRequest www3 = UnityWebRequestAssetBundle.GetAssetBundle(url3))
           {
               yield return www3.SendWebRequest();
               if (www3.result == UnityWebRequest.Result.ConnectionError || www3.result == UnityWebRequest.Result.ProtocolError)
               {
                   Debug.LogWarning("Errro on the get request at: " + url3 + "" + www3.error);
               }
               else
               {
                   AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www3);
                   go3 = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                   bundle.Unload(false);
                   yield return new WaitForEndOfFrame();
               }


               www3.Dispose();
           }
           InstantiateGameObjectFromAssetBundle3(go3);
       }

       public void Place4()
       {
           StartCoroutine(DownloadAssetBundleFromServer4());
       }
       private IEnumerator DownloadAssetBundleFromServer4()
       {

           GameObject go4 = null;

           string url4 = "https://drive.google.com/u/0/uc?id=1gUTUdS1P04PoHWcEaWS26QgX6gVtHOm0&export=download";

           using (UnityWebRequest www4 = UnityWebRequestAssetBundle.GetAssetBundle(url4))
           {
               yield return www4.SendWebRequest();
               if (www4.result == UnityWebRequest.Result.ConnectionError || www4.result == UnityWebRequest.Result.ProtocolError)
               {
                   Debug.LogWarning("Errro on the get request at: " + url4 + "" + www4.error);
               }
               else
               {
                   AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www4);
                   go4 = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                   bundle.Unload(false);
                   yield return new WaitForEndOfFrame();
               }


               www4.Dispose();
           }
           InstantiateGameObjectFromAssetBundle4(go4);
       }

       public void Place5()
       {
           StartCoroutine(DownloadAssetBundleFromServer5());
       }
       private IEnumerator DownloadAssetBundleFromServer5()
       {

           GameObject go5 = null;

           string url5 = "https://drive.google.com/u/0/uc?id=1VJGIjGumIO8DpnZwg1bSV3DuDnUlA1xG&export=download";

           using (UnityWebRequest www5 = UnityWebRequestAssetBundle.GetAssetBundle(url5))
           {
               yield return www5.SendWebRequest();
               if (www5.result == UnityWebRequest.Result.ConnectionError || www5.result == UnityWebRequest.Result.ProtocolError)
               {
                   Debug.LogWarning("Errro on the get request at: " + url5 + "" + www5.error);
               }
               else
               {
                   AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www5);
                   go5 = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                   bundle.Unload(false);
                   yield return new WaitForEndOfFrame();
               }


               www5.Dispose();
           }
           InstantiateGameObjectFromAssetBundle5(go5);
       }

       public void Place6()
       {
           StartCoroutine(DownloadAssetBundleFromServer6());
       }
       private IEnumerator DownloadAssetBundleFromServer6()
       {

           GameObject go6 = null;

           string url6 = "https://drive.google.com/u/0/uc?id=1AhlIEnL8GPrcIOEapd7-DfR7U_jdLn57&export=download";

           using (UnityWebRequest www6 = UnityWebRequestAssetBundle.GetAssetBundle(url6))
           {
               yield return www6.SendWebRequest();
               if (www6.result == UnityWebRequest.Result.ConnectionError || www6.result == UnityWebRequest.Result.ProtocolError)
               {
                   Debug.LogWarning("Errro on the get request at: " + url6 + "" + www6.error);
               }
               else
               {
                   AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www6);
                   go6 = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                   bundle.Unload(false);
                   yield return new WaitForEndOfFrame();
               }

               www6.Dispose();
           }
           InstantiateGameObjectFromAssetBundle6(go6);
       }


       public void Place7()
       {
           StartCoroutine(DownloadAssetBundleFromServer7());
       }
       private IEnumerator DownloadAssetBundleFromServer7()
       {

           GameObject go7 = null;

           string url7 = "https://drive.google.com/u/0/uc?id=1nYrbh2NKFffieCB_QivxI8UK_ZMlUfpl&export=download";

           using (UnityWebRequest www7 = UnityWebRequestAssetBundle.GetAssetBundle(url7))
           {
               yield return www7.SendWebRequest();
               if (www7.result == UnityWebRequest.Result.ConnectionError || www7.result == UnityWebRequest.Result.ProtocolError)
               {
                   Debug.LogWarning("Errro on the get request at: " + url7 + "" + www7.error);
               }
               else
               {
                   AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www7);
                   go7 = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                   bundle.Unload(false);
                   yield return new WaitForEndOfFrame();
               }

               www7.Dispose();
           }
           InstantiateGameObjectFromAssetBundle7(go7);
       }

       public void Place8()
       {
           StartCoroutine(DownloadAssetBundleFromServer8());
       }
       private IEnumerator DownloadAssetBundleFromServer8()
       {

           GameObject go8 = null;

           string url8 = "https://drive.google.com/u/0/uc?id=1OBs8BfyyG-uxe6hV1U7yTNQe3JC6Oj6C&export=download";

           using (UnityWebRequest www8 = UnityWebRequestAssetBundle.GetAssetBundle(url8))
           {
               yield return www8.SendWebRequest();
               if (www8.result == UnityWebRequest.Result.ConnectionError || www8.result == UnityWebRequest.Result.ProtocolError)
               {
                   Debug.LogWarning("Errro on the get request at: " + url8 + "" + www8.error);
               }
               else
               {
                   AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www8);
                   go8 = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                   bundle.Unload(false);
                   yield return new WaitForEndOfFrame();
               }

               www8.Dispose();
           }
           InstantiateGameObjectFromAssetBundle8(go8);
       }

       public void Place9()
       {
           StartCoroutine(DownloadAssetBundleFromServer9());
       }
       private IEnumerator DownloadAssetBundleFromServer9()
       {

           GameObject go9 = null;

           string url9 = "https://drive.google.com/u/0/uc?id=1HAxpzDsMzm2xCYs8I8I0mB36e6V_rhFu&export=download";

           using (UnityWebRequest www9 = UnityWebRequestAssetBundle.GetAssetBundle(url9))
           {
               yield return www9.SendWebRequest();
               if (www9.result == UnityWebRequest.Result.ConnectionError || www9.result == UnityWebRequest.Result.ProtocolError)
               {
                   Debug.LogWarning("Errro on the get request at: " + url9 + "" + www9.error);
               }
               else
               {
                   AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www9);
                   go9 = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                   bundle.Unload(false);
                   yield return new WaitForEndOfFrame();
               }

               www9.Dispose();
           }
           InstantiateGameObjectFromAssetBundle9(go9);
       }

       public void Place10()
       {
           StartCoroutine(DownloadAssetBundleFromServer10());
       }
       private IEnumerator DownloadAssetBundleFromServer10()
       {

           GameObject go10 = null;

           string url10 = "https://drive.google.com/u/0/uc?id=1RfKL6jmhVye3WK6qk2hPHeIut3Iv1SR8&export=download";

           using (UnityWebRequest www10 = UnityWebRequestAssetBundle.GetAssetBundle(url10))
           {
               yield return www10.SendWebRequest();
               if (www10.result == UnityWebRequest.Result.ConnectionError || www10.result == UnityWebRequest.Result.ProtocolError)
               {
                   Debug.LogWarning("Errro on the get request at: " + url10 + "" + www10.error);
               }
               else
               {
                   AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www10);
                   go10 = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                   bundle.Unload(false);
                   yield return new WaitForEndOfFrame();
               }

               www10.Dispose();
           }
           InstantiateGameObjectFromAssetBundle10(go10);
       }

       public void Place11()
       {
           StartCoroutine(DownloadAssetBundleFromServer11());
       }
       private IEnumerator DownloadAssetBundleFromServer11()
       {

           GameObject go11 = null;

           string url11 = "https://drive.google.com/u/0/uc?id=1vyUA7B_C-_ss9vhZ-eF2-D5iBK6VOYAD&export=download";

           using (UnityWebRequest www11 = UnityWebRequestAssetBundle.GetAssetBundle(url11))
           {
               yield return www11.SendWebRequest();
               if (www11.result == UnityWebRequest.Result.ConnectionError || www11.result == UnityWebRequest.Result.ProtocolError)
               {
                   Debug.LogWarning("Errro on the get request at: " + url11 + "" + www11.error);
               }
               else
               {
                   AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www11);
                   go11 = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                   bundle.Unload(false);
                   yield return new WaitForEndOfFrame();
               }

               www11.Dispose();
           }
           InstantiateGameObjectFromAssetBundle11(go11);
       }

       public void Place12()
       {
           StartCoroutine(DownloadAssetBundleFromServer12());
       }
       private IEnumerator DownloadAssetBundleFromServer12()
       {

           GameObject go12 = null;

           string url12 = "https://drive.google.com/u/0/uc?id=1qfVvAMyROP2DmdtYQGI9Dqc1gWJaxUYk&export=download";

           using (UnityWebRequest www12 = UnityWebRequestAssetBundle.GetAssetBundle(url12))
           {
               yield return www12.SendWebRequest();
               if (www12.result == UnityWebRequest.Result.ConnectionError || www12.result == UnityWebRequest.Result.ProtocolError)
               {
                   Debug.LogWarning("Errro on the get request at: " + url12 + "" + www12.error);
               }
               else
               {
                   AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www12);
                   go12 = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                   bundle.Unload(false);
                   yield return new WaitForEndOfFrame();
               }

               www12.Dispose();
           }
           InstantiateGameObjectFromAssetBundle12(go12);
       }

       public void Place13()
       {
           StartCoroutine(DownloadAssetBundleFromServer13());
       }
       private IEnumerator DownloadAssetBundleFromServer13()
       {

           GameObject go13 = null;

           string url13 = "https://drive.google.com/u/0/uc?id=16fF0Jp02O-KbcpxoDutSYi0pZrOM8gAG&export=download";

           using (UnityWebRequest www13 = UnityWebRequestAssetBundle.GetAssetBundle(url13))
           {
               yield return www13.SendWebRequest();
               if (www13.result == UnityWebRequest.Result.ConnectionError || www13.result == UnityWebRequest.Result.ProtocolError)
               {
                   Debug.LogWarning("Errro on the get request at: " + url13 + "" + www13.error);
               }
               else
               {
                   AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www13);
                   go13 = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                   bundle.Unload(false);
                   yield return new WaitForEndOfFrame();
               }

               www13.Dispose();
           }
           InstantiateGameObjectFromAssetBundle13(go13);
       }




       private void InstantiateGameObjectFromAssetBundle(GameObject go)
       {
           if (go != null)
           {
               GameObject instanceGo = Instantiate(go);
               instanceGo.transform.position = Vector3.zero;
           }
           else
           {
               Debug.LogWarning("you asset bundle go is null");
           }
           *//*  if (go != null)
             {
                 GameObject newPlacedObject = Instantiate(go, placeIndicator.transform.position, placeIndicator.transform.rotation);
             }
             else
             {
                 Debug.LogWarning("you asset bundle go is null");
             }*//*
       }
       private void InstantiateGameObjectFromAssetBundle2(GameObject go2)
       {
           if (go2 != null)
           {
               GameObject instanceGo = Instantiate(go2);
               instanceGo.transform.position = Vector3.zero;
           }
           else
           {
               Debug.LogWarning("you asset bundle go is null");
           }
       }
       private void InstantiateGameObjectFromAssetBundle3(GameObject go3)
       {
           if (go3 != null)
           {
               GameObject instanceGo = Instantiate(go3);
               instanceGo.transform.position = Vector3.zero;
           }
           else
           {
               Debug.LogWarning("you asset bundle go is null");
           }
       }
       private void InstantiateGameObjectFromAssetBundle4(GameObject go4)
       {
           if (go4 != null)
           {
               GameObject instanceGo = Instantiate(go4);
               instanceGo.transform.position = Vector3.zero;
           }
           else
           {
               Debug.LogWarning("you asset bundle go is null");
           }
       }
       private void InstantiateGameObjectFromAssetBundle5(GameObject go5)
       {
           if (go5 != null)
           {
               GameObject instanceGo = Instantiate(go5);
               instanceGo.transform.position = Vector3.zero;
           }
           else
           {
               Debug.LogWarning("you asset bundle go is null");
           }
       }
       private void InstantiateGameObjectFromAssetBundle6(GameObject go6)
       {
           if (go6 != null)
           {
               GameObject instanceGo = Instantiate(go6);
               instanceGo.transform.position = Vector3.zero;
           }
           else
           {
               Debug.LogWarning("you asset bundle go is null");
           }
       }
       private void InstantiateGameObjectFromAssetBundle7(GameObject go7)
       {
           if (go7 != null)
           {
               GameObject instanceGo = Instantiate(go7);
               instanceGo.transform.position = Vector3.zero;
           }
           else
           {
               Debug.LogWarning("you asset bundle go is null");
           }
       }
       private void InstantiateGameObjectFromAssetBundle8(GameObject go8)
       {
           if (go8 != null)
           {
               GameObject instanceGo = Instantiate(go8);
               instanceGo.transform.position = Vector3.zero;
           }
           else
           {
               Debug.LogWarning("you asset bundle go is null");
           }
       }
       private void InstantiateGameObjectFromAssetBundle9(GameObject go9)
       {
           if (go9 != null)
           {
               GameObject instanceGo = Instantiate(go9);
               instanceGo.transform.position = Vector3.zero;
           }
           else
           {
               Debug.LogWarning("you asset bundle go is null");
           }
       }
       private void InstantiateGameObjectFromAssetBundle10(GameObject go10)
       {
           if (go10 != null)
           {
               GameObject instanceGo = Instantiate(go10);
               instanceGo.transform.position = Vector3.zero;
           }
           else
           {
               Debug.LogWarning("you asset bundle go is null");
           }
       }
       private void InstantiateGameObjectFromAssetBundle11(GameObject go11)
       {
           if (go11 != null)
           {
               GameObject instanceGo = Instantiate(go11);
               instanceGo.transform.position = Vector3.zero;
           }
           else
           {
               Debug.LogWarning("you asset bundle go is null");
           }
       }
       private void InstantiateGameObjectFromAssetBundle12(GameObject go12)
       {
           if (go12 != null)
           {
               GameObject instanceGo = Instantiate(go12);
               instanceGo.transform.position = Vector3.zero;
           }
           else
           {
               Debug.LogWarning("you asset bundle go is null");
           }
       }
       private void InstantiateGameObjectFromAssetBundle13(GameObject go13)
       {
           if (go13 != null)
           {
               GameObject instanceGo = Instantiate(go13);
               instanceGo.transform.position = Vector3.zero;
           }
           else
           {
               Debug.LogWarning("you asset bundle go is null");
           }
       }*/
}
