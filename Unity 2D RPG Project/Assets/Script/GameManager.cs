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

    //public Text talkText;
    public TypeEffect talk;
    
    public bool isAction;
    public int talkIndex;
    
    public Image npcImage;
    public Sprite prevPortrait;
    public Text questText;

    public GameObject menuSet;
    public GameObject player;
    public GameObject scanObject;
    


    void Start()
    {
        GameLoad();
        Debug.Log(questManager.checkQuest());
        questText.text = questManager.checkQuest();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            //esc버튼으로 키고 꺼질수 있도록 설정
            if (menuSet.activeSelf)
            {
                menuSet.SetActive(false);
            }
            else
            {
                menuSet.SetActive(true);
            }
        }   
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
        int questTalkIndex = 0;
        string talkData = ""; 

        if (talk.isAnim)
        {
            talk.setMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }



        //END TALK
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.checkQuest(id));
            questText.text = questManager.checkQuest(id);
            return;
        }

        //NPC와 Object를 나눈 이유?
        //NPC와 대화할떄 초상화가 나오도록 출력!

        if (isNpc) {
            //talkText.text = talkData.Split(':')[0];

            talk.setMsg(talkData.Split(':')[0]);
            npcImage.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            npcImage.color = new Color(1, 1, 1, 1);
            //과거에 바꾼 sprite와 다른 sprite가 들어왔을때
            if (prevPortrait != npcImage.sprite) { 
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = npcImage.sprite;
            }

        }
        else {
            talk.setMsg(talkData);
            npcImage.color = new Color(1, 1, 1, 0);//투명도를 0으로 하여 안보이게 하면된다
        }

        isAction = true;
        talkIndex++;
    }

    //저장하기 버튼

    //플레이어의 x축 y축의 위치
    //퀘스트 정보 저장 (Quest Id, Quest Action Index)
    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY" , player.transform.position.y);
        PlayerPrefs.SetInt("QustId", questManager.questId);
        PlayerPrefs.SetInt("QustActionIndex", questManager.questActionIndex);

        PlayerPrefs.Save();//저장
        
        menuSet.SetActive(false);
    }


    //저장하기가 안된다.. 레지스트리 편집기에서 확인이..안되는데..?
    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            return; //한번도 저장을 하지 않았으면 return GameLoad()X
        }

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("Playery");
        int questId = PlayerPrefs.GetInt("QustId");
        int questActionIndex = PlayerPrefs.GetInt("QustActionIndex");

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObject();
    }

    //종료하기 버튼용 함수
    public void GameExit()
    {
        Application.Quit();
    }
}
