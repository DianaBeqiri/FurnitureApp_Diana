using UnityEngine;

public class RemoveObject : MonoBehaviour
{
    /*
    public GameObject rotateBtn;
    public GameObject MoveBtn;
    public GameObject DeleteBtn;
    public GameObject placeBtn;
    */
    void Update()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.current.ScreenPointToRay(touch.position);
            RaycastHit hitObject;
            if (Physics.Raycast(ray, out hitObject))
            {
                Destroy(hitObject.transform.parent.transform.parent.gameObject);
                /*
                rotateBtn.SetActive(false);
                DeleteBtn.SetActive(false);
                MoveBtn.SetActive(false);
                placeBtn.SetActive(false);
                */
            }
        }       
    }
}
