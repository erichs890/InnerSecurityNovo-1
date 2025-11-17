using UnityEngine; 
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneDia1 : MonoBehaviour
{
    public TextMeshProUGUI dia1Text;
    public float textDelay = 1f;
    public float textFadeDuration = 2f;
    public float sceneDelay = 2f;
    public string proximaCena = "Inicio"; // nome da cena que virá depois

    private void Start()
    {
        dia1Text.alpha = 0;
        StartCoroutine(PlayCutscene());
    }

    private System.Collections.IEnumerator PlayCutscene()
    {
        yield return new WaitForSeconds(textDelay);
        yield return StartCoroutine(FadeText(0, 1, textFadeDuration));
        yield return new WaitForSeconds(sceneDelay);

        // 4. Troca para a próxima cena
        SceneManager.LoadScene(proximaCena);
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