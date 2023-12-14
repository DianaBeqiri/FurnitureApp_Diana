using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;



public class DownloadAssetBundleSimplifyWay : MonoBehaviour
{
    // public GameObject placementIndicator;
    public Button retryButton;
    public Transform indicator;
    public Transform loadii;
    private ARRaycastManager aRRaycastManager;
    private Pose PlacementPose;
    private bool placementPoseIsValid = false;
    public GameObject ghost;
    private GameObject objectToPlace;
    public GameObject Load;

    public string targetURL = "https://www.example.com";
    public string targetURL1 = "https://www.example1.com";
    public string targetURL2 = "https://www.example2.com";
    public GameObject ErrorPanel;

    private bool isConnected = false;
    public GameObject placementii1;
    private GameObject instantiatedModel; // Reference to the instantiated model
    private GameObject secondinstantiatedModel; // Reference to the instantiated model
    private GameObject thirdinstantiatedModel; // Reference to the instantiated model

    public Demo demoScript; // Reference to the Demo script
    //private bool firstModelInstantiated = false; // Flag to check if the first model has been instantiated
    //private bool secondModelInstantiated = false; // Flag to check if the first model has been instantiated




    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        StartCoroutine(ConnectToNetworkk());

        retryButton.onClick.AddListener(RetryConnection);
        CheckInternetConnection();
        InvokeRepeating("UpdateModelColor", 0, 2);
        InvokeRepeating("UpdateModelColor2", 0, 2);
        InvokeRepeating("UpdateModelColor3", 0, 2);
    }



    /*public IEnumerator RefreshScreen()
    {
        yield return new WaitForSeconds(5f);
        RefreshData();
    }
    private void RefreshData()
    {
        StartCoroutine(Object1FromServer());

    }*/

    public void LoadFirstModel()
    {
        ObjectFromServer1();
    }
    public void ObjectFromServer1()
    {
        StartCoroutine(Object1FromServer());
        placementii1.SetActive(false);
        Load.transform.position = indicator.transform.position;
        Load.transform.rotation = indicator.transform.rotation;
    }



    private IEnumerator Object1FromServer()
    {

        GameObject go = null;

        // Log the URL received for the first model
        Debug.Log("URL for the first model: " + targetURL);
        //string url = "https://drive.google.com/u/0/uc?id=1qhCikVYcJHbFTZMDAhLoGnCPD2tNl_yV&export=download";

        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(targetURL))
        {
            Load.SetActive(true);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("Error on the get request at: " + targetURL + " " + www.error);

            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                go = bundle.LoadAsset<GameObject>(bundle.GetAllAssetNames()[0]);
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
        if (go != null && indicator != null)
        {
            instantiatedModel = Instantiate(go, indicator.position, Quaternion.identity);

            // Copy the position and rotation from the indicator transform
            instantiatedModel.transform.position = loadii.position;
            instantiatedModel.transform.rotation = loadii.rotation;
            Load.SetActive(false);

            

            // Access the MeshRenderer component on the child object recursively
            MeshRenderer meshRenderer = FindMeshRendererInDeepHierarchy(instantiatedModel);
            if (meshRenderer != null)
            {
                Debug.Log("MeshRenderer component found in deep hierarchy.");

                if (demoScript != null)
                {
                    if (demoScript.TryParseColor(demoScript.colorString, out Color newColor))
                    {
                        Debug.Log("Changing material color to: " + newColor);
                        Material material = new Material(meshRenderer.material);
                        material.color = newColor;
                        meshRenderer.material = material;
                    }
                    else
                    {
                        Debug.LogError("Invalid color string: " + demoScript.colorString);
                    }
                }
            }
            else
            {
                Debug.LogWarning("MeshRenderer component not found in deep hierarchy.");
            }

        }
        else
        {
            Debug.LogWarning("Your asset bundle GameObject or indicator transform is null.");
        }
    }

    private void UpdateModelColor()
    {
        if (instantiatedModel != null && demoScript != null)
        {
            if (demoScript.TryParseColor(demoScript.colorString, out Color newColor))
            {
                Debug.Log("Changing material color to: " + newColor);
                MeshRenderer meshRenderer = FindMeshRendererInDeepHierarchy(instantiatedModel);
                if (meshRenderer != null)
                {
                    Material material = new Material(meshRenderer.material);
                    material.color = newColor;
                    meshRenderer.material = material;
                }
                else
                {
                    Debug.LogWarning("MeshRenderer component not found in deep hierarchy.");
                }
            }
            else
            {
                Debug.LogError("Invalid color string: " + demoScript.colorString);
            }
        }
    }
    private MeshRenderer FindMeshRendererInDeepHierarchy(GameObject obj)
    {
        MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            return meshRenderer;
        }

        foreach (Transform child in obj.transform)
        {
            meshRenderer = FindMeshRendererInDeepHierarchy(child.gameObject);
            if (meshRenderer != null)
            {
                return meshRenderer;
            }
        }

        return null;
    }

    public void Sofaa2()
    {

        StartCoroutine(Sofa2());
        placementii1.SetActive(false);
        Load.transform.position = indicator.transform.position;
        Load.transform.rotation = indicator.transform.rotation;
    }

    private void UpdateModelColor2()
    {
        if (secondinstantiatedModel != null && demoScript != null)
        {
            if (demoScript.TryParseColor(demoScript.colorString1, out Color newColor))
            {
                Debug.Log("Changing material color to: " + newColor);
                MeshRenderer meshRenderer = FindMeshRendererInDeepHierarchy(secondinstantiatedModel);
                if (meshRenderer != null)
                {
                    Material material = new Material(meshRenderer.material);
                    material.color = newColor;
                    meshRenderer.material = material;
                }
                else
                {
                    Debug.LogWarning("MeshRenderer component not found in deep hierarchy.");
                }
            }
            else
            {
                Debug.LogError("Invalid color string: " + demoScript.colorString1);
            }
        }
    }

    // Update is called once per frame
    private IEnumerator Sofa2()
    {
        GameObject go2 = null;

        // Log the URL received for the second model
        Debug.Log("URL for the second model: " + targetURL1);
        //string url2 = "https://drive.google.com/u/0/uc?id=1fsBMH8UJVmKVySuPW8yCCKe3xUMHKkg4&export=download";

        using (UnityWebRequest www2 = UnityWebRequestAssetBundle.GetAssetBundle(targetURL1))
        {
            Load.SetActive(true);

            yield return www2.SendWebRequest();
            if (www2.result == UnityWebRequest.Result.ConnectionError || www2.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("Error on the get request at: " + targetURL1 + " " + www2.error);
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

    private void InstantiateGameObjectFromAssetBundle2(GameObject go2)
    {
        if (go2 != null && indicator != null)
        {
            secondinstantiatedModel = Instantiate(go2, indicator.position, Quaternion.identity);

            // Copy the position and rotation from the indicator transform
            secondinstantiatedModel.transform.position = loadii.position;
            secondinstantiatedModel.transform.rotation = loadii.rotation;
            Load.SetActive(false);

            

            // Access the MeshRenderer component on the child object recursively
            MeshRenderer meshRenderer = FindMeshRendererInDeepHierarchy(secondinstantiatedModel);
            if (meshRenderer != null)
            {
                Debug.Log("MeshRenderer component found in deep hierarchy.");

                if (demoScript != null)
                {
                    if (demoScript.TryParseColor(demoScript.colorString1, out Color newColor))
                    {
                        Debug.Log("Changing material color to: " + newColor);
                        Material material = new Material(meshRenderer.material);
                        material.color = newColor;
                        meshRenderer.material = material;
                    }
                    else
                    {
                        Debug.LogError("Invalid color string: " + demoScript.colorString1);
                    }
                }
            }
            else
            {
                Debug.LogWarning("MeshRenderer component not found in deep hierarchy.");
            }
        }
        else
        {
            Debug.LogWarning("Your asset bundle GameObject or indicator transform is null.");
        }
    }

    public void Sofaa3()
    {

        StartCoroutine(Sofa3());
        placementii1.SetActive(false);
        Load.transform.position = indicator.transform.position;
        Load.transform.rotation = indicator.transform.rotation;

    }

    // Update is called once per frame
    private IEnumerator Sofa3()
    {
        GameObject go3 = null;
        //string url3 = "https://drive.google.com/u/0/uc?id=1XaLBN9cQWK_rveVlW8ikLUUE9tfbBwEN&export=download";

        using (UnityWebRequest www3 = UnityWebRequestAssetBundle.GetAssetBundle(targetURL2))
        {
            Load.SetActive(true);

            yield return www3.SendWebRequest();
            if (www3.result == UnityWebRequest.Result.ConnectionError || www3.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("Error on the get request at: " + targetURL2 + " " + www3.error);
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
        UseObject(go3);
    }

    private void UpdateModelColor3()
    {
        if (thirdinstantiatedModel != null && demoScript != null)
        {
            if (demoScript.TryParseColor(demoScript.colorString2, out Color newColor))
            {
                Debug.Log("Changing material color to: " + newColor);
                MeshRenderer meshRenderer = FindMeshRendererInDeepHierarchy(thirdinstantiatedModel);
                if (meshRenderer != null)
                {
                    Material material = new Material(meshRenderer.material);
                    material.color = newColor;
                    meshRenderer.material = material;
                }
                else
                {
                    Debug.LogWarning("MeshRenderer component not found in deep hierarchy.");
                }
            }
            else
            {
                Debug.LogError("Invalid color string: " + demoScript.colorString2);
            }
        }
    }

    private void InstantiateGameObjectFromAssetBundle3(GameObject go3)
    {
        if (go3 != null && indicator != null)
        {
            thirdinstantiatedModel = Instantiate(go3, indicator.position, Quaternion.identity);

            // Copy the position and rotation from the indicator transform
            thirdinstantiatedModel.transform.position = loadii.position;
            thirdinstantiatedModel.transform.rotation = loadii.rotation;
            Load.SetActive(false);



            // Access the MeshRenderer component on the child object recursively
            MeshRenderer meshRenderer = FindMeshRendererInDeepHierarchy(thirdinstantiatedModel);
            if (meshRenderer != null)
            {
                Debug.Log("MeshRenderer component found in deep hierarchy.");

                if (demoScript != null)
                {
                    if (demoScript.TryParseColor(demoScript.colorString2, out Color newColor))
                    {
                        Debug.Log("Changing material color to: " + newColor);
                        Material material = new Material(meshRenderer.material);
                        material.color = newColor;
                        meshRenderer.material = material;
                    }
                    else
                    {
                        Debug.LogError("Invalid color string: " + demoScript.colorString2);
                    }
                }
            }
            else
            {
                Debug.LogWarning("MeshRenderer component not found in deep hierarchy.");
            }
        }
        else
        {
            Debug.LogWarning("Your asset bundle GameObject or indicator transform is null.");
        }
    }

    public void SetTargetURL(string url)
    {
        targetURL = url;
    }

    public void SetTargetURL1(string url)
    {
        targetURL1 = url;
    }

    public void SetTargetURL2(string url)
    {
        targetURL2 = url;
    }

    void Update()
    {
        UpdatePlacementPose();
        //RefreshScreen();
        UpdatePlacementIndicator();
    }



    /// <Retry>


    void CheckInternetConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            // No internet connection, show error panel
            ErrorPanel.SetActive(true);
        }
        else
        {
            // Internet connection is available, hide error panel
            ErrorPanel.SetActive(false);
        }
    }

    public void RetryConnection()
    {
        CheckInternetConnection();
    }

    IEnumerator ConnectToNetworkk()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(targetURL))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Connected to the network.");
                CheckInternetConnection();
            }
            else
            {
                Debug.LogError("Failed to connect to the network: " + webRequest.error);
                ErrorPanel.SetActive(true);
            }
        }
    }

    /// </Retry>
    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid) // works
        {
            //placementIndicator.SetActive(true);
            //  confirmBtn.SetActive(true);

            indicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            Debug.LogWarning("Is null Indicator");
            ///placementii1.SetActive(false);
           // confirmBtn.SetActive(false);
        }
    }



    IEnumerator ConnectToNetwork()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(targetURL))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Connected to the network.");
                //ErrorPanel.SetActive(false);
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    // No internet connection, show error panel
                    ErrorPanel.SetActive(true);
                }


            }
            else
            {
                Debug.LogError("Failed to connect to the network: " + webRequest.error);
                //ErrorPanel.SetActive(true);
                // Internet connection is available, hide error panel
                ErrorPanel.SetActive(false);

            }


        }

    }




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

    public void PlaceObject()
    {
        ghost.GetComponent<Recolour>().SetOriginalMaterial();
        ghost.transform.parent = null;
        ghost = Instantiate(objectToPlace, PlacementPose.position, PlacementPose.rotation);
        ghost.GetComponent<Recolour>().SetValid();
        ghost.transform.parent = indicator.transform;
    }

    private void UseObject(GameObject o)
    {
        objectToPlace = o;
        Destroy(ghost);
        ghost.GetComponent<Recolour>().SetValid();
        ghost.transform.parent = indicator.transform;
    }





}