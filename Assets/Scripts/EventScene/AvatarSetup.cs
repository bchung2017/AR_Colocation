using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AvatarSetup : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            transform.GetComponent<MovementController>().enabled = true;
        }
        else
        {
            transform.GetComponent<MovementController>().enabled = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
