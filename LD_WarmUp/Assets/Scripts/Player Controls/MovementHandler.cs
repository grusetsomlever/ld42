using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class MovementHandler : MonoBehaviour
{
    public float speed = 7;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    
    public AudioSource walkingNoise;
    public AudioSource jumpNoise;

    private Vector3 playerDirection = Vector3.zero;
    private CharacterController controller;
    private bool grounded = false;
    private bool falling;
    private int jumpTimer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        jumpTimer = 1;
    }

    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        // If both horizontal and vertical are used simultaneously, limit speed (if allowed), so the total doesn't exceed normal move speed
        float inputModifyFactor = (inputX != 0.0f && inputY != 0.0f) ? .7071f : 1.0f;

        if ((inputX != 0.0f || inputY != 0.0f))
        {
            Quaternion CameraRotata = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
            transform.GetChild(1).rotation = Quaternion.Lerp(transform.GetChild(1).rotation, CameraRotata, Time.deltaTime * 10f);
            if (!walkingNoise.isPlaying) {
                walkingNoise.Play();
            }
        }
        else {
            if (walkingNoise.isPlaying) {
                walkingNoise.Stop();
            }
        }

        if (grounded) {            
            if (falling)
                falling = false;

            Vector3 cameraForward = Camera.main.transform.TransformDirection(Vector3.forward);
            cameraForward.y = 0;

            Vector3 right = new Vector3(cameraForward.z, 0, -cameraForward.x);

            playerDirection = (inputX * right + inputY * cameraForward) * speed;

            if (!Input.GetKey(KeyCode.Space))
                jumpTimer++;
            else if (jumpTimer >= 1)
            {
                playerDirection.y = jumpSpeed;
                jumpTimer = 0;
                jumpNoise.Play();
            }
        }
        else {
            if (!falling) {
                falling = true;
            }

            if (walkingNoise.isPlaying) {
                walkingNoise.Stop();
            }            
        }

        playerDirection.y -= gravity * Time.deltaTime;
    }

    void Update()
    {
        grounded = (controller.Move(playerDirection * Time.deltaTime) & CollisionFlags.Below) != 0;



    }
}