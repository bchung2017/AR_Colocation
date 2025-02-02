using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrentEventObject 
{
    public static string eventId { get; set; }
    public static string numOfOrgs { get; set; }
    public static string uid { get; set; }
    public static List<string> orgs { get; set; }
    public static Vector3 origin {get; set;}
    public static GameObject ev {get; set;}

    public static Vector3[] positions = new Vector3[] {
        new Vector3 (-3,0,3),
        new Vector3 (3,0,3),
        new Vector3 (3,0,-3),
        new Vector3 (-3,0,-3),
        new Vector3 (-9,0,-3),
        new Vector3 (-9,0,3),
        new Vector3 (9,0,3),
        new Vector3 (9,0,-3),
        new Vector3 (9,0,-9),
        new Vector3 (3,0,-9),
        new Vector3 (-3,0,-9),
        new Vector3 (-9,0,-9),
        new Vector3 (-9,0,9),
        new Vector3 (-3,0,9),
        new Vector3 (3,0,9),
        new Vector3 (9,0,9)
    };

    public static List<GameObject> areas = new List<GameObject>();

    static CurrentEventObject()
    {
        eventId = "";
        numOfOrgs = "";
        uid = "";
        orgs = new List<string>();
    }

    public static void AddOrg(string value)
    {
        // Record this value in the list.
        orgs.Add(value);
    }

    public static void Display()
    {
        // Write out the results.
        foreach (var value in orgs)
        {
            Debug.Log(value);
        }
    }

    public static bool IsOriginSet()
    {
        if(origin != null)
            return true;
        else
            return false;
    }
}
