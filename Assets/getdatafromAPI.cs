using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class getdatafromAPI : MonoBehaviour
{
    public string url;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getImageTarget());
        //StartCoroutine(getSingleImageTarget(2));
        //StartCoroutine(createImageTarget());
        //StartCoroutine(updateImageTarget(13));
        //StartCoroutine(deleteImageTarget(3));
    }

    // Update is called once per frame
    void Update()
    {

    }


    private IEnumerator getImageTarget()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            /*www.SendWebRequest("Accept-Language", "alb");*/
            /*www.SetRequestHeader("Authorization", "Bearer" + authorizationToken);*/

            AsyncOperation request = www.SendWebRequest();

            while (!request.isDone)
            {
                // Show loading UI while getting data
                yield return null;
            }

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                // show network popup if there is no network
            }
            else
            {
                string imageTargets = www.downloadHandler.text;

                Debug.Log(imageTargets);
                AllImageTargets allImageTargets = JsonUtility.FromJson<AllImageTargets>(imageTargets);
                Debug.Log(allImageTargets);
                Debug.Log(allImageTargets.data[0].name);
                Debug.Log(allImageTargets.data[1].name);
                Debug.Log(allImageTargets.data[2].name);

                //get meta data
                Debug.Log(allImageTargets.meta.page);

            }
        }
    }

    private IEnumerator getSingleImageTarget(int id)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + "/" + id))
        {
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            AsyncOperation request = www.SendWebRequest();

            while (!request.isDone)
            {
                //Show loading UI while getting data
                yield return null;
            }

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                // Show network error
            }
            else
            {
                string imageTarget = www.downloadHandler.text;
                Debug.Log(imageTarget);
            }
        }
    }

    private IEnumerator createImageTarget()
    {
        ImageTarget newImageTarget = new ImageTarget();
        newImageTarget.name = "Ford";
        newImageTarget.description = "An amazing car that will never stop!!";
        newImageTarget.color = "#ffffff";
        newImageTarget.image_url = "gfffff";
        newImageTarget.model_url = "ford.png";

        string newImageTargetJson = JsonUtility.ToJson(newImageTarget);

        using (UnityWebRequest www = UnityWebRequest.Post(url, newImageTargetJson))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(newImageTargetJson);
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            AsyncOperation request = www.SendWebRequest();

            while (!request.isDone)
            {
                yield return null;
            }

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                // Show network error
            }
            else
            {
                string imageTarget = www.downloadHandler.text;
                Debug.Log(imageTarget);
            }
        }
    }

    private IEnumerator updateImageTarget(int id)
    {
        ImageTarget updateImageTarget = new ImageTarget();
        updateImageTarget.name = "Ford1";
        updateImageTarget.description = "New car";
        updateImageTarget.color = "#bebebe";
        updateImageTarget.image_url = "#bebdfebe";
        updateImageTarget.model_url = "ford1.png";

        string updateImageTargetJson = JsonUtility.ToJson(updateImageTarget);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(updateImageTargetJson);

        using (UnityWebRequest www = UnityWebRequest.Put(url + "/" + id, jsonToSend))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            AsyncOperation request = www.SendWebRequest();
            while (!request.isDone)
            {
                yield return null;
            }

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                //show network error
            }
            else
            {
                string imageTarget = www.downloadHandler.text;

                Debug.Log(imageTarget);
            }
        }

    }

    private IEnumerator deleteImageTarget(int id)
    {
        using (UnityWebRequest www = UnityWebRequest.Delete(url + "/" + id))
        {
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            AsyncOperation request = www.SendWebRequest();

            while (!request.isDone)
            {
                yield return null;
            }
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                //show network error
            }
            else
            {
                string imageTarget = www.downloadHandler.text;
                Debug.Log(imageTarget);
            }
        }
    }
}
[System.Serializable]
public class AllImageTargets
{
    public ImageTarget[] data;
    public Meta meta;
}

[System.Serializable]
public class ImageTarget
{
    public int id;
    public string name;
    public string description;
    public string color;
    public string image_url;
    public string model_url;
}

[System.Serializable]
public class Meta
{
    public string page;
}
