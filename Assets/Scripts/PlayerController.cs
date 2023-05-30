using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] float power = 20f;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private Transform cam;
    private float turnSmoothVelocity;
    [SerializeField] GameObject default_hinge;
    [SerializeField] CharacterController controller;
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
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f,  Input.GetAxisRaw("Vertical")).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

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
