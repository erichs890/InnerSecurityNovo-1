using UnityEngine;

public class DisappearanceAnomaly : MonoBehaviour
{
    public GameObject[] objectsToHide;

    void OnEnable()
    {
        SetObjectsVisible(false);
    }

    void OnDisable()
    {
        SetObjectsVisible(true);
    }

    void SetObjectsVisible(bool visible)
    {
        if (objectsToHide == null) return;

        foreach (GameObject go in objectsToHide)
        {
            if (go) go.SetActive(visible);
        }
    }
}