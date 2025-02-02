using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class SelectRoom : MonoBehaviour
{
    public Camera cam;
    List<FloorBehavior> floors = new List<FloorBehavior>();
    int layerMask;
    [SerializeField]
    private GameObject selectButton;
    [SerializeField]
    private GameObject backButton;
    private bool selectBtnPressed = false;
    private bool backBtnPressed = false;
    int selectionMode = 1;
    private GameObject[] areas;

    // Start is called before the first frame update
    void Start()
    {
        selectButton.GetComponent<Button>().onClick.AddListener(ToggleSelect);
        backButton.GetComponent<Button>().onClick.AddListener(ToggleBack);
        floors = FindObjectsOfType<FloorBehavior>().ToList();
        layerMask = 1 << 9;
        areas = GameObject.FindGameObjectsWithTag("area");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            floors = FindObjectsOfType<FloorBehavior>().ToList();
        }
        // Debug.Log("selectionMode: " + selectionMode);
        switch (selectionMode)
        {
            case 1:
                Vector3 cameraPoint = cam.transform.position;

                if (Physics.Raycast(cameraPoint, cam.transform.forward, out RaycastHit hit, Mathf.Infinity, layerMask))
                {
                    Debug.Log("hit detected");
                    GameObject obj = hit.collider.gameObject;
                    if (obj.CompareTag("floor"))
                    {
                        TurnOn(obj.GetComponent<FloorBehavior>());
                        if (selectBtnPressed)
                        {
                            foreach (GameObject a in areas)
                            {
                                Debug.Log("floor ID: " + a.GetComponent<AreaMetadata>().floor.GetInstanceID());
                                Debug.Log("obj ID: " + obj.GetInstanceID());
                                if (a.GetComponent<AreaMetadata>().floor.GetInstanceID() == obj.GetInstanceID())
                                {
                                    Debug.Log("match detected");
                                    a.transform.position = CurrentEventObject.origin;
                                    // a.transform.localScale = new Vector3(7, 7, 7);
                                }
                                else
                                {
                                    Debug.Log("setting inactive");
                                    a.SetActive(false);
                                }
                            }
                            selectionMode = 2;
                            selectBtnPressed = false;
                        }
                    }

                }
                else
                {
                    TurnOffAll();
                }
                break;

            case 2:
                if (backBtnPressed)
                {
                    Debug.Log("turning on all areas");
                    foreach (GameObject a in areas)
                    {
                        a.transform.position = a.GetComponent<AreaMetadata>().position;
                        // a.transform.localScale = new Vector3(1, 1, 1);
                        a.SetActive(true);
                    }
                    backBtnPressed = false;
                    selectionMode = 1;
                }
                break;
        }
    }


    void TurnOn(FloorBehavior floor)
    {
        floor.TurnOn();
    }

    void TurnOff(FloorBehavior floor)
    {
        floor.TurnOff();
    }

    void TurnOffAll()
    {
        foreach (FloorBehavior floor in floors)
        {
            TurnOff(floor);
        }
    }

    void ToggleSelect()
    {
        selectBtnPressed = true;
        Debug.Log(selectBtnPressed);
    }

    void ToggleBack()
    {
        backBtnPressed = true;
        Debug.Log(backBtnPressed);
    }



    // public static void SetGlobalScale(this Transform transform, Vector3 globalScale)
    // {
    //     transform.localScale = Vector3.one;
    //     transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
    // }
}
