using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCursor : MonoBehaviour {
    public GameObject cursor;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update () {
        transform.LookAt (cursor.transform);
    }
}