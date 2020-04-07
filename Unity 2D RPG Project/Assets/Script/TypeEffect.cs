using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public int charPerSeconds;
    public GameObject endCursor;

    float interval;
    public bool isAnim;

    Text msgText;
    AudioSource audioSource;

    int index;
    string targetMsg;

    private void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void setMsg(string msg)
    {
        if (isAnim)
        {
            msgText.text = targetMsg;
            //invoke를 끄고 애니메이션을 끔
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }
    void EffectStart()
    {
        endCursor.SetActive(false);
        
        msgText.text = "";
        index = 0;

        interval = 1.0f / charPerSeconds;
        Debug.Log(interval);

        isAnim = true;
        //시간차 반복호출 Invoke 사용
        Invoke("Effecting", interval); //1글자가 나오는 딜레이
    }
    void Effecting()
    {
        if(msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        //Audio Sound
        //글자에서만 사운드가 나오도록 설정

        if(targetMsg[index] != ' ' || targetMsg[index] != '.') { 
            audioSource.Play();
        }

        msgText.text += targetMsg[index]; //문자열도 배열처럼 접근이 가능..ㅗㅜㅑ
        index++;

        interval = 1.0f / charPerSeconds;
        Invoke("Effecting", interval);
    }
    void EffectEnd()
    {
        isAnim = false;
        endCursor.SetActive(true);
    }
}
