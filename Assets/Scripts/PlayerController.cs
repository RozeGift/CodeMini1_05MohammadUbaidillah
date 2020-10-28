using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 10.0f;
    float maxLimit = 10.0f;
    float planeBz = 19.5f;
    float planeBx = 5.0f;
    float gravitymodifier = 5.0f;
    float jumpcounter = 0;
    bool Plane;

    Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravitymodifier;
    }



    // Update is called once per frame
    void Update()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");



        transform.Translate(Vector3.forward * Time.deltaTime * VerticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * HorizontalInput * speed);
        jumpPlayer();




        if (Plane == true)
        {
            Debug.Log("PlaneA");
            if (transform.position.x < -maxLimit)
            {
                transform.position = new Vector3(-maxLimit, transform.position.y, transform.position.z);
            }
            if (transform.position.x > maxLimit)
            {
                transform.position = new Vector3(maxLimit, transform.position.y, transform.position.z);
            }
            if (transform.position.z < -maxLimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -maxLimit);
            }
            if (transform.position.z > maxLimit && transform.position.x > planeBx)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxLimit);
            }
            if (transform.position.z > maxLimit && transform.position.x < -planeBx)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxLimit);

            }
            
        }
        if (Plane == false)
        {
            Debug.Log("PlaneB");
            if (transform.position.x < -planeBx)
            {
                transform.position = new Vector3(-planeBx, transform.position.y, transform.position.z);
            }
            if (transform.position.x > planeBx)
            {
                transform.position = new Vector3(planeBx, transform.position.y, transform.position.z);
            }
           
            if (transform.position.z > planeBz)
            {

                transform.position = new Vector3(transform.position.x, transform.position.y, planeBz);
            }
        }
    }
    private void jumpPlayer()
    {
        if (jumpcounter == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRb.AddForce(Vector3.up * 4 * gravitymodifier, ForceMode.Impulse);
                jumpcounter = 1;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlaneA"))
        {
            Debug.Log("Plane A");
            Plane = true;
            jumpcounter = 0;

        }
        if (collision.gameObject.CompareTag("PlaneB"))
        {
            Debug.Log("Plane B");

            Plane = false;
            jumpcounter = 0;
        }



    }
}
