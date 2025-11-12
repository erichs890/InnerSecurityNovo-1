using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using System.Collections;

public class CutsceneVitoriaSuprema : MonoBehaviour
{
    public Image fadeImage;
    public TextMeshProUGUI vitoriaText;
    public RawImage videoSurface;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;

    [Header("Tempos")]
    public float preVideoFadeOut = 1.5f;
    public float postVideoFadeIn = 1.2f;
    public float textDelay = 0.4f;
    public float textFadeDuration = 1.6f;
    public float holdVictoryScreen = 4f;
    public float videoIntroMask = 0.5f;

    [Header("Efeitos")]
    public float pulseSpeed = 2f;
    public float pulseScale = 1.06f;
    public float bgColorSpeed = 0.8f;
    public Color bgA = new Color(0f, 0.15f, 0.35f, 1f);
    public Color bgB = new Color(0.1f, 0.5f, 0.3f, 1f);

    RectTransform txtRT;
    Vector3 baseScale;
    Color fadeBase;

    void Start()
    {
        if (fadeImage) fadeImage.color = new Color(0,0,0,1);
        if (vitoriaText) vitoriaText.alpha = 0;
        if (videoSurface) videoSurface.color = new Color(1,1,1,0);
        if (audioSource) audioSource.Play();
        txtRT = vitoriaText ? vitoriaText.rectTransform : null;
        baseScale = txtRT ? txtRT.localScale : Vector3.one;
        fadeBase = fadeImage ? fadeImage.color : Color.black;
        if (videoPlayer)
        {
            videoPlayer.playOnAwake = false;
            videoPlayer.isLooping = false;
            videoPlayer.waitForFirstFrame = true;
            videoPlayer.skipOnDrop = true;
        }
        if (fadeImage) fadeImage.transform.SetAsLastSibling();
        StartCoroutine(Flow());
    }

    IEnumerator Flow()
    {
        if (videoPlayer)
        {
            videoPlayer.Prepare();
            while (!videoPlayer.isPrepared) yield return null;
        }

        if (videoSurface) videoSurface.color = new Color(1,1,1,1);

        if (videoPlayer)
        {
            videoPlayer.Play();
            yield return new WaitForSeconds(videoIntroMask);
            yield return StartCoroutine(FadeImageAlpha(1f, 0f, preVideoFadeOut));
            while (videoPlayer.isPlaying) yield return null;
        }
        else
        {
            yield return StartCoroutine(FadeImageAlpha(1f, 0f, preVideoFadeOut));
        }

        yield return StartCoroutine(FadeImageAlpha(0f, 1f, postVideoFadeIn));

        if (videoSurface) videoSurface.color = new Color(1,1,1,0);

        if (fadeImage) fadeImage.transform.SetAsFirstSibling();

        float t = 0f;
        float timer = 0f;
        if (vitoriaText) StartCoroutine(FadeText(0f, 1f, textFadeDuration, textDelay));
        while (timer < holdVictoryScreen)
        {
            if (fadeImage)
            {
                t += Time.deltaTime * bgColorSpeed;
                fadeImage.color = Color.Lerp(bgA, bgB, 0.5f * (Mathf.Sin(t) + 1f));
            }
            if (txtRT)
            {
                float s = Mathf.Lerp(1f, pulseScale, 0.5f * (Mathf.Sin(Time.time * pulseSpeed) + 1f));
                txtRT.localScale = baseScale * s;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        if (fadeImage) fadeImage.color = fadeBase;
        if (txtRT) txtRT.localScale = baseScale;
    }

    IEnumerator FadeImageAlpha(float start, float end, float duration)
    {
        if (!fadeImage) yield break;
        float e = 0f;
        Color c = fadeImage.color;
        while (e < duration)
        {
            float a = Mathf.Lerp(start, end, e / duration);
            fadeImage.color = new Color(c.r, c.g, c.b, a);
            e += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = new Color(c.r, c.g, c.b, end);
    }

    IEnumerator FadeRawImageAlpha(float start, float end, float duration)
    {
        if (!videoSurface) yield break;
        float e = 0f;
        Color c = videoSurface.color;
        while (e < duration)
        {
            float a = Mathf.Lerp(start, end, e / duration);
            videoSurface.color = new Color(c.r, c.g, c.b, a);
            e += Time.deltaTime;
            yield return null;
        }
        videoSurface.color = new Color(c.r, c.g, c.b, end);
    }

    IEnumerator FadeText(float start, float end, float duration, float delay)
    {
        if (!vitoriaText) yield break;
        yield return new WaitForSeconds(delay);
        float e = 0f;
        while (e < duration)
        {
            float a = Mathf.Lerp(start, end, e / duration);
            vitoriaText.alpha = a;
            e += Time.deltaTime;
            yield return null;
        }
        vitoriaText.alpha = end;
    }
}
