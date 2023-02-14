using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Vector3 movement = Vector2.zero;
    Rigidbody rb;
    [SerializeField] float power = 20f;
    [SerializeField] GameObject default_hinge;

    // Rope Stuff 
    GameObject TouchingRopeable = null;
    GameObject roped = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.z = Input.GetAxis("Horizontal") * power;
        movement.x = Input.GetAxis("Vertical") * power;

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (TouchingRopeable != null && roped != null)
            {
                //RopeMaker.makeRope(TouchingRopeable, roped);
            }
            else if (TouchingRopeable == null && roped != null)
            {
                Debug.Log("Connected");

                RopeMaker.makeRope(gameObject, roped);
                roped = null;
            }
            else if (TouchingRopeable != null)
            {
                Debug.Log("Roped");
                roped = TouchingRopeable;

            }
        }
    }
    private void FixedUpdate()
    {

        rb.AddForce(movement);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (TouchingRopeable == null && collision.gameObject.tag == "ropeable")
        {
            Debug.Log("Connection");
            TouchingRopeable = collision.gameObject;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (TouchingRopeable != null && collision.gameObject == TouchingRopeable)
        {
            Debug.Log("DisConnected");
            TouchingRopeable = null;
        }
    }
}
