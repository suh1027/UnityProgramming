using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBalls : MonoBehaviour
{
    MeshRenderer mesh;
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
    }

    // # 물리충돌 이벤트 
    
    // OncollisionEnter -> 물리충돌이 시작될때 호출되는 함수.
    private void OnCollisionEnter(Collision collision) 
        //Collision 충돌정보 클래스
    {
        if(collision.gameObject.name == "CenterBall") { 
            // color : 기본색상 클래스, color32 : 255 색상 클래스
            mat.color = new Color(0, 0, 0); //(r,g,b)
        }
    }

    
    // OnCollisionStay -> 물리적충돌이 지속될떄 호출되는 함수
    private void OnCollisionStay(Collision collision)
    {
        
    }

    // OnCollisionExit -> 물리적 충돌이 끝났을때 호출되는 함수.
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.name == "CenterBall")
        {
            mat.color = new Color(1, 1, 1);//흰색
        }
    }
    

}
