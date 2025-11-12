using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("Configurações")]
    public CanvasGroup hudGroup;     // componente CanvasGroup do HUD
    public float visibleTime = 5f;   // tempo que o HUD fica visível
    public float fadeSpeed = 1f;     // velocidade do fade

    private float timer;
    private bool fadingOut = false;

    void Start()
    {
        if (hudGroup == null)
            hudGroup = GetComponent<CanvasGroup>();

        hudGroup.alpha = 1f;
        timer = visibleTime;
    }

    void Update()
    {
        if (Time.timeScale == 0f) return; // pausa = HUD parado

        if (!fadingOut)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
                fadingOut = true;
        }
        else
        {
            hudGroup.alpha = Mathf.MoveTowards(hudGroup.alpha, 0f, fadeSpeed * Time.deltaTime);
            if (hudGroup.alpha <= 0f)
                gameObject.SetActive(false); // desativa depois do fade
        }
    }
}
