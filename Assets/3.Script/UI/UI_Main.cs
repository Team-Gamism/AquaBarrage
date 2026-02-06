using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Main : MonoBehaviour
{
    public GameObject settingUI;
    public GameObject checkPlayUI;
    [SerializeField] AudioClip clickAudio;

    Animator anySignAnimator;
    private void Start()
    {
        GameManager.Instance.InitData();
        AddressableManager.Instance.ReleaseAll();
        GameObject go = Instantiate(settingUI);
        go.GetComponent<UI_Setting>().FirstSet();
        Destroy(go);

        anySignAnimator = Util.FindChild<Animator>(gameObject,"Any");

        StartCoroutine(CheckAnyKey());
    }

    IEnumerator CheckAnyKey()
    {
        while (true)
        {
            yield return null;
            if (!Input.anyKeyDown)
                continue;

            Play();
            yield break;
        }
    }
    public void Play()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        Instantiate(checkPlayUI);
        anySignAnimator.SetTrigger("Trigger");
    }
}
