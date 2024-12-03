using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Setting : MonoBehaviour
{
    public AudioMixer audioMixer;

    int musicFigure;
    int effectFigure;

    int MusicFigure { get { return musicFigure; }set { musicFigure = Mathf.Clamp(value,0,10); } }
    int EffectFigure { get { return effectFigure; } set { effectFigure = Mathf.Clamp(value, 0, 10); } }

    public Text musicText;
    public Text effectText;

    public Image[] musicBar;
    public Image[] effectBar;

    [SerializeField] AudioClip clickAudio;

    private void Start()
    {
        FirstSet();
    }

    public void FirstSet()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            MusicFigure = PlayerPrefs.GetInt("Music", MusicFigure);
            EffectFigure = PlayerPrefs.GetInt("Effect", EffectFigure);
        }
        else
        {
            MusicFigure = 4;
            EffectFigure = 4;
        }

        SetEffect();
        SetMusic();
    }

    private void OnEnable()
    {
        GameManager.UI.AddPopupUI(gameObject);
    }

    private void OnDisable()
    {
        GameManager.UI.RemovePopupUI(gameObject);
        PlayerPrefs.SetInt("Music", MusicFigure);
        PlayerPrefs.SetInt("Effect", EffectFigure);
    }

    public void MusicUp()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        MusicFigure++;
        SetMusic();
    }

    public void MusicDown()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        MusicFigure--;
        SetMusic();
    }

    public void EffectUp()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        EffectFigure++;
        SetEffect();
    }

    public void EffectDown()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        EffectFigure--;
        SetEffect();
    }

    void SetMusic()
    {
        for (int i = 0; i < 10; i++)
        {
            musicBar[i].color = i <= MusicFigure - 1 ? new Color(0.45f, 1, 1) : Color.gray;
        }
        if(MusicFigure != 0)
            audioMixer.SetFloat("music", Mathf.Log10(MusicFigure / 10f) * 20);
        else
            audioMixer.SetFloat("music", -80);
        musicText.text = $"{MusicFigure}";
    }

    void SetEffect()
    {
        for (int i = 0; i < 10; i++)
        {
            effectBar[i].color = i <= EffectFigure - 1 ? new Color(0.45f, 1, 1) : Color.gray;
        }
        if (EffectFigure != 0)
            audioMixer.SetFloat("effect", Mathf.Log10(EffectFigure / 10f) * 20);
        else
            audioMixer.SetFloat("effect",-80);
        effectText.text = $"{EffectFigure}";
    }


    public void Close()
    {
        GameManager.Instance.effectAudioSource.PlayOneShot(clickAudio);
        Destroy(gameObject);
    }
}
