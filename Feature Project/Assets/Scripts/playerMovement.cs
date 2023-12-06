using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    //public float jump_force;
    private Rigidbody rigid_body;
    public float jumpVelocity;
    public float speed;
    public bool isGrounded;
    public GameObject MegaBlaster;
    private Vector3 startPos;
    public float stunTimer;

    void Start()
    {
        rigid_body = GetComponent<Rigidbody>();
        DontDestroyOnLoad(gameObject);
        startPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If Mega Man grabs the big bullets upgrade:
        if (other.tag == "BigRocket")
        {
            GetComponentInChildren<MegaBlaster>().ChangeBullet();
            other.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        movement();
        jump();
        Vector3 add_position = Vector3.zero;
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
        Debug.Log(hit.distance);
        if (hit.distance <= 2.5f)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    // This will allow the player to move up using spacebar/jump
    private void jump()
    {
        // Get keyboard input and test to see if we have pressed "w"
        if (Input.GetKey("w") && isGrounded)
        {
            rigid_body.AddForce(Vector3.up * jumpVelocity);
        }
    }

    // This will allow the player to move left and right by using A and D.
    private void movement()
    {
        Vector3 add_position = Vector3.zero;
        // Get the keyboard's input and test to see if A or D was pressed
        if (Input.GetKey("a"))
        {
            add_position += Vector3.left * Time.deltaTime * speed;
            // This will allow the mega blaster to rotate left with every "A" press.
            transform.Find("MegaBlaster").rotation = Quaternion.Euler(0, 180, 0);
            transform.Find("MegaBlaster").GetComponentInChildren<MegaBlaster>().goingLeft = true;
        }
        if (Input.GetKey("d"))
        {
            add_position += Vector3.right * Time.deltaTime * speed;
            // This will allow the mega blaster to rotate right with every "D" press.
            transform.Find("MegaBlaster").rotation = Quaternion.Euler(0, 0, 0);
            transform.Find("MegaBlaster").GetComponentInChildren<MegaBlaster>().goingLeft = false;
        }
        // Then apply the add_position code to the player model
        GetComponent<Transform>().position += add_position;
    }

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag(this.tag).Length != 1)
        {
            Destroy(this.gameObject);
        }

    }
}
