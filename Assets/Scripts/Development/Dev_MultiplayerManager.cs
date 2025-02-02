using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Dev_MultiplayerManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.LocalPlayer.NickName = "test";
        PhotonNetwork.ConnectUsingSettings();
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    #region PHOTON Callback Methods

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;
        PhotonNetwork.JoinOrCreateRoom("404", roomOptions, TypedLobby.Default);
    }
     public override void OnConnected()
    {
        
    }
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined room: " + PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " created room: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " has entered");
    }

    #endregion

    #region PRIVATE Methods

    #endregion
}
