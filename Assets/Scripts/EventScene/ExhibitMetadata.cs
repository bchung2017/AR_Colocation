using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ExhibitMetadata : MonoBehaviour
{
    private Texture tex;
    private string title;
    private string desc;

    [SerializeField]
    private GameObject titleObject;
    [SerializeField]
    private GameObject posterObject;

    public void SetupMetaData(string t, string d, string url) {
        this.title = t;
        this.desc = d;
        SetTexture(url);
    }

    public string GetDesc() {
        return this.desc;
    }

    private void SetTexture(string url)
    {
        StartCoroutine(GetTexture(url));

    }

    IEnumerator GetTexture(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
            yield return StartCoroutine(SetProperties());
        }
    }

    IEnumerator SetProperties()
    {
        titleObject.GetComponent<TextMesh>().text = title;
        posterObject.GetComponent<Renderer>().material.mainTexture = tex;
        yield return null;
    }


}
