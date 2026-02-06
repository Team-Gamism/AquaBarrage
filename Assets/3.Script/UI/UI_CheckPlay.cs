using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_CheckPlay : MonoBehaviour
{
    [SerializeField] StageInfoSO stage1Info;
    [SerializeField] StageInfoSO tutorialInfo;
    
    [SerializeField] AudioClip clickAudio;

    public GameObject settingUI;

    Text playText;
    Text tutorialText;
    Text leaveText;
    Text settingText;

    private void Start()
    {
        playText = Util.FindChild<Text>(gameObject,"Play");
        tutorialText = Util.FindChild<Text>(gameObject,"Tutorial");
        leaveText = Util.FindChild<Text>(gameObject,"Leave");
        settingText = Util.FindChild<Text>(gameObject, "Setting");

        UI_EventHandler evt = playText.GetComponent<UI_EventHandler>();
        evt._OnClick += Play;
        evt._OnEnter += (PointerEventData p) => { playText.fontSize = 65; };
        evt._OnExit += (PointerEventData p) => { playText.fontSize = 50; };

        evt = tutorialText.GetComponent<UI_EventHandler>();
        evt._OnClick += Tutorial;
        evt._OnEnter += (PointerEventData p) => { tutorialText.fontSize = 65; };
        evt._OnExit += (PointerEventData p) => { tutorialText.fontSize = 50; };

        evt = settingText.GetComponent<UI_EventHandler>();
        evt._OnClick += (PointerEventData p) => { Instantiate(settingUI); };
        evt._OnEnter += (PointerEventData p) => { settingText.fontSize = 65; };
        evt._OnExit += (PointerEventData p) => { settingText.fontSize = 50; };

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

        yield return FishPreloadController.Instance
            .Preload(stage1Info.fishSpawnList);

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

        yield return FishPreloadController.Instance
            .Preload(tutorialInfo.fishSpawnList);

        SceneManager.LoadScene("HelpScene");
    }

    public void Leave(PointerEventData p)
    {
       Application.Quit();
    }
}
