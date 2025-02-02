// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;
// using UnityEngine.Networking;

// public class API : MonoBehaviour
// {
//     public string fireBaseStorageURL = "gs://pare-58bd5.appspot.com/Assets/AssetBundles/";

//     Firebase.Storage.FirebaseStorage storage;
//     public void GetBundleObject(string assetName, UnityAction<GameObject> callback, Transform bundleParent)
//     {
//         StartCoroutine(GetDisplayBundleRoutine(assetName, callback, bundleParent));
//     }

//     IEnumerator GetDisplayBundleRoutine(string assetName, UnityAction<GameObject> callback, Transform bundleParent)
//     {
//         string bundleURL = fireBaseStorageURL + assetName + "-";

// #if UNITY_ANDROID
//             bundleURL += "Android";
// #else
//         bundleURL += "IOS";
// #endif

//         storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
//         storage_ref = storage.GetReferenceFromUrl(bundleURL);
//         Debug.Log("Requesting bundle at " + bundleURL);
//         storage_ref.GetDownloadUrlAsync().ContinueWith((Task<Uri> task) =>
//             {
//                 if (!task.IsFaulted && !task.IsCanceled)
//                 {
//                     Debug.Log("Download URL: " + task.Result);
//                     // ... now download the file via WWW or UnityWebRequest.
//                     UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL);
//                     yield return www.SendWebRequest();

//                     if (www.isNetworkError)
//                     {
//                         Debug.Log("Network error");
//                     }
//                     else
//                     {
//                         AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
//                         if (bundle != null)
//                         {
//                             string rootAssetPath = bundle.GetAllAssetNames()[0];
//                             GameObject arObject = Instantiate(bundle.LoadAsset(rootAssetPath) as GameObject, bundleParent);
//                             bundle.Unload(false);
//                             callback(arObject);
//                         }
//                         else
//                         {
//                             Debug.Log("Not a valid asset bundle");
//                         }
//                     }





//                     // WebClient client = new WebClient();
//                     // client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadFileCompleted);
//                     // client.DownloadFileAsync(task.Result, "Assets/penguin_bundle1");
//                     // placeObject1.CreateObject("Assets/penguin_bundle1");


//                 }
//             });
//         //request asset bundle

//     }
// }
