using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator Screenshot()
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();
        string name = "Screenshot_EpicApp" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        //PC
        //byte[] bytes = texture.Encode ToPNG();
        //File.WriteAllBytes (Application.dataPath + "/../" + name, bytes);
        //MOBILE
        NativeGallery.SaveImageToGallery(texture, "My app pictures", name);


        Destroy(texture);
        UI.SetActive(true);
    }

    public void TakeScreenshot()
    {
        UI.SetActive(false);
        StartCoroutine("Screenshot");
    }
}
