using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Linq;
using UnityEngine.AI;
using UnityEditor;

[RequireComponent(typeof(ARTrackedImageManager))]
[RequireComponent(typeof(ARRaycastManager))]
/*public class DragAndAutoHideController : MonoBehaviour
{
    // Marker-Bibliothek
    [Serializable]
    public struct ImagePrefabPair
    {
        public string imageName;    // Name des Bildes in der Reference‑Image‑Library
        public GameObject prefab;   // Prefab, das angezeigt werden soll
    }
    private ARTrackedImageManager _trackedImageManager;
    private ARRaycastManager _raycastManager;

    [Tooltip("Bindet die zu erkennenden Bildnamen an die jeweiligen Prefabs")]
    public List<ImagePrefabPair> imagePrefabs;

    // Relevante Variablen für das automatische Entfernen der Objekte per Timer    
    private Dictionary<string, GameObject> _spawned = new Dictionary<string, GameObject>();// Speichert die bereits instanziierten GameObjects
    private Dictionary<string, Coroutine> _hideTimers = new Dictionary<string, Coroutine>();// Referenz auf die Hide‑Timer‑Coroutinen

   void Awake()
    {
        _trackedImageManager = GetComponent<ARTrackedImageManager>();
        _raycastManager = GetComponent<ARRaycastManager>();

        // Prefabs vorab instanziieren und verstecken
        foreach (var pair in imagePrefabs)
        {
            var go = Instantiate(pair.prefab);
            go.name = pair.imageName;
            go.SetActive(false);

            // Collider hinzufügen, falls noch keiner vorhanden ist
            if (go.GetComponent<Collider>() == null)
            {
                go.AddComponent<BoxCollider>();
            }

            _spawned[pair.imageName] = go;
        }
    }

    private void Start()
    {
        
    }

    void Update()
    {

    }

    void OnEnable()
    {
        _trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        _trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var added in args.added)
            ShowOrUpdate(added);
        foreach (var updated in args.updated)
            ShowOrUpdate(updated);
    }

    private void ShowOrUpdate(ARTrackedImage tracked)
    {
        string name = tracked.referenceImage.name;
        if (name == null)
            return;
        if (!_spawned.TryGetValue(name, out var go)) return;

        if (tracked.trackingState == TrackingState.Tracking)
        {
            if (!go.activeSelf)
            {
                go.SetActive(true);
            }
            go.transform.position = tracked.transform.position;
            go.transform.rotation = tracked.transform.rotation;
            RestartHideTimer(name, go);
        }
    }

    private void RestartHideTimer(string name, GameObject go)
    {
        // Vorhandenen Timer stoppen, falls er existiert
        if (_hideTimers.TryGetValue(name, out var c) && c != null)
            StopCoroutine(c);
        // Neuen Timer starten
        _hideTimers[name] = StartCoroutine(HideAfterDelay(go));
    }

    private IEnumerator HideAfterDelay(GameObject go)
    {
        yield return new WaitForSeconds(99999f);
        go.SetActive(false);
    }
    
}*/

public class DragAndAutoHideController : MonoBehaviour
{
    [Serializable]
    public struct ImagePrefabPair
    {
        public string imageName;     // Must EXACTLY match image name in Image Library
        public GameObject prefab;    // Your PC_Assembly_Root prefab
    }

    private ARTrackedImageManager trackedImageManager;

    public List<ImagePrefabPair> imagePrefabs;

    private HashSet<string> locked = new HashSet<string>();


    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var tracked in args.added)
            UpdatePrefab(tracked);

        foreach (var tracked in args.updated)
            UpdatePrefab(tracked);

       // foreach (var tracked in args.removed)
            //HidePrefab(tracked);
    }

    private void UpdatePrefab(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (!spawnedPrefabs.TryGetValue(imageName, out GameObject prefabInstance))
        {
            // create once
            ImagePrefabPair pair = imagePrefabs.Find(p => p.imageName == imageName);
            if (pair.prefab == null) return;

            prefabInstance = Instantiate(pair.prefab, trackedImage.transform);
            prefabInstance.transform.localPosition = Vector3.zero;
            prefabInstance.transform.localRotation = Quaternion.identity;

            var controller = prefabInstance.GetComponent<PCAssemblyController>();
            var relay = prefabInstance.GetComponentInChildren<AssemblyButtonRelay>();

            if (controller != null && relay != null)
            {
                relay.SetController(controller);
            }
            
            spawnedPrefabs[imageName] = prefabInstance;
        }

        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            prefabInstance.SetActive(true);
            /*prefabInstance.transform.position = trackedImage.transform.position;
            prefabInstance.transform.rotation = trackedImage.transform.rotation;

            locked.Add(imageName);*/
            prefabInstance.transform.SetParent(trackedImage.transform);
            prefabInstance.transform.localPosition = Vector3.zero;
            prefabInstance.transform.localRotation = Quaternion.identity;
        }
        else
        {
            prefabInstance.SetActive(false);
        }
    }

    /*private void HidePrefab(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (spawnedPrefabs.TryGetValue(imageName, out GameObject prefabInstance))
        {
            prefabInstance.SetActive(false);
        }
    }*/
}

