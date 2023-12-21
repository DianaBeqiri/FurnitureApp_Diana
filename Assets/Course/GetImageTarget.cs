using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Course.Core.RestHttp;
using Assets.Course.Core;
using TriLibCore.Samples;
using Assets.Course.Models;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using Assets;

namespace Assets.Course
{
    public class GetImageTarget : MonoBehaviour
    {
        public string baseUrl = "https://edde-84-22-36-74.ngrok-free.app/imagetargets";
        public LoadModelFromURLSample load;
        private GameObject imgTargetCard;
        public GameObject detailsPanel;
        public TextMeshProUGUI detailsDescription;
        public RawImage detailsImage;
        public GameObject imgTargetCardPrefab;

        public Transform contentPanel;

        private void Start()
        {
            RequestHeader reqHeader = new RequestHeader
            {
                Key = "Content-Type",
                Value = "application/json"
            };

            StartCoroutine(UpdateDataRoutine());
        }

        private IEnumerator UpdateDataRoutine()
        {
            while (true)
            {
                // Make the HTTP request to fetch data
                StartCoroutine(RestApiClient.Instance.HttpGet(baseUrl, (r) => OnRequestCompleted(r)));

                // Wait for 3 seconds before making the next request
                yield return new WaitForSeconds(3f);
            }
        }
        void OnRequestCompleted(Response response)
        {
            // Clear existing data before updating
            ClearExistingData();
            Debug.Log("Status Code :" + response.StatusCode);
            Debug.Log("Data : " + response.Data);
            Debug.Log("Error : " + response.Error);

            AllImageTargets allImageTargets = JsonUtility.FromJson<AllImageTargets>(response.Data);
            int count = allImageTargets.data.Length;
            int index = 0;
            Debug.Log(allImageTargets);
            for (int i = 0; i < count; i++)
            {
                imgTargetCard = Instantiate(imgTargetCardPrefab) as GameObject;
                imgTargetCard.SetActive(true);

                ImageTargetButton imgTarget = imgTargetCard.GetComponent<ImageTargetButton>();

                imgTarget.id.text = allImageTargets.data[i].id.ToString();
                imgTarget.img_name.text = allImageTargets.data[i].name;
                imgTarget.color.text = allImageTargets.data[i].color;
                imgTarget.description.text = allImageTargets.data[i].description;
                imgTarget.model_url.text = allImageTargets.data[i].model_url;

                /*load.ModelURL = imgTarget.model_url.text;
                Debug.Log(load.ModelURL);*/
                

                StartCoroutine(GetTexture(allImageTargets.data[i].image_url, tex =>
                {
                    imgTarget.image_target.texture = tex;
                }));

                imgTargetCard.transform.SetParent(contentPanel, false);
                int newIndex = index;

                imgTargetCard.GetComponent<Button>().onClick.AddListener(
                    () =>
                    {
                        imageTargetDetails(newIndex, allImageTargets);
                        
                    });
                index++;
            }
        }

        private void ClearExistingData()
        {
            // Destroy existing instantiated prefabs
            foreach (Transform child in contentPanel.transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void imageTargetDetails(int index, AllImageTargets imgTargetObject)
        {
            detailsPanel.SetActive(true);
            detailsDescription.text = imgTargetObject.data[index].name;

            // Pass color information to LoadModelFromURLSample script
            load.SetModelDetails(imgTargetObject.data[index].model_url, imgTargetObject.data[index].color);

            StartCoroutine(GetTexture(imgTargetObject.data[index].image_url, tex =>
            {
                detailsImage.texture = tex;
            }));
        }


        IEnumerator GetTexture(string url, System.Action<Texture> callback)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture tex = DownloadHandlerTexture.GetContent(www);
                callback(tex);
            }
        }

    }
}
