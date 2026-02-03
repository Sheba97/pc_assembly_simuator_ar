using System.Collections;
using UnityEngine;

public class AssemblyButtonRelay : MonoBehaviour
{
    private PCAssemblyController controller;

    public void SetController(PCAssemblyController c)
    {
        controller = c;
    }

    public void InstallRam() { controller?.InstallRam(); }
    public void InstallCooler() { controller?.InstallCooler(); }
    public void InstallIntoCase() { controller?.InstallIntoCase(); }
    public void InstallGPU() { controller?.InstallGPU(); }
    public void CloseCase() { controller?.CloseCase(); }
}
