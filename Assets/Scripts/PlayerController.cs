using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Vector3 movement = Vector2.zero;
    Rigidbody rb;
    [SerializeField] float power = 20f;
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
    }
    private void FixedUpdate()
    {
        Debug.Log(movement);
        rb.AddForce(movement);
    }
}
