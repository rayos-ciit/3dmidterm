using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;
    
    // Index 1 corresponds to the second layer ("Upper Body") created in the Animator
    private int upperBodyLayerIndex = 1; 

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleWaving();
    }

    private void HandleMovement()
    {
        // Get standard WASD or Arrow key input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 movementInput = new Vector2(horizontal, vertical);
        
        float targetSpeed = 0f;

        // If the player is providing input, set speed to Walk (0.5) or Run (1.0)
        if (movementInput.magnitude > 0.1f)
        {
            // Hold Left Shift to run, otherwise walk
            targetSpeed = Input.GetKey(KeyCode.LeftShift) ? 1f : 0.5f; 
        }

        // Smoothly blend the "Speed" parameter in the Animator
        float currentSpeed = animator.GetFloat("Speed");
        animator.SetFloat("Speed", Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 10f));
    }

    private void HandleWaving()
    {
        // GetMouseButton(1) returns true as long as the Right Mouse Button is held
        bool isHoldingRightClick = Input.GetMouseButton(1);
        
        // Target weight: 1 (fully waving) if holding right click, 0 (no wave) if not
        float targetWeight = isHoldingRightClick ? 1f : 0f;
        
        // Smoothly blend the weight of the Upper Body layer so the arms don't snap
        float currentWeight = animator.GetLayerWeight(upperBodyLayerIndex);
        animator.SetLayerWeight(upperBodyLayerIndex, Mathf.Lerp(currentWeight, targetWeight, Time.deltaTime * 15f));
    }
}