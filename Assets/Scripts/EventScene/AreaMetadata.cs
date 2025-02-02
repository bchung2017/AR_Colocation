using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaMetadata : MonoBehaviour
{
    public Vector3 position { get; set; }
    public GameObject floor;
    private int posIndex { get; set; }
    private int rotIndex {get; set;}

    public static Vector3[] positions = new Vector3[] {
        new Vector3 (-1,0,1),
        new Vector3 (1,0,1),
        new Vector3 (1,0,-1),
        new Vector3 (-1,0,-1)
    };

    public static Vector3[] rotations = new Vector3[] {
        new Vector3 (0,90,0),
        new Vector3 (0,90,0),
        new Vector3 (0,-90,0),
        new Vector3 (0,-90,0)
    };

    public Vector3 GetNextPos()
    {
        // Debug.Log("posIndex: " + posIndex);
        Vector3 nextPos = positions[posIndex];
        posIndex++;
        if (posIndex == 4)
            posIndex = 0;
        return nextPos;
    }

    public Vector3 GetNextRot()
    {
        Vector3 nextRot = rotations[rotIndex];
        rotIndex++;
        if (rotIndex == 4)
            rotIndex = 0;
        return nextRot;
    }

    void Start()
    {
        this.position = gameObject.transform.position;
        this.posIndex = 0;
        this.rotIndex = 0;
    }

    public void SetOgPosition(Vector3 currentPos) {
        this.position = currentPos;
    }
}
