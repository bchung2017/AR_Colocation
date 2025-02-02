using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.IO;

public class PlaceObject1 : MonoBehaviour
{
    public ARSessionOrigin origin;
    public ARRaycastManager raycastManager;
    public GameObject placementIndicator;       //gameobject that visualizes where to set object
    public GameObject placementVisualizer;
    public GameObject objectToPlace;            //object to place in placementIndicator's location
    public GameObject button;
    public Camera cam;
    bool placementPoseIsValid = false;

    Pose placementPose;                         //pose of placementIndicator
    bool buttonPressed = false;
    //public GraphicRaycaster raycaster;

    void Awake()
    {
        button.GetComponent<Button>().onClick.AddListener(Activate);
        // obj2 = Instantiate(objectToPlace, Vector3.zero, Quaternion.identity);
    }

    void Start()
    {
        // obj2 = Instantiate(objectToPlace, Vector3.zero, Quaternion.identity);
    }
    void Update()
    {
        if (!buttonPressed)
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();
        }
    }

    //Get raycast hits and set placementPose to the first hit pose
    private void UpdatePlacementPose()
    {
        var screenCenter = cam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }

    //set location of placementIndicator to placementPose
    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementVisualizer.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            placementVisualizer.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            button.SetActive(true);
        }
        else
        {
            placementVisualizer.SetActive(false);
            button.SetActive(false);
        }
    }

    private Vector3 adjPos(Vector3 position)
    {
        return position + new Vector3(0.0f, -10.00f, 0.0f);
    }

    public void CreateObject(string assetBundleName)
    {
        var loadedAssetBundle = AssetBundle.LoadFromFile(assetBundleName);
        if (loadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        Debug.Log("assetbundlename: " + assetBundleName);
        objectToPlace = loadedAssetBundle.LoadAsset<GameObject>("MyObject");
    }

    public void Activate()
    {
        if (placementPoseIsValid)
        {

            // AssetBundle loadedAssetBundle = AssetBundle.LoadFromFile(@"C:\cygwin64\home\bchun\AR\Unity\personal projects\Databases final project\Assets\penguin_bundle1");
            // if (loadedAssetBundle == null)
            // {
            //     Debug.Log("Failed to load AssetBundle!");
            //     return;
            // }
            // Debug.Log("assetbundlename: " + "Assets/penguin_bundle1");
            // var prefab = loadedAssetBundle.LoadAsset("Tux");

            //Place object
            objectToPlace.SetActive(true);
            objectToPlace.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            CurrentEventObject.origin = placementPose.position;
            GameObject[] areas = GameObject.FindGameObjectsWithTag("area");
            foreach (GameObject a in areas)
            {
                a.GetComponent<AreaMetadata>().SetOgPosition(a.transform.position);
            }
            Debug.Log(objectToPlace.transform.position);
            Debug.Log(placementPose.position);
            buttonPressed = true;
            placementPoseIsValid = false;
            placementVisualizer.SetActive(false);
            placementIndicator.SetActive(false);
            button.SetActive(false);
            origin.GetComponent<ARPlaneManager>().enabled = false;
            origin.GetComponent<ARPointCloudManager>().enabled = false;
            foreach (var Point in origin.GetComponent<ARPointCloudManager>().trackables)
            {
                Point.gameObject.SetActive(false);
                // Destroy(Point.gameObject);   
            }
            foreach (var Plane in origin.GetComponent<ARPlaneManager>().trackables)
            {
                Plane.gameObject.SetActive(false);
                // Destroy(Plane.gameObject);
            }
        }
    }

    public void Deactivate()
    {
        objectToPlace.SetActive(false);
        buttonPressed = false;
        placementIndicator.SetActive(true);
        button.SetActive(true);
        origin.GetComponent<ARPlaneManager>().enabled = true;
        origin.GetComponent<ARPointCloudManager>().enabled = true;
        foreach (var Point in origin.GetComponent<ARPointCloudManager>().trackables)
        {
            Point.gameObject.SetActive(true);
            // Destroy(Point.gameObject);   
        }
        foreach (var Plane in origin.GetComponent<ARPlaneManager>().trackables)
        {
            Plane.gameObject.SetActive(true);
            // Destroy(Plane.gameObject);
        }
    }

}
