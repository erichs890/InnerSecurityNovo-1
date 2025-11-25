using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepClips; // coloque 2 áudios aqui

    public float stepInterval = 0.45f;
    private float timer;

    private int currentIndex = 0; // alterna entre 0 e 1

    void Update()
    {
        bool isWalking =
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D);

        if (isWalking)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                PlayAlternatingFootstep();
                timer = stepInterval;
            }
        }
        else
        {
            timer = 0f;
        }
    }

    void PlayAlternatingFootstep()
    {
        if (footstepClips.Length < 2)
        {
            Debug.LogWarning("Coloque *exatamente 2* sons de passo em footstepClips!");
            return;
        }

        audioSource.PlayOneShot(footstepClips[currentIndex]);

        // alterna entre 0 e 1
        currentIndex = (currentIndex + 1) % 2;
    }
}
