using System;
using UnityEngine;
using UnityEngine.UI;
using BestHTTP;
using BestHTTP.JSON;
using TMPro;
using System.Text.RegularExpressions;


public class mysql : MonoBehaviour
{
    public TextMeshProUGUI theName;
    public TextMeshProUGUI theDescription;
    public TextMeshProUGUI theLink;
    void Start()
    {
        string name = "http://localhost:5000/attributes/name";  // Update with your server's IP
        HTTPRequest request = new HTTPRequest(new Uri(name), HTTPMethods.Get, (originalRequest, response) => {
            if (response.IsSuccess)
            {
                //Debug.Log("Data received: " + response.DataAsText);
                string logMessage = response.DataAsText;
                logMessage = RemoveBracketsAndQuotes(logMessage);

                theName.text = logMessage;
                // Parse the response data as needed
            }
            else
            {
                Debug.LogError("HTTP request failed: " + response.Message);
            }
        });

        // Send the request
        request.Send();



        string description = "http://localhost:5000/attributes/description";  // Update with your server's IP
        HTTPRequest request1 = new HTTPRequest(new Uri(description), HTTPMethods.Get, (originalRequest, response) => {
            if (response.IsSuccess)
            {
                //Debug.Log("Data received: " + response.DataAsText);
                string logMessage = response.DataAsText;
                logMessage = RemoveBracketsAndQuotes(logMessage);

                theDescription.text = logMessage;
                // Parse the response data as needed
            }
            else
            {
                Debug.LogError("HTTP request failed: " + response.Message);
            }
        });

        // Send the request
        request1.Send();

        string link = "http://localhost:5000/attributes/link";  // Update with your server's IP
        HTTPRequest request2 = new HTTPRequest(new Uri(link), HTTPMethods.Get, (originalRequest, response) => {
            if (response.IsSuccess)
            {
                //Debug.Log("Data received: " + response.DataAsText);
                string logMessage = response.DataAsText;
                logMessage = RemoveBracketsAndQuotes(logMessage);
                theLink.text = logMessage;
                // Parse the response data as needed
            }
            else
            {
                Debug.LogError("HTTP request failed: " + response.Message);
            }
        });

        // Send the request
        request2.Send();
        RefreshData();

    }

    public void RefreshData()
    {
        FetchAndDisplayData("http://localhost:5000/attributes/name", theName);
        FetchAndDisplayData("http://localhost:5000/attributes/description", theDescription);
        FetchAndDisplayData("http://localhost:5000/attributes/link", theLink);
    }

    void FetchAndDisplayData(string url, TextMeshProUGUI targetText)
    {
        HTTPRequest request = new HTTPRequest(new Uri(url), HTTPMethods.Get, (originalRequest, response) => {
            if (response.IsSuccess)
            {
                string responseData = response.DataAsText;

                // Remove brackets and quotes from the received data
                responseData = RemoveBracketsAndQuotes(responseData);

                targetText.text = responseData;
            }
            else
            {
                Debug.LogError("HTTP request failed: " + response.Message);
            }
        });

        // Send the request
        request.Send();
    }

    private string RemoveBracketsAndQuotes(string data)
    {
        data = data.Replace("[", "").Replace("]", "").Replace("\"", "");
        return data;
    }


}



