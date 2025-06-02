using UnityEngine;
using TMPro; 
using System.Collections.Generic; 

public class FishCountManager : MonoBehaviour
{
    
    public TMP_Dropdown fishDropdownTMP; 
    public GameObject angelFish1;
    public GameObject angelFish2;
    public GameObject angelFish3;

    private List<GameObject> allAngelFish;

    void Start()
    {
        allAngelFish = new List<GameObject> { angelFish1, angelFish2, angelFish3 };

        if (fishDropdownTMP == null)
        {
            Debug.LogError("TextMeshPro Dropdown not assigned in the Inspector!");
            return;
        }

        // Add a listener to the dropdown.
        fishDropdownTMP.onValueChanged.AddListener(OnDropdownValueChanged);

     
        HandleFishVisibility(fishDropdownTMP.value); 
    }

    void OnDropdownValueChanged(int index)
    {
        HandleFishVisibility(index);
    }

    void HandleFishVisibility(int selectedIndex)
    {
       
        foreach (GameObject fish in allAngelFish)
        {
            if (fish != null)
            {
                fish.SetActive(false);
            }
        }


        if (selectedIndex == 0) // "Three"
        {
            if (angelFish1 != null) angelFish1.SetActive(true);
            if (angelFish2 != null) angelFish2.SetActive(true);
            if (angelFish3 != null) angelFish3.SetActive(true);
        }
        else if (selectedIndex == 1) // "Two"
        {
            if (angelFish1 != null) angelFish1.SetActive(true);
            if (angelFish2 != null) angelFish2.SetActive(true);
        }
        else if (selectedIndex == 2) // "One"
        {
            if (angelFish1 != null) angelFish1.SetActive(true);
        }
    }

    void OnValidate()
    {
        if (allAngelFish == null || allAngelFish.Count != 3)
        {
            allAngelFish = new List<GameObject>(3);
        }
        allAngelFish.Clear();
        allAngelFish.Add(angelFish1);
        allAngelFish.Add(angelFish2);
        allAngelFish.Add(angelFish3);
    }
}