using UnityEngine;

public class FishMovement : MonoBehaviour 
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationSpeed = 180f; // degrees per second
    
    [Header("Animation")]
    [SerializeField] private Animator fishAnimator;
    [SerializeField] private string speedParameterName = "Speed";
    
    private Rigidbody rb;
    private bool isRotating = false;
    private Vector3 currentDirection = Vector3.right;
    
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        if (fishAnimator == null)
            fishAnimator = GetComponent<Animator>();
            
        
        if (rb != null)
        {
            rb.useGravity = false;
            rb.freezeRotation = true; 
        }
        
        
        UpdateAnimation();
    }
    
    void FixedUpdate()
    {
        if (!isRotating)
        {
            
            Vector3 movement = currentDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + movement);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("TankWall") && !isRotating)
        {
            StartCoroutine(RotateAndContinue());
        }
    }
    
    private System.Collections.IEnumerator RotateAndContinue()
    {
        isRotating = true;
        
        
        UpdateAnimation(0f);
        
        
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0, 180, 0);
        
        
        float rotationTime = 0f;
        float rotationDuration = 180f / rotationSpeed; 
        
        while (rotationTime < rotationDuration)
        {
            rotationTime += Time.deltaTime;
            float progress = rotationTime / rotationDuration;
            
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, progress);
            yield return null;
        }
        
        
        transform.rotation = targetRotation;
        
        
        currentDirection = -currentDirection;
        
        
        isRotating = false;
        UpdateAnimation();
    }
    
    private void UpdateAnimation(float? speedOverride = null)
    {
        if (fishAnimator != null)
        {
            float animationSpeed = speedOverride ?? (isRotating ? 0f : moveSpeed);
            
            
            if (HasParameter(fishAnimator, speedParameterName))
            {
                fishAnimator.SetFloat(speedParameterName, animationSpeed);
            }
        }
    }
    
   
    private bool HasParameter(Animator animator, string parameterName)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == parameterName)
                return true;
        }
        return false;
    }
    
    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, currentDirection * 2f);
    }
    
    
    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = Mathf.Max(0f, newSpeed);
        if (!isRotating) UpdateAnimation();
    }
    
    public void SetDirection(Vector3 newDirection)
    {
        if (!isRotating)
        {
            currentDirection = newDirection.normalized;
        }
    }
    
    public bool IsMoving => !isRotating;
    public Vector3 CurrentDirection => currentDirection;
}