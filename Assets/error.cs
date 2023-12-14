using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class error : MonoBehaviour
{
    [SerializeField] RectTransform FxHolder;
    [SerializeField] Image CircleImg;
    [SerializeField] TextMesh textProgress;
    public GameObject errorpanel;

    private float currentProgress = 0f;
    private float targetProgress = 1f;
    private float timer = 0f;
    private float duration = 1f; // Default duration (for medium network speed)

    private enum NetworkSpeed { Weak, Medium, Strong }
    private NetworkSpeed currentNetworkSpeed = NetworkSpeed.Medium;

    private bool isConnected = false;
    public string targetURL = "https://www.example.com";
    private Coroutine loadingCoroutine;

    private void Start()
    {
        StartCoroutine(CheckInternetAndLoad());
    }


    IEnumerator ConnectToNetwork()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(targetURL))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Connected to the network.");
                errorpanel.SetActive(false);
            }
            else
            {
                Debug.LogError("Failed to connect to the network: " + webRequest.error);
                errorpanel.SetActive(true);
            }
        }
    }
    private IEnumerator CheckInternetAndLoad()
    {
        while (true)
        {
            // Check for internet connectivity
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                isConnected = false;
                errorpanel.SetActive(true);
                StopLoading();
                Debug.LogWarning("No internet connection. Stopping loading.");
            }
            else
            {
                if (!isConnected)
                {
                    // Internet connection restored, start loading again
                    isConnected = true;
                    errorpanel.SetActive(false);
                    loadingCoroutine = StartCoroutine(StartLoading());
                }
            }

            yield return new WaitForSeconds(5f); // Check network status every few seconds (adjust as needed)
        }
    }

    private void StopLoading()
    {
        // Stop the loading process
        timer = duration;

        // Stop the coroutine if it's running
        if (loadingCoroutine != null)
        {
            StopCoroutine(loadingCoroutine);
        }
    }

    private float GetNetworkSpeedMultiplier(NetworkSpeed speed)
    {
        switch (speed)
        {
            case NetworkSpeed.Weak:
                return 2f;
            case NetworkSpeed.Strong:
                return 0.5f;
            default:
                return 1f;
        }
    }

    private IEnumerator StartLoading()
    {
        // Determine the actual duration based on the network speed
        float actualDuration = duration * GetNetworkSpeedMultiplier(currentNetworkSpeed);

        while (timer < actualDuration)
        {
            timer += Time.deltaTime;
            currentProgress = Mathf.Lerp(0, targetProgress, timer / actualDuration);

            // Update UI elements
            CircleImg.fillAmount = currentProgress;
            textProgress.text = Mathf.Floor(currentProgress * 100).ToString();
            FxHolder.rotation = Quaternion.Euler(new Vector3(0f, 0f, -currentProgress * 360));

            yield return null;
        }

        // Loading is complete
        timer = 0f;
        targetProgress = 1f;

      
    }
   public void Restart()
    {
        if(isConnected == true)
        {
            isConnected = true;
            errorpanel.SetActive(false);
            StartCoroutine(StartLoading());
        }
        else
        {
            if (isConnected == false)
            {
                isConnected = false;
                errorpanel.SetActive(true);
               
            }
           
        }
       
     

    }
}
