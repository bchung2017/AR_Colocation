using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GenerateEventPrefab : MonoBehaviour
{
    public GameObject areaObject;
    public GameObject eventObject;
    public GameObject exhibitObject;
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private int orgsPerArea;
    private IEnumerator GetData;
    void Start()
    {
        CurrentEventObject.Display();
        Debug.Log("Number of events in EventScene: " + CurrentEventObject.orgs.Count);
        // CurrentEventObject.AddOrg("Bloomberg");
        // CurrentEventObject.AddOrg("Solidworks");

        // CurrentEventObject.ev = Instantiate(eventObject, Vector3.zero, Quaternion.identity);
        int numOfAreas = (CurrentEventObject.orgs.Count - 1) / orgsPerArea + 1;
        for (int i = 0; i < numOfAreas; i++)
        {
            GameObject a = Instantiate(areaObject, CurrentEventObject.positions[i], Quaternion.identity);
            for (int j = 0; j < orgsPerArea; j++)
            {
                int currentIndex = i * orgsPerArea + j;
                if(currentIndex >= CurrentEventObject.orgs.Count)
                    break;
                // Debug.Log("i: " + i + "\nj: " + j +"\ncurrentIndex: " + currentIndex);
                string currentOrgName = CurrentEventObject.orgs[currentIndex];
                getElements(currentOrgName, snapshot =>
                {
                    // Debug.Log("Snapshot retrieved for: " + currentOrgName);
                    UnityMainThreadDispatcher.Instance().Enqueue(CreateExhibit(snapshot, a, j));
                });
            }

            a.transform.SetParent(eventObject.transform);
        }
        UnityMainThreadDispatcher.Instance().Enqueue(FinishLoading());
    }

    public void getElements(string currentOrgName, System.Action<DataSnapshot> onSnapshotRetrieved)
    {
        // Debug.Log("current orgName: " + currentOrgName);
        Query q = FirebaseDatabase.DefaultInstance.GetReference("orgs").OrderByChild("orgName").EqualTo(currentOrgName);
        q.KeepSynced(true);
        q.GetValueAsync().ContinueWith(task =>
        {

            if (task.IsFaulted)
            {
                Debug.Log("error");
            }
            else if (task.IsCompleted)
            {
                onSnapshotRetrieved(task.Result);
            }
        });
    }

    private IEnumerator CreateExhibit(DataSnapshot snapshot, GameObject area, int j)
    {
        string ss = snapshot.GetRawJsonValue().ToString();
        // Debug.Log("Raw Json: " + ss);
        dynamic data = JsonConvert.DeserializeObject(ss);
        dynamic data2 = data.First.Value;
        string t = data2["orgName"].Value.ToString();
        string d = data2["orgDesc"].Value.ToString();
        string u = data2["imgUrl"].Value.ToString();
        GameObject _exhibit = Instantiate(exhibitObject, area.GetComponent<AreaMetadata>().GetNextPos(), Quaternion.Euler(area.GetComponent<AreaMetadata>().GetNextRot()));
        // Debug.Log("Instantiating " + t + "at " + _exhibit.transform.position.ToString());
        _exhibit.GetComponent<ExhibitMetadata>().SetupMetaData(t, d, u);
        _exhibit.transform.SetParent(area.transform, false);
        yield return null;
    }

    private IEnumerator FinishLoading() {
        loadingScreen.SetActive(false);
        eventObject.GetComponent<SelectRoom>().enabled = true;
        yield return null;
    }



    // Update is called once per frame
    void Update()
    {

    }
}
