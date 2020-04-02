using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Myball : MonoBehaviour
{
    //Rigidbody 선언
    Rigidbody rb;

    // Start is called before the first frame update
    void Start() //게임 시작시 단 한번 실행
    {

        // #1. 속력 바꾸기

        //rigidbody 초기화
        rb = GetComponent<Rigidbody>();
        //rb.velocity = Vector3.right;
        //rb.velocity = Vector3.left;

        //rb.velocity = new Vector3(2, 4, 3);


        // #2. 힘을 가하기

        //rb.AddForce(Vector3.up * 5 ,ForceMode.Impulse);
        //impulse 값은 mass(무게)값에 영향을 받는다.
    }

    // Update is called once per frame
    /*void Update()
    {
        //rb.velocity = new Vector3(2, 4, 3);
        //안정된 물리효과를 받기 위해서는 FixedUpdate에 사용
    }
*/
    void FixedUpdate()
    {
        //rb.velocity = new Vector3(2, 4, 3);//속력바꾸기

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
        Vector3 vt3 = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            0,
            Input.GetAxisRaw("Vertical"));

        rb.AddForce(vt3, ForceMode.Impulse);
        /*Input.GetAxisRaw("Horizontal");
        Input.GetAxisRaw("Vertical");
        */

        // #3. 회전력
        rb.AddTorque(Vector3.down); 
        // 매개변수의 vector를 축으로 해서 돈다
    }
}
