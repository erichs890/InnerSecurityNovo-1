using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CutsceneDerrotaInsana : MonoBehaviour
{
    public Image backgroundImage;        // Fundo que vai mudar de cor
    public TextMeshProUGUI derrotaText;  // Texto "A mente venceu..."

    [Header("Tempos e efeitos")]
    public float fadeDuration = 1.5f;       // Duração do fade inicial do fundo
    public float textDelay = 0.5f;          // Espera antes do texto aparecer
    public float textFadeDuration = 2f;     // Fade-in do texto
    public float sceneDelay = 4f;           // Duração total da cutscene
    public float shakeMagnitude = 5f;       // Intensidade do tremor do texto
    public float shakeSpeed = 20f;          // Velocidade do tremor
    public float glitchFrequency = 0.1f;    // Frequência do glitch de cores
    public float colorChangeSpeed = 2f;     // Velocidade da mudança de cor do fundo
    public AudioSource audioSource;

    private Vector3 originalTextPos;

    private void Start()
    {
        derrotaText.alpha = 0;
        backgroundImage.color = Color.black;
        originalTextPos = derrotaText.rectTransform.localPosition;
        StartCoroutine(PlayCutscene());

        if(audioSource != null)
        audioSource.Play(); 

        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        // 1. Fade-in inicial do fundo
        yield return StartCoroutine(FadeImageAlpha(0, 1, fadeDuration));

        // 2. Espera e fade-in do texto
        yield return new WaitForSeconds(textDelay);
        yield return StartCoroutine(FadeText(0, 1, textFadeDuration));

        // 3. Tremor + glitch de cores + fundo mudando
        float elapsed = 0f;
        float glitchTimer = 0f;
        bool colorToggle = false;

        while (elapsed < sceneDelay)
        {
            // Tremor do texto
            Vector3 offset = new Vector3(
                Mathf.PerlinNoise(Time.time * shakeSpeed, 0f) - 0.5f,
                Mathf.PerlinNoise(0f, Time.time * shakeSpeed) - 0.5f,
                0f
            ) * shakeMagnitude;
            derrotaText.rectTransform.localPosition = originalTextPos + offset;

            // Glitch de cores do texto
            glitchTimer += Time.deltaTime;
            if (glitchTimer >= glitchFrequency)
            {
                colorToggle = !colorToggle;
                derrotaText.color = colorToggle ? Color.white : Color.red;
                glitchTimer = 0f;
            }

            // Mudança contínua do fundo
            float t = Mathf.PingPong(Time.time * colorChangeSpeed, 1f);
            backgroundImage.color = Color.Lerp(Color.black, new Color(0.3f, 0, 0.3f), t); // Preto <-> Roxo escuro

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Resetar posição e cores
        derrotaText.rectTransform.localPosition = originalTextPos;
        derrotaText.color = Color.white;
        backgroundImage.color = Color.black;

        // 4. Ir para menu ou reinício
        // SceneManager.LoadScene("CenaMenu");
    }

    private IEnumerator FadeImageAlpha(float start, float end, float duration)
    {
        float elapsed = 0f;
        Color c = backgroundImage.color;
        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(start, end, elapsed / duration);
            backgroundImage.color = new Color(c.r, c.g, c.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        backgroundImage.color = new Color(c.r, c.g, c.b, end);
    }

    private IEnumerator FadeText(float start, float end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(start, end, elapsed / duration);
            derrotaText.alpha = alpha;
            elapsed += Time.deltaTime;
            yield return null;
        }
        derrotaText.alpha = end;
    }
}
