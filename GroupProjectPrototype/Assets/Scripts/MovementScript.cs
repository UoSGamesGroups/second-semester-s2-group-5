using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour
{
    //Variables
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float platformSpeed = -1.0f;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        // is the controller on the ground?

        if (controller.isGrounded)
        {
            //Feed moveDirection with input.
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection *= speed;
            //Jumping
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            } else
            {
                moveDirection.y = platformSpeed;
            }

        }

        moveDirection.x = Input.GetAxis("Horizontal") * speed;
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move

        controller.Move(moveDirection * Time.deltaTime);

        if (transform.position.z != 0)
        {
            Vector3 newPosition = transform.position;
            newPosition.z = 0;
            transform.position = newPosition;
        }
    }
}