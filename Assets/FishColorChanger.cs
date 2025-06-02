using UnityEngine;
using System.Collections.Generic;

public class FishColorChanger : MonoBehaviour
{
    [Header("Materials")]
    public Material yellowMaterial;
    public Material blueMaterial;
    public Material orangeMaterial;

    [Header("Fish Settings")]
    public string angelFishTag = "AngelFish";
    
    public string fishBodyObjectName = "EmperorAngelfish_body";

    private List<Renderer> fishBodyRenderers = new List<Renderer>();

    void Start()
    {
        PopulateFishRenderers();
    }

    void PopulateFishRenderers()
    {
        fishBodyRenderers.Clear(); 
        GameObject[] angelFishes = GameObject.FindGameObjectsWithTag(angelFishTag);

        if (angelFishes.Length == 0)
        {
            Debug.LogWarning($"No GameObjects found with tag '{angelFishTag}'. Fish color changing will not work.");
            return;
        }

        foreach (GameObject fish in angelFishes)
        {
            Transform bodyTransform = fish.transform.Find(fishBodyObjectName);
            if (bodyTransform != null)
            {
                Renderer bodyRenderer = bodyTransform.GetComponent<Renderer>(); 
                if (bodyRenderer != null)
                {
                    fishBodyRenderers.Add(bodyRenderer);
                }
                else
                {
                    Debug.LogWarning($"No Renderer component found on '{fishBodyObjectName}' child of '{fish.name}'.");
                }
            }
            else
            {
                Debug.LogWarning($"Could not find child object named '{fishBodyObjectName}' in '{fish.name}'.");
            }
        }

        Debug.Log($"Found {fishBodyRenderers.Count} fish body renderers to control.");
    }

    
    public void SetColorYellow()
    {
        if (yellowMaterial == null)
        {
            Debug.LogError("Yellow Material not assigned in FishColorChanger script!");
            return;
        }
        ApplyMaterialToAllFish(yellowMaterial);
    }

    
    public void SetColorBlue()
    {
        if (blueMaterial == null)
        {
            Debug.LogError("Blue Material not assigned in FishColorChanger script!");
            return;
        }
        ApplyMaterialToAllFish(blueMaterial);
    }

    
    public void SetColorOrange()
    {
        if (orangeMaterial == null)
        {
            Debug.LogError("Orange Material not assigned in FishColorChanger script!");
            return;
        }
        ApplyMaterialToAllFish(orangeMaterial);
    }

    private void ApplyMaterialToAllFish(Material materialToApply)
    {
        if (fishBodyRenderers.Count == 0)
        {
            Debug.LogWarning("No fish renderers available to apply material. Trying to repopulate.");
            PopulateFishRenderers(); // Attempt to find them again
            if (fishBodyRenderers.Count == 0)
            {
                Debug.LogError("Still no fish renderers found. Cannot apply material.");
                return;
            }
        }

        foreach (Renderer bodyRenderer in fishBodyRenderers)
        {
            bodyRenderer.material = materialToApply;
        }
        Debug.Log($"Applied material '{materialToApply.name}' to {fishBodyRenderers.Count} fish.");
    }

 
    public void RefreshFishList()
    {
        PopulateFishRenderers();
    }
}