using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrentAreaObject
{
    public static Vector3 pos { get; set; }
    public static bool isBuilt { get; set; }
    private static Vector3 _orgPos;


    

    public static Vector3 orgPos
    {
        get
        {
            return _orgPos;
        }
        set
        {
            if (!isBuilt)
                _orgPos = value;
        }
    }

    static CurrentAreaObject()
    {
        pos = Vector3.zero;
        orgPos = Vector3.zero;
    }


}
