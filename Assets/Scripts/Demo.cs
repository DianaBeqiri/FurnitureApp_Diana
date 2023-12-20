using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.Networking;
using TMPro;

public static class ButtonExtension
{
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate ()
        {
            OnClick(param);
        });
    }
}

public class Demo : MonoBehaviour
{
    /*[Serializable]
    public struct Game
    {
        public string name;
        public string description;
        public string link;
        public string color;

    }*/

    /*public TextMeshProUGUI NameText;        // Assign this in the Inspector
    public TextMeshProUGUI DescriptionText; // Assign this in the Inspector
    //public TextMeshProUGUI LinkText;        // Assign this in the Inspector
    public TextMeshProUGUI AdditionalNameText; // Assign this in the Inspector
    public TextMeshProUGUI AdditionalDescriptionText; // Assign this in the Inspector
    //public TextMeshProUGUI AdditionalLinkText; // Assign this in the Inspector
    public TextMeshProUGUI AdditionalNameText2; // Assign this in the Inspector
    public TextMeshProUGUI AdditionalDescriptionText2; // Assign this in the Inspector
    //public TextMeshProUGUI AdditionalLinkText2; // Assign this in the Inspector
    public GameObject gb;
    public GameObject gb1;
    public GameObject gb2;
    public DownloadAssetBundleSimplifyWay downloadScript; // Reference to the other script
*/
    public Material targetMaterial; // Reference to the Material you want to change

    public string colorString = "";
    public string colorString1 = "";
    public string colorString2 = "";

    //Game[] allGames;

    void Start()
    {
        // Fetch data from Json
        //StartCoroutine(GetGames());

        InvokeRepeating("changeColor", 0, 5); // Change 5 to your desired interval in seconds.

    }

    /*void FetchDataPeriodically()
    {
        StartCoroutine(GetGames());
    }*/

    void changeColor(string newColorString)
    {

        if (TryParseColor(newColorString, out Color newColor))
        {
            if (targetMaterial != null)
            {
                targetMaterial.color = newColor;
            }
            else
            {
                Debug.LogError("Target Material is not assigned. Please assign a Material.");
            }
        }
        else
        {
            Debug.LogError("Invalid color string: " + newColorString);
        }

    }
    public bool TryParseColor(string colorString, out Color color)
    {
        color = Color.white; // Default color

        if (ColorUtility.TryParseHtmlString(colorString, out color))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*void DrawUI()
    {
        int N = allGames.Length;

        // Check if the UI Text elements are assigned
        if (NameText != null && DescriptionText != null && N > 0)
        {
            // Update the UI Text elements with data from the last record
            NameText.text = allGames[0].name;
            DescriptionText.text = allGames[0].description;
            //LinkText.text = allGames[0].link;
            gb.SetActive(true);

            if (!string.IsNullOrEmpty(allGames[0].color))
            {
                colorString = allGames[0].color;
                changeColor(colorString);
            }
            // Set the URL in the DownloadAssetBundleSimplifyWay script
            downloadScript.SetTargetURL(allGames[0].link);
        }

        // Check if there is at least one more row of data
        if (N > 1)
        {
            // Populate the additional text fields with data from the second last record
            AdditionalNameText.text = allGames[1].name;
            AdditionalDescriptionText.text = allGames[1].description;
            //AdditionalLinkText.text = allGames[1].link;

            // Activate the second game object
            // Set your game object to active here
            gb1.SetActive(true);

            if (!string.IsNullOrEmpty(allGames[1].color))
            {
                colorString1 = allGames[1].color;
                changeColor(colorString1);
            }
            downloadScript.SetTargetURL1(allGames[1].link);

            if (N > 2)
            {
                // Populate the additional text fields with data from the third last record
                AdditionalNameText2.text = allGames[2].name;
                AdditionalDescriptionText2.text = allGames[2].description;
                //AdditionalLinkText2.text = allGames[2].link;

                if (!string.IsNullOrEmpty(allGames[2].color))
                {
                    colorString2 = allGames[2].color;
                    changeColor(colorString2);
                }

                downloadScript.SetTargetURL2(allGames[2].link);
                // Activate the third game object
                // Set your game object to active here
                gb2.SetActive(true);
            }
        }
        else
        {
            // If there are less than 2 rows, deactivate the second and third game objects
            // Set your game objects to inactive here
            // gameObjects[1].SetActive(false);
            // gameObjects[2].SetActive(false);
            gb2.SetActive(false);
            gb1.SetActive(false);
        }
    }


    //***************************************************
    IEnumerator GetGames()
    {
        string url = "https://3718-84-22-36-74.ngrok-free.app/furniture"; // Use the correct server URL.

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.chunkedTransfer = false;
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Failed to fetch data from the server: " + request.error);
            // Handle the error appropriately, e.g., show an error message.
        }
        else
        {
            if (request.isDone)
            {
                allGames = JsonHelper.GetArray<Game>(request.downloadHandler.text);
                DrawUI();
            }
        }
    }*/

}