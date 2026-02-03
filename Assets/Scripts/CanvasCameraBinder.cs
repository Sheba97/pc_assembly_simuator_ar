using UnityEngine;

public class CanvasCameraBinder : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(AssignCamera());
    }

    System.Collections.IEnumerator AssignCamera()
    {
        yield return null; // wait 1 frame

        var canvas = GetComponent<Canvas>();
        if (canvas != null && canvas.renderMode == RenderMode.WorldSpace)
        {
            if (Camera.main != null)
                canvas.worldCamera = Camera.main;
        }
    }
}
