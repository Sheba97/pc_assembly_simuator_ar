using System.Collections;
using UnityEngine;

public class BillBoardToCamera : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (!cam) return;

        transform.LookAt(transform.position + cam.transform.forward);
    }
}
