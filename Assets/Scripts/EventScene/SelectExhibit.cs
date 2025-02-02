using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class SelectExhibit : MonoBehaviour
{
    public Camera cam;
    List<FloorBehavior> floors = new List<FloorBehavior>();

    [SerializeField]
    private GameObject selectButton;
    private bool selectBtnPressed = false;
    private bool backBtnPressed = false;
    int selectionMode = 1;
    private GameObject[] exhibits;

    [SerializeField]
    private GameObject text;
    [SerializeField]
    private GameObject panel;
    private GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        selectButton.GetComponent<Button>().onClick.AddListener(ToggleSelect);
        exhibits = GameObject.FindGameObjectsWithTag("exhibit");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPoint = cam.transform.position;

        if (Physics.Raycast(cameraPoint, cam.transform.forward, out RaycastHit hit, Mathf.Infinity))
        {
            Debug.Log("exhibit hit detected");
            obj = hit.collider.gameObject;
            if (obj.CompareTag("exhibit"))
            {
                selectButton.SetActive(true);
            }
        }
        else
        {
            selectButton.SetActive(false);
        }

        if (selectBtnPressed)
        {
            panel.SetActive(true);
            text.SetActive(true);
            if(obj != null) {
                Debug.Log(obj.GetComponent<ExhibitMetadata>().GetDesc());
            }
            
            text.GetComponent<Text>().text = obj.GetComponent<ExhibitMetadata>().GetDesc();
        }
        else
        {
            panel.SetActive(false);
            text.SetActive(false);
            // text.GetComponent<Text>().text = obj.GetComponent<ExhibitMetadata>().GetDesc();
        }

    }

    void ToggleSelect()
    {
        selectBtnPressed = !selectBtnPressed;
        Debug.Log(selectBtnPressed);
    }

}
