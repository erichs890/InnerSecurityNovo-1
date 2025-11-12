using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneDia1 : MonoBehaviour
{
    public TextMeshProUGUI dia1Text;      // Texto "Dia 1"
    public float textDelay = 1f;           // Tempo antes de começar o fade do texto
    public float textFadeDuration = 2f;    // Duração do fade-in do texto
    public float sceneDelay = 2f;          // Tempo para manter o texto visível antes de ir pro jogo

    private void Start()
    {
        dia1Text.alpha = 0;                // Começa invisível
        StartCoroutine(PlayCutscene());
    }

    private System.Collections.IEnumerator PlayCutscene()
    {
        // 1. Espera um pouco antes de mostrar o texto
        yield return new WaitForSeconds(textDelay);

        // 2. Fade-in do texto "Dia 1"
        yield return StartCoroutine(FadeText(0, 1, textFadeDuration));

        // 3. Mantém o texto visível por um tempo
        yield return new WaitForSeconds(sceneDelay);

        // 4. Troca para a próxima cena (descomente e coloque o nome correto)
        // SceneManager.LoadScene("CenaDoJogo");
    }

    private System.Collections.IEnumerator FadeText(float start, float end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(start, end, elapsed / duration);
            dia1Text.alpha = alpha;
            elapsed += Time.deltaTime;
            yield return null;
        }
        dia1Text.alpha = end;
    }
}
