using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    // Main arsenal
    private Rigidbody rigid_body;
    public float jumpVelocity;
    public float speed;
    public bool isGrounded;
    public GameObject MegaBlaster;
    private Vector3 startPos;
    public float stunTimer;

    // For player damage and respawn if in contact with enemy, as well as temp invincibility frame
    public bool is_invincible = false;
    public int health;

    void Start()
    {
        rigid_body = GetComponent<Rigidbody>();
        DontDestroyOnLoad(gameObject);
        startPos = transform.position;
    }

    //What happens when a specific thing touches the player
    private void OnTriggerEnter(Collider other)
    {
        // If Mega Man grabs the big bullets upgrade:
        if (other.tag == "BigRocket")
        {
            GetComponentInChildren<MegaBlaster>().ChangeBullet();
            other.gameObject.SetActive(false);
        }

        //If Mega Man collides with a Met or bigger enemy, this triggers the Hurt function
        if (is_invincible == false)
        {
            if (other.tag == "Enemy")
            {
                Hurt(15);
            }
            if (other.tag == "BigEnemy")
            {
                Hurt(65);
            }
        }
    }

    // This will make Mega Man lose health and be temporarily invincible
    private void Hurt(int damage)
    {
        health -= damage;
        transform.position = startPos;
        StartCoroutine(Blink());
        // Update text for amount of health
        // If the player's out of health, then they die
    }

    // The temporary invinibility frames will trigger once the player dies, causing them to visibly to blink
    private IEnumerator Blink()
    {
        is_invincible = true;
        for (int index = 0; index < 30; index++)
        {
            if (index % 2 == 0)
            {
                GetComponent<MeshRenderer>().enabled = false;
                MegaBlaster.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                GetComponent<MeshRenderer>().enabled = true;
                MegaBlaster.GetComponent<MeshRenderer>().enabled = true;
            }
            yield return new WaitForSeconds(.2f);
        }
        GetComponent<MeshRenderer>().enabled = true;
        is_invincible = false;
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
            transform.Find("AllBody").rotation = Quaternion.Euler(0, 180, 0);
            transform.Find("AllBody").Find("MegaBlaster").GetComponentInChildren<MegaBlaster>().goingLeft = true;
        }
        if (Input.GetKey("d"))
        {
            add_position += Vector3.right * Time.deltaTime * speed;
            // This will allow the mega blaster to rotate right with every "D" press.
            transform.Find("AllBody").rotation = Quaternion.Euler(0, 0, 0);
            transform.Find("AllBody").Find("MegaBlaster").GetComponentInChildren<MegaBlaster>().goingLeft = false;
        }
        // Then apply the add_position code to the player model
        GetComponent<Transform>().position += add_position;
    }
}
