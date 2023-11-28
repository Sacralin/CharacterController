using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    public float rotationSpeed = 100.0f;

    private float gravity = -9.81f; // earths gravity
    private CharacterController characterController;
    private Vector3 velocity;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(0, 0, vertical).normalized;

        // rotate the character 
        if (horizontal != 0)
        {
            transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);
        }
        
        // move the character
        if(direction.magnitude >= 0.1f)
        {
            if(vertical >= 0)
            {
                characterController.Move(transform.forward * vertical * movementSpeed * Time.deltaTime);
            }
            else
            {
                characterController.Move(-transform.forward * -vertical * movementSpeed * Time.deltaTime);
            }
        }

        // apply gravity
        if(characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        // update animations parameters
        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(vertical));
            animator.SetFloat("Direction", horizontal);
        }

    }
}
