using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;

    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("첫 마을 방문" , 
            new int[] { 1000, 2000 })); // 퀘스트와 관련된 npc의 id를 입력해놓음
        questList.Add(20, new QuestData("루도의 동전 찾아주기.",
            new int[] { 5000, 2000 }));
        questList.Add(30, new QuestData("퀘스트 올 클리어!",
            new int[] { 0 }));
    }

    //오버로딩

    public string checkQuest()
    {
        return questList[questId].questName;
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string checkQuest(int id) //npc id를 받아옴
    {
        //Next Talk Target
        if(id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        //Control Quest Object
        ControlObject();

        //Talk Complete & Next Quest
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        //Quest Name Return
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }
    void ControlObject()
    {
        switch (questId)
        {
            case 10:
                if (questActionIndex == 2)
                    questObject[0].SetActive(true);
                break;
            case 20:
                if(questActionIndex == 1) //동전을 먹었을때
                    questObject[0].SetActive(false);
                break;
        }
    }
}
