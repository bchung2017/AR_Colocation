using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase.Storage;
using System.IO;

public class UploadFiles : MonoBehaviour
{
    Firebase.Storage.FirebaseStorage storage;
    Firebase.Storage.StorageReference storage_ref;
    Firebase.Storage.StorageReference penguin_ref;

    // Start is called before the first frame update
    void Start()
    {
        storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        storage_ref = storage.GetReferenceFromUrl("gs://pare-58bd5.appspot.com");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonPress()
    {

        foreach (string file_name in System.IO.Directory.GetFiles("Assets/AssetBundles")) {
            var file_ref = storage_ref.Child(file_name);
            file_ref.PutFileAsync(file_name).ContinueWith((Task<StorageMetadata> task) =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.Log(task.Exception.ToString());
                }
                else
                {
                    // Metadata contains file metadata such as size, content-type, and download URL.
                    Firebase.Storage.StorageMetadata metadata = task.Result;
                    var download_result = storage_ref.GetDownloadUrlAsync();
                    Debug.Log("Finished uploading...");
                    Debug.Log("download url = " + download_result.Result);
                }
            });
        }
    }
}
