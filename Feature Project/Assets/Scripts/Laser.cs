using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Projectile Variables
    public float speed;
    public bool goingLeft;
    public int vanish;
    public bool bigBullet;

    void Update()
    {
        // Laser moving
        if (goingLeft == true)
        {
            transform.position += speed * Vector3.left * Time.deltaTime;
        }
        else
        {
            transform.position += speed * Vector3.right * Time.deltaTime;
        }
    }

    private void Start()
    {
        StartCoroutine(laserGone());
    }

    public void OnTriggerEnter(Collider other)
    {
        //If regular blue door...
        if (other.tag == "Door")
        {
            //...Then destroy blue door
            Destroy(other.gameObject);
        }

        //If big red door...
        if (other.tag == "BigDoor")
        {
            //If you have bigBullet...
            if (bigBullet)
            {
                //...Then destroy red door
                Destroy(other.gameObject);
            }
        }
    }

    //Makes Laser disappear after going far enough
    IEnumerator laserGone()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
