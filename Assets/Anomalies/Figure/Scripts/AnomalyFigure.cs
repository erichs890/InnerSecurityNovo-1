using UnityEngine;

public class AnomalyFigure : MonoBehaviour
{
    public Animator animator;
    public Camera playerCamera;
    public string idleAnimation = "Zombie Idle";
    public string[] reactionAnimations = { "Ch10_Zombie Turn" };

    public float minDelay = 1f;
    public float maxDelay = 3f;
    public float reactionDuration = 2.5f;
    public float rotationSpeed = 2f;

    private bool hasReacted = false;
    private bool isVisible = false;
    private bool isFacingPlayer = false;

    void Start()
    {
        if (!animator) animator = GetComponent<Animator>();
        if (!playerCamera) playerCamera = Camera.main;
        animator.CrossFade(idleAnimation, 0.2f);
    }

    void Update()
    {
        CheckVisibility();

        if (isVisible && !hasReacted)
        {
            hasReacted = true;
            Invoke(nameof(React), Random.Range(minDelay, maxDelay));
        }

        // Se já estiver encarando o player, continua olhando pra ele
        if (isFacingPlayer)
        {
            LookAtPlayer();
        }
    }

    void CheckVisibility()
    {
        Renderer rend = GetComponentInChildren<Renderer>();
        if (!rend) return;

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCamera);
        bool visibleNow = GeometryUtility.TestPlanesAABB(planes, rend.bounds);

        if (!visibleNow && isVisible)
        {
            hasReacted = false;
            isFacingPlayer = false;
        }

        isVisible = visibleNow;
    }

    void React()
    {
        if (reactionAnimations.Length == 0) return;

        string anim = reactionAnimations[Random.Range(0, reactionAnimations.Length)];
        animator.CrossFade(anim, 0.2f);

        // Depois de reagir (virar), começa a olhar pro player
        Invoke(nameof(StartLookingAtPlayer), reactionDuration);
    }

    void StartLookingAtPlayer()
    {
        animator.CrossFade(idleAnimation, 0.2f);
        isFacingPlayer = true;
        hasReacted = false;
    }

    void LookAtPlayer()
    {
        Vector3 lookPos = playerCamera.transform.position;
        lookPos.y = transform.position.y; // mantém o olhar na horizontal
        Quaternion targetRotation = Quaternion.LookRotation(lookPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
