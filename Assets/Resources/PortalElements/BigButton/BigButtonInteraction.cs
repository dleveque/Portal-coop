using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WasaaMP {
    public class BigButtonInteraction : MonoBehaviour {
        Signal signalOut;
        Rigidbody rb;
        Animator animator;

        // Start is called before the first frame update
        void Start () {
            signalOut = GetComponent<Signal> ();
            animator = GetComponentInChildren<Animator> ();
        }

        void OnTriggerEnter (Collider other) {
            rb = other.gameObject.GetComponent<Rigidbody> ();
            if (rb != null) {
                InvokeRepeating ("WillDeactive", 0f, 0.1f);
            }
        }

        void OnTriggerExit (Collider other) {
            rb = other.gameObject.GetComponent<Rigidbody> ();
            if (rb != null) {
                Deactivate ();
                CancelInvoke ();
            }
        }

        void OnTriggerStay (Collider other) {
            rb = other.gameObject.GetComponent<Rigidbody> ();
            if (rb != null) {
                if (rb.mass >= 50) {
                    Activate ();
                    CancelInvoke ("Deactivate");
                }
            }
        }

        void Activate () {
            signalOut.Activate ();
            animator.SetBool ("up", false);
        }

        void Deactivate () {
            signalOut.Deactivate ();
            animator.SetBool ("up", true);
        }

        void WillDeactive () {
            Invoke ("Deactivate", 0.05f);
        }
    }
}