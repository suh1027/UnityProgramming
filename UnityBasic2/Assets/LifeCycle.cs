using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour
{
    //1. 초기화

    void Awake() //게임 오브젝트 생성시, 최초실행
    {
        Debug.Log("플레이어 데이터가 준비되었습니다.");
    }
    private void OnEnable()
    {
        Debug.Log("플레이어 로그인");
    }
    private void Start() // 업데이트 시작 직전, 최초실행
    {
        Debug.Log("사냥 장비를 챙겼습니다.");
    }

    //2. 물리연산
    private void FixedUpdate() //물리연산을 하기 전에 실행되는 함수
    {
        Debug.Log("이동~");
    }
    private void Update()
    {
        //물리연산을 제외한 주기적인 변화에 쓰는 함수
        Debug.Log("몬스터 사냥!!");
    }
    void LateUpdate()
    { //마지막 업데이트 모든 업데이트가 끝난후에 실행
        Debug.Log("경험치 획득");
    }

 
    // 해체
    void OnDisable()
    {
        Debug.Log("플레이어 로그아웃");
    }

    void OnDestroy() //게임오브젝트 삭제시 활성화가되네?
    {
        Debug.Log("플레이어 데이터를 해제하였습니다.");
    }

    // 프레임
    // 초기화 --- (활성화)-- > 물리 -> 게임로직 --- (비활성화) --> 해체

}
