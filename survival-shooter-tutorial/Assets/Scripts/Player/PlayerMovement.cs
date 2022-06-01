using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public variables
    public float speed = 6f;

    //private variables
    Animator anim;
    float camRayLength = 100f;
    int floorMask;
    Rigidbody playerRigidBody;
    Vector3 movement;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");   //only get value of -1, 0, or 1 in x direction
        float v = Input.GetAxisRaw("Vertical");     //only get value of -1, 0, or 1 in y direction

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;    //makes sure that the player moves at the same speed in all directions
        playerRigidBody.MovePosition(transform.position + movement);    //actually move the character

    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;   //prevent character from leaning back
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidBody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;  //returns true if input has been pressed down
        anim.SetBool("IsWalking", walking);
    }
}
