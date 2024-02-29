using UnityEngine;
using TriLibCore.Samples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using TMPro;
using Lean.Touch;

namespace TriLibCore.Samples
{
    /// <summary>
    /// Represents a sample that loads a compressed (Zipped) Model.
    /// </summary>
    public class LoadModelFromURLSample : MonoBehaviour
    {
        public string ModelURL = "https://8f53-84-22-36-74.ngrok-free.app/imagetargets";
        public Transform indicator;
        private ARRaycastManager aRRaycastManager;
        private Pose PlacementPose;
        private bool placementPoseIsValid = false;
        public GameObject goToARBtn;
        public GameObject loadingProgressGame;
        public TextMeshProUGUI loadingProgressText; // Reference to the UI Text element


        private AssetLoaderOptions _assetLoaderOptions;

        public AssetLoaderContext _assetLoaderContext;

        // Added color variable
        public Color modelColor = Color.white;

        void Start()
        {
            // Initialize indicator or any other setup you may need
            // For example, you might want to disable the indicator initially
            //indicator.gameObject.SetActive(true);
            aRRaycastManager = FindObjectOfType<ARRaycastManager>();
            InvokeRepeating("UpdateMaterials", 0f, 3f);
        }

        private void Update()
        {
            UpdatePlacementPose();
        }
        public void PlaceAsset()
        {
            
        }
        public void PlaceModel()
        {
            if (_assetLoaderContext != null)
            {
                Debug.Log("Model loaded. Loading materials.");
                _assetLoaderContext.RootGameObject.transform.position = indicator.transform.position;
                _assetLoaderContext.RootGameObject.transform.rotation = indicator.transform.rotation;
                _assetLoaderContext.RootGameObject.SetActive(true); // Add the model to the scene
                indicator.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("No model data available. Please download the model first.");
            }
        }
        public void Download()
        {
            if (_assetLoaderOptions == null)
            {
                _assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions(false, true);
            }

            // Activate the indicator when starting the download
            //indicator.gameObject.SetActive(true);

            var webRequest = AssetDownloader.CreateWebRequest(ModelURL);
            AssetDownloader.LoadModelFromUri(webRequest, OnLoad, OnMaterialsLoad, OnProgress, OnError, null, _assetLoaderOptions);
        }

        private void OnError(IContextualizedError obj)
        {
            Debug.LogError($"An error occurred while loading your Model: {obj.GetInnerException()}");
            // Deactivate the indicator on error
            indicator.gameObject.SetActive(false);
        }

        private void OnProgress(AssetLoaderContext assetLoaderContext, float progress)
        {


            Debug.Log($"Loading Model. Progress: {progress:P}");
            // You may update the indicator's position or progress visualization here
            //assetLoaderContext.RootGameObject = AssetStored;
            UpdateLoadingProgress(progress); // Update the UI Text with the loading progress
        }
        public void SetModelDetails(string modelURL, string color)
        {
            ModelURL = modelURL;

            // Convert color string to Color
            ColorUtility.TryParseHtmlString(color, out modelColor);
           
        }
        private void UpdateMaterials()
        {
            // Check if _assetLoaderContext is not null before calling OnMaterialsLoad
            if (_assetLoaderContext != null)
            {
                OnMaterialsLoad(_assetLoaderContext);
            }
        }

        public void OnMaterialsLoad(AssetLoaderContext assetLoaderContext)
        {
            Debug.Log("Materials loaded. Model fully loaded.");
            // Deactivate the indicator when materials are loaded
            //indicator.gameObject.SetActive(false);

            // Set the loaded model to active
            //assetLoaderContext.RootGameObject.SetActive(true);

            // Apply color to materials
            ApplyColorToMaterials(assetLoaderContext.RootGameObject);
        }

        private void ApplyColorToMaterials(GameObject loadedModel)
        {
            if (loadedModel != null)
            {
                Renderer[] renderers = loadedModel.GetComponentsInChildren<Renderer>(true);

                foreach (Renderer renderer in renderers)
                {
                    Material[] materials = renderer.materials;

                    foreach (Material material in materials)
                    {
                        // Set the color to the specified modelColor
                        material.color = modelColor;
                    }

                    renderer.materials = materials;
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

                // Update the position of the indicator based on the AR hit point
                indicator.position = PlacementPose.position;
                indicator.rotation = PlacementPose.rotation;
            }
        }

        private void OnLoad(AssetLoaderContext assetLoaderContext)
        {
            _assetLoaderContext = assetLoaderContext; // Store the downloaded asset
            _assetLoaderContext.RootGameObject.SetActive(false); // Prevent the model from being added to the scene
            assetLoaderContext.RootGameObject.SetActive(false); // Prevent the model from being added to the scene
            Invoke("DeactivateProgresiveText", 1f);

            // Attach LeanTouch script to enable touch gestures
            //AttachLeanTouchScripts(assetLoaderContext.RootGameObject);
        }

        /*private void AttachLeanTouchScripts(GameObject loadedModel)
        {
            // Attach Lean Drag Translate script
            LeanTouch leanDragTranslate = loadedModel.AddComponent<LeanTouch>();

            // Attach Lean Pinch Scale script
            LeanPinchScale leanPinchScale = loadedModel.AddComponent<LeanPinchScale>();

            // Attach Lean Twist Rotate Axis script
            LeanTwistRotateAxis leanTwistRotateAxis = loadedModel.AddComponent<LeanTwistRotateAxis>();

            

            // You may need to configure additional properties based on the Lean Touch documentation
        }*/

        private void DeactivateProgresiveText()
        {
            loadingProgressGame.SetActive(false);
            goToARBtn.SetActive(true);
        }
        private void UpdateLoadingProgress(float progress)
        {
            if (loadingProgressText != null)
            {
                if (progress < 1.0f)
                {
                    loadingProgressText.text = $"Downloading Model {progress:P}";
                }
                else
                {
                    loadingProgressText.text = "Download Model";
                }
            }
        }
    }
}
