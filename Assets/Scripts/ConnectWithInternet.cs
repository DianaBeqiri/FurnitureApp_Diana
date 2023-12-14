using PetrushevskiApps.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectWithInternet : MonoBehaviour
{
    public ConnectivityManager _maneger1;
    public GameObject url;

    public bool Internet;

    //public SceneManager GamePlay;
    // Start is called before the first frame update
    void Start()
    {
        //targetURL.SetActive(false);
        //Debug.Log("u nal");
    }

    // Update is called once per frame
    void Update()
    {
        Internet = _maneger1.IsConnected;

        if (_maneger1.IsConnected == true)
        {
            //SceneManager.LoadScene("GamePlay");
            url.SetActive(false);

        }

        if (_maneger1.IsConnected == false)
        {
            url.SetActive(true);
            //SceneManager.LoadScene("GamePlay");
        }
        //return;
    }

}
