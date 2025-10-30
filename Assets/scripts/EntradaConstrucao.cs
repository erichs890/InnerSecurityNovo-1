using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para trocar de cena

public class EntradaConstrucao : MonoBehaviour
{
    public GameObject camerasConstrucaoGO; // GameObject que contém todas as câmeras da construção
    public Camera cameraPrincipal;         // Câmera do mundo externo
    public GameObject textoInteracao;      // Texto acima da porta
    public KeyCode teclaInteracao = KeyCode.E; // Tecla pra entrar/sair
    public GameObject jogador;             // Player para travar movimento
    public MonoBehaviour scriptMovimento;  // Script de movimento do jogador

    private bool jogadorPerto = false;
    private bool dentroConstrucao = false;
    private TrocarCamera trocarCameraScript;

    void Start()
    {
        if (textoInteracao != null)
            textoInteracao.SetActive(false);

        if (cameraPrincipal != null)
            cameraPrincipal.gameObject.SetActive(true);

        if (camerasConstrucaoGO != null)
        {
            camerasConstrucaoGO.SetActive(false); // Desativa todas as câmeras da construção
            trocarCameraScript = camerasConstrucaoGO.GetComponent<TrocarCamera>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;
            if (textoInteracao != null)
                textoInteracao.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            if (textoInteracao != null)
                textoInteracao.SetActive(false);
        }
    }

    void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(teclaInteracao))
        {
            EntrarConstrucao();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        }

        if (dentroConstrucao && Input.GetKeyDown(KeyCode.Q))
        {
            SairConstrucao();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        }
    }

    void EntrarConstrucao()
    {
        SceneManager.LoadScene("Cameras");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void SairConstrucao()
    {
        if (camerasConstrucaoGO != null)
        {
            camerasConstrucaoGO.SetActive(false);
        }

        if (cameraPrincipal != null)
            cameraPrincipal.gameObject.SetActive(true);

        // Destrava o movimento do jogador
        if (scriptMovimento != null)
            scriptMovimento.enabled = true;

        dentroConstrucao = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
