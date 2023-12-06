using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject leftPoint;
    public GameObject rightPoint;
    private Vector3 leftPos;
    private Vector3 rightPos;
    public int speed;
    public bool goingLeft;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        //Left position grabs the transform from the leftPoint GameObject
        leftPos = leftPoint.transform.position;
        //Right position grabs the transform from the leftPoint GameObject
        rightPos = rightPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    //This will move the enemy back and forth on the x axis
    private void Movement()
    {
        //If goingLeft is true then do this
        if (goingLeft)
        {
            //If the enemy's x position is less than or equal to the leftPos
            if (transform.position.x <= leftPos.x)
            {
                //Then it is not goingLeft anymore
                goingLeft = false;
            }
            else
            {
                //going left
                transform.position += Vector3.left * Time.deltaTime * speed;
            }
        }
        //If the enemy is not goingLeft then do this
        else
        {
            //If the enemy's x position is greater than or equal to the rightPos
            if (transform.position.x >= rightPos.x)
            {
                //Then it is not going right anymore
                goingLeft = true;
            }
            else
            {
                //going right
                transform.position += Vector3.right * Time.deltaTime * speed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If player shoots an enemy with a laser, this will trigger Hurt
        if (other.tag == "Laser")
        {
            Hurt(1);
            Destroy(other.gameObject);
        }

        //If player shoots an enemy with a bullet, this will trigger Hurt
        if (other.tag == "Bullet")
        {
            Hurt(3);
            Destroy(other.gameObject);
        }
    }

    //Hurt enemies
    private void Hurt(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
