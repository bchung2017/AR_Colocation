using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameObject ARCamera;
    Rigidbody m_Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        ARCamera = GameObject.FindGameObjectWithTag("MainCamera");
        m_Rigidbody = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = ARCamera.transform.position;
    }
}
