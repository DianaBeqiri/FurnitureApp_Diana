using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChangeMenu : MonoBehaviour
{
    public GameObject catalogueControls;
    //public GameObject moveControls;
    public GameObject rotateControls;
    public GameObject baba;
    public GameObject currentlyDisplayed;
    public GameObject deleteControls;
    public GameObject ghost;
    public GameObject placementIndicator;
    public GameObject btn1;
    public GameObject btn2;
    public bool isSectionDisplayedToggle = true;
    void Start()
    {
        /*
        catalogueControls.SetActive(true);
        currentlyDisplayed = catalogueControls;
        */
    }
    
    public void SetCatalogue()
    {
        ToggleMenu(catalogueControls);
    }
    public void SetCatalogue1()
    {
        ToggleMenu(baba);
    }
  
    public void SetRotate()
    {
        ToggleMenu(rotateControls);
    }
    
    public void SetDelete()
    {
        ToggleMenu(deleteControls);
    }
    
    private void ToggleMenu(GameObject menu)
    {
        if (currentlyDisplayed == catalogueControls)
        {
            Destroy(ghost);
            placementIndicator.SetActive(false);
           
        }

        /*
      
        if (!menu.activeInHierarchy)
        {
            btn1.SetActive(false);
            btn2.SetActive(false);
            placementIndicator.SetActive(false);

        }
        else
        {
            btn1.SetActive(true);
            //btn2.SetActive(true);
            //placementIndicator.SetActive(true);

        }
        */
        if (currentlyDisplayed != menu)
        {
            currentlyDisplayed.SetActive(false);
            menu.SetActive(true);
            currentlyDisplayed = menu;
            isSectionDisplayedToggle = true;
          
        }
        else
        {
            currentlyDisplayed.SetActive(!isSectionDisplayedToggle);
            isSectionDisplayedToggle = !isSectionDisplayedToggle;
           

        }
    }

  
}
