using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBehavior : MonoBehaviour
{
    Renderer rend;
    bool lightState;
    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOn() {
        rend.material.EnableKeyword("_EMISSION");
    }

    public void TurnOff() {
        rend.material.DisableKeyword("_EMISSION");
    }
}
