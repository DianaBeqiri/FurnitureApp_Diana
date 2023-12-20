using UnityEngine;
using TriLibCore.Samples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

namespace TriLibCore.Samples
{
    /// <summary>
    /// Represents a sample that loads a compressed (Zipped) Model.
    /// </summary>
    public class LoadModelFromURLSample : MonoBehaviour
    {
        public string ModelURL = "https://ricardoreis.net/trilib/demos/sample/TriLibSampleModel.zip";
        public Transform indicator;
        private ARRaycastManager aRRaycastManager;
        private Pose PlacementPose;
        private bool placementPoseIsValid = false;

        private AssetLoaderOptions _assetLoaderOptions;

        // Added color variable
        public Color modelColor = Color.white;

        void Start()
        {
            // Initialize indicator or any other setup you may need
            // For example, you might want to disable the indicator initially
            //indicator.gameObject.SetActive(true);
            aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        }

        private void Update()
        {
            UpdatePlacementPose();
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
        }
        public void SetModelDetails(string modelURL, string color)
        {
            ModelURL = modelURL;

            // Convert color string to Color
            ColorUtility.TryParseHtmlString(color, out modelColor);
        }

        private void OnMaterialsLoad(AssetLoaderContext assetLoaderContext)
        {
            Debug.Log("Materials loaded. Model fully loaded.");
            // Deactivate the indicator when materials are loaded
            indicator.gameObject.SetActive(false);

            // Set the loaded model to active
            assetLoaderContext.RootGameObject.SetActive(true);

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

        public void PlaceAsset()
        {
            // Add placement logic if needed
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
            Debug.Log("Model loaded. Loading materials.");

            // Deactivate the indicator when the model is fully loaded
            indicator.gameObject.SetActive(false);

            // Set the position of the loaded model to match the indicator's position
            if (assetLoaderContext.RootGameObject != null)
            {
                assetLoaderContext.RootGameObject.transform.position = indicator.position;
                // Optionally, you can set the rotation as well
                assetLoaderContext.RootGameObject.transform.rotation = indicator.rotation;
            }
        }
    }
}
