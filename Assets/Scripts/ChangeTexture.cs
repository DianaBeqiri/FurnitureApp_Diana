using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeTexture : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gameObj; // Array of game objects
    [SerializeField]
    private Material[] materials; // Array of materials
    private int currentMaterialIndex = 0;
    [SerializeField]
    private Button[] buttons; // Array of buttons to assign specific materials
    private void Start()
    {
        // Attach button click events to the corresponding methods
        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i; // Store the current button index in a variable for the lambda
            buttons[i].onClick.AddListener(() => AssignSpecificMaterial(buttonIndex));
        }
        // Set the initial material for all walls
        //SetInitialMaterials();
    }
    private void SetInitialMaterials()
    {
        // Make sure there are materials and walls to assign them to
        if (materials.Length > 0 && gameObj.Length > 0)
        {
            foreach (GameObject wall in gameObj)
            {
                Renderer wallRenderer = wall.GetComponent<Renderer>();
                if (wallRenderer != null)
                {
                    wallRenderer.material = materials[currentMaterialIndex];
                }
            }
        }
    }
    private void AssignSpecificMaterial(int materialIndex)
    {
        // Assign a specific material to all walls when a button is pressed
        if (materialIndex >= 0 && materialIndex < materials.Length)
        {
            currentMaterialIndex = materialIndex;
            foreach (GameObject wall in gameObj)
            {
                Renderer wallRenderer = wall.GetComponent<Renderer>();
                if (wallRenderer != null)
                {
                    wallRenderer.material = materials[currentMaterialIndex];
                }
            }
        }
    }
}