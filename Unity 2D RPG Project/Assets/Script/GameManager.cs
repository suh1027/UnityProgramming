using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    //public GameObject talkPanel;
    public Animator talkPanel;
    public Animator portraitAnim;

    public Text talkText;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;
    public Image npcImage;
    public Sprite prevPortrait;

    void Start()
    {
        Debug.Log(questManager.checkQuest());
    }

    public void UI_Action(GameObject scanObj)
    { 
        isAction = true;
        scanObject = scanObj;

        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);


        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);


        //END TALK
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.checkQuest(id));
            return;
        }

        //NPC와 Object를 나눈 이유?
        //NPC와 대화할떄 초상화가 나오도록 출력!

        if (isNpc) {
            talkText.text = talkData.Split(':')[0];
            npcImage.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            npcImage.color = new Color(1, 1, 1, 1);
            //과거에 바꾼 sprite와 다른 sprite가 들어왔을때
            if (prevPortrait != npcImage.sprite) { 
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = npcImage.sprite;
            }

        }
        else {
            talkText.text = talkData;
            npcImage.color = new Color(1, 1, 1, 0);//투명도를 0으로 하여 안보이게 하면된다
        }

        isAction = true;
        talkIndex++;
    }
}
