using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PetrushevskiApps.Utilities;
using UnityEngine.SceneManagement;

public class ConnectionManager : MonoBehaviour
{
    public ConnectivityManager _maneger;


    public bool isInternet;

    //public SceneManager GamePlay;
    // Start is called before the first frame update
    void Start()
    {
        //targetURL.SetActive(false);
        Debug.Log("u nal"); 
    }

    // Update is called once per frame
     void Update()
    {
        isInternet = _maneger.IsConnected;
       
        if (_maneger.IsConnected == true)
        {
            //SceneManager.LoadScene("GamePlay");

        }

        if (_maneger.IsConnected == false)
        {
            //targetURL.SetActive(true);
            //SceneManager.LoadScene("GamePlay");
        }
        //return;
    }
  


}
