using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase.Storage;
using System;
using System.Net;
public class DownloadFiles : MonoBehaviour
{

    Firebase.Storage.FirebaseStorage storage;
    Firebase.Storage.StorageReference storage_ref;
    Firebase.Storage.StorageReference penguin_ref;
    public PlaceObject1 placeObject1;
    // Start is called before the first frame update
    void Start()
    {
        storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        penguin_ref = storage.GetReferenceFromUrl("gs://pare-58bd5.appspot.com/Assets/AssetBundles/penguin1-Android");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonPress()
    {
        penguin_ref.GetDownloadUrlAsync().ContinueWith((Task<Uri> task) =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log("Download URL: " + task.Result);
                // ... now download the file via WWW or UnityWebRequest.
                WebClient client = new WebClient();
                client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadFileCompleted);
                client.DownloadFileAsync(task.Result, "Assets/penguin_bundle1");
                placeObject1.CreateObject("Assets/penguin_bundle1");
            }
        });
    }

    void DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
{
    if (e.Error == null)
    {
        Debug.Log("Download complete");
        
    }
}
}
