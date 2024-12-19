using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_CheckPlay : MonoBehaviour
{
    [SerializeField] AudioClip clickAudio;

    Text playText;
    Text tutorialText;
    Text leaveText;

    private void Start()
    {
        playText = Util.FindChild<Text>(gameObject,"Play");
        tutorialText = Util.FindChild<Text>(gameObject,"Tutorial");
        leaveText = Util.FindChild<Text>(gameObject,"Leave");

        UI_EventHandler evt = playText.GetComponent<UI_EventHandler>();
        evt._OnClick += Play;
        evt._OnEnter += (PointerEventData p) => { playText.fontSize = 65; };
        evt._OnExit += (PointerEventData p) => { playText.fontSize = 50; };

        evt = tutorialText.GetComponent<UI_EventHandler>();
        evt._OnClick += Tutorial;
        evt._OnEnter += (PointerEventData p) => { tutorialText.fontSize = 65; };
        evt._OnExit += (PointerEventData p) => { tutorialText.fontSize = 50; };

        evt = leaveText.GetComponent<UI_EventHandler>();
        evt._OnClick += Leave;
        evt._OnEnter += (PointerEventData p) => { leaveText.fontSize = 65; };
        evt._OnExit += (PointerEventData p) => { leaveText.fontSize = 50; };
    }

    public void Play(PointerEventData p)
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        UI_Fade.instance.FadeIn();
        yield return new WaitForSeconds(2f);
        GameManager.Instance.isPlayGame = true;
        SceneManager.LoadScene("GameScene_UI_HC");
    }

    public void Tutorial(PointerEventData p)
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        StartCoroutine(LoadTutorialScene());
    }

    IEnumerator LoadTutorialScene()
    {
        UI_Fade.instance.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("HelpScene2");
    }

    public void Leave(PointerEventData p)
    {
       Application.Quit();
    }
}
