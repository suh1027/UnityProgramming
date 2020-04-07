using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public string questName;
    public int[] npcId; //퀘스트와 연관된 npc의 id를 저장하는 배열

    //생성자 생성
    public QuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}
