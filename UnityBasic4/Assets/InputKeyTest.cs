using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyTest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("플레이어가 아무키를 눌렀습니다.");
        }
        if (Input.anyKey)
        {
            Debug.Log("플레이어가 키를 꾹 누르고 있습니다.");
        }

        // 키보드 이벤트 -> Down Stay Up 행동으로 구분

        if (Input.GetKeyDown(KeyCode.Return)) // 1.Down 
                                              //enter는 리턴이랑 같음
        {
            Debug.Log("아이템을 구입하였습니다.");
        }
        if (Input.GetKey(KeyCode.LeftArrow)) // 2.Stay
        {
            Debug.Log("왼쪽으로 이동중");
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) //  3.Up 
        {
            Debug.Log("오른쪽으로 이동을 멈췄습니다.");
        }


        // 마우스 이벤트 Down Stay Up
        // GetMouse... 함수-> 입력을 받으면 true
        // 매개변수 0 -> 왼쪽클릭 1 -> 오른쪽클릭
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("미사일 발사!");
        }
        if (Input.GetMouseButton(0))
        {
            Debug.Log("미사일 모으는중....");
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("슈퍼 미사일 발사!");
        }

        //Edit ProjectSetting Input 기본설정 사용
        //설정 확인하고 키워드로 사용 가능 Fire1, Jump, Mouse X ....
        //inputmanager 에서 size 올려서 사용자설정 키워드 가능

        if (Input.GetButtonDown("Jump")) { Debug.Log("점프!"); }
        if (Input.GetButton("Jump")) { Debug.Log("점프 모으는중.."); }
        if (Input.GetButtonUp("Jump")) { Debug.Log("슈퍼 점프!"); }

        if (Input.GetButton("Horizontal"))
        {
            Debug.Log("횡 이동 중.."
                + Input.GetAxisRaw("Horizontal"));
            // GetAxis(중간값 나오는..) , GetAxisRaw(가중치 없이)
        }
        if (Input.GetButton("Vertical"))
        {
            Debug.Log("종 이동 중.."
                + Input.GetAxis("Vertical"));

        }

        // Update에서 translte함수
        // 지속적으로 Update
        // MainCamera에도 Script를 넣어서 Cylinder와 같이 움직이도록 설정 가능
        Vector3 vec = new Vector3(
            Input.GetAxis("Horizontal"), 
            Input.GetAxis("Vertical"), //GetAxisRaw 로도 실험 
            0);
        transform.Translate(vec);
    }

    void Start()
    {
        // Transform 변수는 선언할 필요 없음
        // Vector2 -> 2차원 벡터 ,  Vector3 -> 3차원 벡터
        // int number = 4; //스칼라 값
        // Vector3 vt = new Vector3(0, 0, 0); // 벡터값 매개변수 x축 y축 z축

        // Vector3 vec = new Vector3(5,0,0);
        // transform.Translate(vec); //vec 값 만큼 이동 (transform -> cylinder)
  
    }
}
