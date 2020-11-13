using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WasaaMP {
    public class WaitingArea : MonoBehaviour {
        bool hasObject;
        GameObject cubeObject;

        // Start is called before the first frame update
        void Start () {
            hasObject = false;
        }

        void OnTriggerEnter (Collider other) {
            var cube = other.gameObject.GetComponent<TestCubeChamber> ();
            if (cube != null) {
                hasObject = true;
                cubeObject = other.gameObject;
            }
        }

        void OnTriggerExit (Collider other) {
            var cube = other.gameObject.GetComponent<TestCubeChamber> ();
            if (cube != null) hasObject = false;
        }

        public bool HasWaitingCube () {
            return hasObject;
        }

        public GameObject GetWaitingCube () {
            return cubeObject;
        }
    }
}