using UnityEngine;

[RequireComponent(typeof(FishMovement))]
public class SharkFollow : MonoBehaviour 
{
    [Header("Follow Settings")]
    public Transform target; 
    public float followDistance = 3f; 
    public float followSpeed = 1f; 
    
    private FishMovement fishMovement;
    
    void Start()
    {
        fishMovement = GetComponent<FishMovement>();
        
        
        if (target == null)
        {
            GameObject angelfish = GameObject.Find("EmperorAngelfish");
            if (angelfish != null)
                target = angelfish.transform;
        }
    }
    
    void Update()
    {
        if (target == null || fishMovement == null) return;
        
        
        float distance = Vector3.Distance(transform.position, target.position);
        
       
        if (distance > followDistance)
        {
            // Calculate direction to target
            Vector3 direction = (target.position - transform.position).normalized;
            
            // Set the fish movement direction and speed
            fishMovement.SetDirection(direction);
            fishMovement.SetMoveSpeed(followSpeed);
        }
        else
        {
            // Stop or slow down when close enough
            fishMovement.SetMoveSpeed(0.2f);
        }
    }
}