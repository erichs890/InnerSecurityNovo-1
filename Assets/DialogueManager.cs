using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // pra mudar de cena
using System.Collections; // pra coroutine

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    [Header("Cena seguinte")]
    public string proximaCena = "Cutscene_Dia1"; // nome da próxima cena (depois do diálogo)

    [Header("Fade")]
    public Image fadeImage; // full screen preta
    public float fadeSpeed = 1.5f; // velocidade do fade

    private string[] dialogueLines = new string[]
    {
        "Por favor, sente-se.",
        "Antes de começarmos, preciso que você se lembre de algo muito importante.",
        "Você tem passado por distorções… visões estranhas.",
        "Eu sei que parecem reais, mas não são.",
        "Se você enxergar algo incomum… uma anomalia…",
        "Precisa manter a calma!, Não entre em pânico.",
        "Não tente lutar contra isso.",
        "Apenas repita para si mesmo: é a sua mente pregando peças.",
        "E, acima de tudo — tome a sua medicação.",
        "Sem ela, as anomalias vão se intensificar… vão ser mais difíceis de ignorar.",
        "Prometa que vai se lembrar disso. A sua segurança depende disso."
    };

    private int currentLine = 0;

    void Start()
    {
        ShowLine();
        nextButton.onClick.AddListener(NextLine);

        // garante que o fade comece transparente
        if (fadeImage != null)
            fadeImage.color = new Color(0, 0, 0, 0);
    }

    void ShowLine()
    {
        dialogueText.text = dialogueLines[currentLine];
    }

    void NextLine()
{
    if (currentLine < dialogueLines.Length - 1)
    {
        currentLine++;
        ShowLine();
    }
    else
    {
        // Última fala → inicia fade da cena atual
        if (fadeImage != null)
            StartCoroutine(FadeOutAndChangeScene());
        else
            SceneManager.LoadScene(proximaCena, LoadSceneMode.Single);
    }
}

IEnumerator FadeOutAndChangeScene()
{
    float alpha = 0f;

    // Desabilita o botão pra não clicar de novo
    nextButton.interactable = false;

    while (alpha < 1f)
    {
        alpha += Time.deltaTime * fadeSpeed;
        fadeImage.color = new Color(0, 0, 0, alpha);
        yield return null;
    }

    // Agora sim troca de cena
    SceneManager.LoadScene(proximaCena, LoadSceneMode.Single);
}
}
