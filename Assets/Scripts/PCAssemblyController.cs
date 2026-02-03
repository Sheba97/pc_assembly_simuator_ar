using System.Collections;
using UnityEngine;

public class PCAssemblyController : MonoBehaviour
{
    public GameObject motherboard;
    public GameObject ramPreview;
    public GameObject coolerPreview;
    public GameObject gpuPreview;
    public GameObject openCasingPreview;

    public GameObject motherboardRamCpu;
    public GameObject motherboardRamCpuCooler;
    public GameObject systemWithoutGpu;
    public GameObject systemWithGpu;
    public GameObject closedCasing;

    // Buttons
    public GameObject btnInstallRam;
    public GameObject btnInstallCooler;
    public GameObject btnPutInCase;
    public GameObject btnInstallGpu;
    public GameObject btnCloseCase;

    public float installationDelay = 1.5f;

    void Start()
    {
        // Parts
        motherboard.SetActive(true);
        ramPreview.SetActive(false);
        coolerPreview.SetActive(false);
        gpuPreview.SetActive(false);
        openCasingPreview.SetActive(false);

        motherboardRamCpu.SetActive(false);
        motherboardRamCpuCooler.SetActive(false);
        systemWithoutGpu.SetActive(false);
        systemWithGpu.SetActive(false);
        closedCasing.SetActive(false);

        // Buttons
        btnInstallRam.SetActive(true);
        btnInstallCooler.SetActive(false);
        btnPutInCase.SetActive(false);
        btnInstallGpu.SetActive(false);
        btnCloseCase.SetActive(false);
    }

    // -------- RAM --------
    public void InstallRam()
    {
        StartCoroutine(RamRoutine());
    }

    IEnumerator RamRoutine()
    {
        ramPreview.SetActive(true);
        yield return new WaitForSeconds(installationDelay);

        motherboard.SetActive(false);
        ramPreview.SetActive(false);
        motherboardRamCpu.SetActive(true);

        btnInstallRam.SetActive(false);
        btnInstallCooler.SetActive(true);
    }

    // -------- COOLER --------
    public void InstallCooler()
    {
        StartCoroutine(CoolerRoutine());
    }

    IEnumerator CoolerRoutine()
    {
        coolerPreview.SetActive(true);
        yield return new WaitForSeconds(installationDelay);

        motherboardRamCpu.SetActive(false);
        coolerPreview.SetActive(false);
        motherboardRamCpuCooler.SetActive(true);

        btnInstallCooler.SetActive(false);
        btnPutInCase.SetActive(true);
    }

    // -------- PUT INTO CASE --------
    public void InstallIntoCase()
    {
        StartCoroutine(CaseRoutine());
    }

    IEnumerator CaseRoutine()
    {
        openCasingPreview.SetActive(true);
        yield return new WaitForSeconds(installationDelay);

        motherboardRamCpuCooler.SetActive(false);
        openCasingPreview.SetActive(false);
        systemWithoutGpu.SetActive(true);

        btnPutInCase.SetActive(false);
        btnInstallGpu.SetActive(true);
    }

    // -------- GPU --------
    public void InstallGPU()
    {
        StartCoroutine(GpuRoutine());
    }

    IEnumerator GpuRoutine()
    {
        gpuPreview.SetActive(true);
        yield return new WaitForSeconds(installationDelay);

        systemWithoutGpu.SetActive(false);
        gpuPreview.SetActive(false);
        systemWithGpu.SetActive(true);

        btnInstallGpu.SetActive(false);
        btnCloseCase.SetActive(true);
    }

    // -------- CLOSE CASE --------
    public void CloseCase()
    {
        systemWithGpu.SetActive(false);
        closedCasing.SetActive(true);

        btnCloseCase.SetActive(false);
    }
}
