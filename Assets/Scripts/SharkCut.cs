using System.Collections;
using UnityEngine;

public class SharkCut : MonoBehaviour
{
    [Header("Shark Cutting System")]
    [SerializeField] private GameObject sharkAlive;
    [SerializeField] private GameObject knife;
    [SerializeField] private GameObject blood;
    [SerializeField] private GameObject sharkCut;
    
    [Header("Settings")]
    [SerializeField] private float bloodDisplayDuration = 2f;
    
    private bool hasBeenCut = false;
    
    private void Start()
    {
        
        ValidateComponents();
    }
    
    private void OnTriggerEnter(Collider other)
    {
       
        if ((other.gameObject == knife || other.transform.parent?.gameObject == knife) && !hasBeenCut)
        {
            Debug.Log("Trigger detected with knife!");
            StartCoroutine(CutSharkSequence());
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        
        if ((collision.gameObject == knife || collision.transform.parent?.gameObject == knife) && !hasBeenCut)
        {
            Debug.Log("Collision detected with knife!");
            StartCoroutine(CutSharkSequence());
        }
    }
    
    private IEnumerator CutSharkSequence()
    {
        hasBeenCut = true;
        
       
        if (blood != null)
        {
            blood.SetActive(true);
        }
        
       
        yield return new WaitForSeconds(bloodDisplayDuration);
        
        // Disable the alive shark
        if (sharkAlive != null)
        {
            sharkAlive.SetActive(false);
        }
        
        
        if (sharkCut != null && sharkAlive != null)
        {
            sharkCut.transform.position = sharkAlive.transform.position;
            sharkCut.transform.rotation = sharkAlive.transform.rotation;
            
        }
        
        
        if (sharkCut != null)
        {
            sharkCut.SetActive(true);
        }
        
        
        if (blood != null)
        {
            blood.SetActive(false);
        }
    }
    
    private void ValidateComponents()
    {
        if (sharkAlive == null)
        {
            Debug.LogWarning("SharkCut: SharkAlive GameObject is not assigned!");
        }
        
        if (knife == null)
        {
            Debug.LogWarning("SharkCut: Knife GameObject is not assigned!");
        }
        
        if (blood == null)
        {
            Debug.LogWarning("SharkCut: Blood GameObject is not assigned!");
        }
        
        if (sharkCut == null)
        {
            Debug.LogWarning("SharkCut: SharkCut GameObject is not assigned!");
        }
    }
    
    
    public void ResetCuttingState()
    {
        hasBeenCut = false;
        
        if (sharkAlive != null)
            sharkAlive.SetActive(true);
            
        if (sharkCut != null)
            sharkCut.SetActive(false);
            
        if (blood != null)
            blood.SetActive(false);
    }
    
    
    public bool HasBeenCut
    {
        get { return hasBeenCut; }
    }
}