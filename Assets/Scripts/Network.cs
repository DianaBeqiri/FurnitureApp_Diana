using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
public class Network : MonoBehaviour
{
    public string targetURL = "https://www.example.com"; // Replace with your desired URL

    public GameObject targetURL1;

    void Start()
    {
        StartCoroutine(ConnectToNetwork());
    }

    IEnumerator ConnectToNetwork()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(targetURL))
        {
            // Send the request and wait for a response
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                // Connection was successful
                Debug.Log("Connected to the network.");
                // You can process the data here if needed
                targetURL1.SetActive(false);

            }
            else
            {
                // Connection failed
                Debug.LogError("Failed to connect to the network: " + webRequest.error);
                // Handle the error or display a message to the user
                targetURL1.SetActive(true);

            }
        }
    }
}
