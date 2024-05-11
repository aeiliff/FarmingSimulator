using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerMovement : MonoBehaviour
{
    public float speed;
    public Animator animator;

    private Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, vertical);
        AnimateMovement(direction);
    }

    // Function to stop character from clipping through the building 
    // Called before updated
    void FixedUpdate() { 
        this.transform.position += direction.normalized * speed * Time.deltaTime;
    }

    void AnimateMovement(Vector3 direction) {
        if (animator != null) {       // If animator is not null do the following
            if (direction.magnitude > 0) {  // If the magnitude is greater than 0, then you are moving, so do the following 
                animator.SetBool("Moving", true);  // Set the animator variable "Moving" to true, so the character moves

                animator.SetFloat("Horizontal", direction.x);  // Set the parameter "Horizontal" to the x direction
                animator.SetFloat("Vertical", direction.y); // Set the parameter "Vertical" to the y direction
            }
            else {
                animator.SetBool("Moving", false);
            }
        }
    }
}
