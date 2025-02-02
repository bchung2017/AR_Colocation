using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Firebase;
using Firebase.Database;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using RSG;


public class EventCodeHandler : MonoBehaviourPunCallbacks
{
    [Header("Login UI")]
    public InputField code_Input;
    public InputField name_Input;
    public Text connectionStatusText;
    public bool isDevMode;
    private bool fireBase = false;



    void Start()
    {
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://pare-58bd5.firebaseio.com/");
        var se = new InputField.SubmitEvent();

    }

    public void onCodeSubmit()
    {
        ConnectToPhoton()
            .Then(() => ConnectToFirebase()
                .Then(() => fireBase = true))
                .Catch(Debug.LogException)
            .Catch(Debug.LogException);
    }



    void Update()
    {
        connectionStatusText.text = "Connection Status: " + PhotonNetwork.NetworkClientState;
        if (PhotonNetwork.IsConnectedAndReady && fireBase)
        {
            if (isDevMode)
            {
                SceneManager.LoadScene("DevEventScene");
            }
            else
            {
                SceneManager.LoadScene("EventScene");
            }
        }
    }

    private IPromise ConnectToFirebase()
    {
        var promise = new Promise();
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        FirebaseDatabase.DefaultInstance.GetReference("events/" + code_Input.text).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("error");
                promise.Reject(task.Exception);

            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                string ss = snapshot.GetRawJsonValue().ToString();
                Debug.Log("Raw Json: " + ss);
                // dynamic data = JsonConvert.DeserializeObject<dynamic>(ss);
                dynamic data = JsonConvert.DeserializeObject(ss);
                // Debug.Log(data);
                // CurrentEventObject.numOfOrgs = data["numOfOrgs"];

                Debug.Log(data["numOfOrgs"].Value.GetType());
                CurrentEventObject.eventId = code_Input.text;
                CurrentEventObject.numOfOrgs = data["numOfOrgs"].Value;
                CurrentEventObject.uid = data["uid"].Value;
                Debug.Log(data["orgs"].Count);
                foreach (JProperty org in (JToken)data["orgs"])
                {
                    CurrentEventObject.AddOrg(org.Value.ToString());
                }
                CurrentEventObject.Display();
                promise.Resolve();
            }
        });

        return promise;
    }

    private IPromise ConnectToPhoton()
    {
        var promise = new Promise();
        string userName = name_Input.text;

        if (!string.IsNullOrEmpty(userName) && !PhotonNetwork.IsConnected)
        {

            PhotonNetwork.LocalPlayer.NickName = userName;
            PhotonNetwork.ConnectUsingSettings();
            promise.Resolve();
        }
        else
        {
            promise.Reject(new Exception("Player name is invalid"));
        }

        return promise;
    }

    #region PHOTON Callback Methods
    public override void OnConnected()
    {
        Debug.Log("Connected to internet");
    }
    #endregion
}

