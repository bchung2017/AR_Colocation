using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Firebase;
using Firebase.Database;

public class FirebaseInit : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent OnFirebaseInitialized = new UnityEvent();
    private async void Start()
    {
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dependencyStatus == DependencyStatus.Available)
        {
            OnFirebaseInitialized.Invoke();
            FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        }   
    }
}
