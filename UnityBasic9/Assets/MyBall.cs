using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBall : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 vt = new Vector3(h, 0, v);

        rb.AddForce(vt, ForceMode.Impulse);
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * 20, ForceMode.Impulse);
    }

}
