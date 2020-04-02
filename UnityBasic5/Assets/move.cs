using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{

    Vector3 target = new Vector3(8, 1.5f, 0);

    // Update is called once per frame
    void Update()
    {

        //1. MoveToward 등속이동
        /*transform.position = 
            Vector3.MoveTowards(
                transform.position,
                target,
                1f);
*/
        /*   
           //2. SmoothDamp 방식 감속 이동
           Vector3 velo = Vector3.zero; // 속도를 0이라고 가정
                                        //Vector3 velo = Vector3.up * 50;

           transform.position =
               Vector3.SmoothDamp(
                   transform.position,
                   target,
                   ref velo,
                   //ref 참조접근 실시간으로 바뀌는값 적용가능
                   0.1f);
   */


        // 3. Lerp (선형보간)값을 적게줄수록 느리게 움직임
        //transform.position =
          //  Vector3.Lerp(transform.position, target, 0.05f);


        // 4. SLerp (구면 선형 보간) 호를 그리며 이동
        transform.position =
            Vector3.Slerp(transform.position, target, 0.05f);
    }
}
