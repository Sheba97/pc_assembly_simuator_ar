using UnityEngine;

public class InstallRamButton : MonoBehaviour
{
    public void OnClick()
    {
        PCAssemblyController controller = FindObjectOfType<PCAssemblyController>();

        if (controller != null)
            controller.InstallRam();
        else
            Debug.LogWarning("PCAssemblyController not found");
    }
}