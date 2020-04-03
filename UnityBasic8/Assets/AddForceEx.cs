using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceEx : MonoBehaviour
{

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        //rigidbody 초기화
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vt3 = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical"));
        rb.AddForce(vt3, ForceMode.Impulse);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Cube")
        {
            rb.AddForce(Vector3.up * 2, ForceMode.Impulse);
        }
    }

    /* 정리 
    
    1. 실제 물리적인 충돌로 발생하는 이벤트

    void OnCollisionEnter(Collision collision)
    void OnCollisionStay(Collision collision)
    void OnCollisionExit(Collision collision)


    2. Collider 충돌로 발생하는 이벤트 
    (충돌대상이 되는 Material에 BoxCollider에 Is Trigger 체크 ex> Cube)

    void OnTriggerEnter(Collider other)
    void OnTriggerStay(Collider other)
    void OnTriggerExit(Collider other)
     
    */

}
