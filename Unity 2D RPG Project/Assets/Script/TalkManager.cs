using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portratitData;
    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portratitData = new Dictionary<int, Sprite>();
        GenerateData();
    }
    void GenerateData()
    {
        //기본대화 100 200 1000 2000 정리
        //대화랑 portraitIndex를 엮어줌 문장 뒤에 Index
        talkData.Add(1000, new string[] {
            "안녕? : 2",
            "눈을 왜 그렇게 떠? : 3",
            "미안 : 1",
            "잘가 : 0"
        });
        talkData.Add(200, new string[] {
            "딱딱..",
            "평범한 나무 상자이다."
        });
        talkData.Add(2000, new string[] {
            "눈이 풀려있다 : 4",
            "Zzz....졸려 : 5",
            "왜그렇게 생겼어? : 6",
            "화나게 하지마 : 7"
        });
        talkData.Add(100, new string[] {
            "퀘스트#1 한바퀴 돌기",
            "퀘스트#2 말걸기"
        });

        //Quest Talk + 10 20

        //10번대 퀘스트 여성NPC 말걸고 루도에게 말걸기 까지
        talkData.Add(10 + 1000, new string[] {"어서 와.:0",
            "이 마을에 놀라운 전설이 있다는데:1",
            "윗쪽 호수 쪽에 루도가 알려줄거야.:0" });

        talkData.Add(11 + 2000, new string[] {"여어~:5",
            "이 호수의 전설을 들으러 온거야?:4",
            "그럼 일좀 하나 해주면 좋을텐데....:4",
            "내 집 근처에 떨어진 동전좀 주워줬으면 해:6"});

        //20번대 퀘스트
        //동전을 찾아서 다시 루도에게 돌려주는 과정까지
        talkData.Add(20 + 1000, new string[] {"루도의 동전?:1", 
            "돈을 흘리고 다니면 못쓰지!:3", 
            "나중에 루도에게 한마디 해야겠어.:3"});
        
        talkData.Add(20 + 2000, new string[] { "찾으면 꼭 좀 가져다 줘." });
        talkData.Add(21 + 2000, new string[] { "엇, 찾아줘서 고마워:4" });
        
        talkData.Add(20 + 5000, new string[] { "근처에서 동전을 찾았다." });
        
        //이걸 왜 못읽어 오는건지 모르겟음..
        talkData.Add(30 + 2000, new string[] { "더미데이터 : 6" });


        portratitData.Add(1000 + 0, portraitArr[0]);
        portratitData.Add(1000 + 1, portraitArr[1]); //대화
        portratitData.Add(1000 + 2, portraitArr[2]); //웃
        portratitData.Add(1000 + 3, portraitArr[3]); //화

        portratitData.Add(2000 + 4, portraitArr[4]);
        portratitData.Add(2000 + 5, portraitArr[5]); //대화
        portratitData.Add(2000 + 6, portraitArr[6]); //웃
        portratitData.Add(2000 + 7, portraitArr[7]); //화

    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portratitData[id + portraitIndex];
    }

    public string GetTalk(int id, int talkIndex)
    {

        //토크 예외 처리
        if(!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10)){

                // 퀘스트 맨 처음 대사도 없을때
                // 퀘스트 기본대사를 가지고 온다.
                if (talkIndex == talkData[id - id % 100].Length)
                    return null;
                else
                    return talkData[id - id % 100][talkIndex];
            }
            else
            {
                //해당 퀘스트 진행 중 순서 대사가 없을 때
                //퀘스트 맨처음 대사를 가지고옴
                if (talkIndex == talkData[id - id % 10].Length)
                    return null;
                else
                    return talkData[id - id % 10][talkIndex];
            }

        }

        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex]; // 출력하는법 확인
        }
    }

}
