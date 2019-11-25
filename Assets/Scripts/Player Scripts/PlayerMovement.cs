using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private CharacterController character_Controller;

    private Vector3 move_direction;

    public float speed = 25f;
    public float sprint_speed = 50f;
    private float gravity = 20f;

    public float jump_Force = 10f;
    private float vertical_Velocity;

    void Awake() {
        character_Controller = GetComponent<CharacterController>();
    }
	// Update is called once per frame
	void Update () {
        MoveThePlayer();
	}

    void MoveThePlayer() {

        move_direction = new Vector3(
                                    Input.GetAxis(Axis.HORIZONTAL),
                                    0f,
                                    Input.GetAxis(Axis.VERTICAL)
                                    );

        move_direction = transform.TransformDirection(move_direction);
        move_direction *= speed * Time.deltaTime;

        ApplyGravity();
        Sprint();

        character_Controller.Move(move_direction);
    }

    void ApplyGravity() {

        if (character_Controller.isGrounded) {
            vertical_Velocity -= gravity * Time.deltaTime;

            // jump
            PlayerJump();
        } else {
            vertical_Velocity -= gravity * Time.deltaTime;
        }

        move_direction.y = vertical_Velocity * Time.deltaTime;
    }

    void PlayerJump() {
        if (character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            vertical_Velocity = jump_Force;
        }
    }

    void Sprint() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            speed = sprint_speed;
        } else {
            while (speed > 5f) {
                speed -= 0.01f *Time.deltaTime;
            }
            speed = 5f;
        }
    }
}
