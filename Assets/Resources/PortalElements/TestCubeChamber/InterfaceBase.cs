using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WasaaMP {
    public class InterfaceBase : MonoBehaviour {
        public bool isColliding { get; private set; }

        void Start () {
            isColliding = false;
        }

        void OnTriggerExit (Collider other) {
            isColliding = false;
        }

        void OnTriggerStay (Collider other) {
            if (other.gameObject.tag != "Passable" && other.gameObject.tag != "TestCubeChamber") {
                isColliding = true;
            }
        }
    }
}
