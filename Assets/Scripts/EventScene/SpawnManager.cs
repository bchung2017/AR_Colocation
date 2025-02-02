using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    public string playerPrefabName;
    private bool isPlayerSpawned = false;
    public GameObject spawnButton;
    // Start is called before the first frame update
    void Start()
    {
        spawnButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnSpawnButtonPress()
    {
        Debug.Log("Spawn button pressed");
        PhotonNetwork.Instantiate(playerPrefabName, CurrentEventObject.origin, Quaternion.identity);
    }



    #region PHOTON Callback Methods
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            Debug.Log("Photon network connected and ready");
        }

    }
    #endregion
}
