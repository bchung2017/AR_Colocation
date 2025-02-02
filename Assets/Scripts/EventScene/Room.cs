using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public GameObject room {get; set;}
    public Vector3 ogPos {get; set;}

    public Room (GameObject gameObject, Vector3 position) {
        this.room = gameObject;
        this.ogPos = position;
    }
}
