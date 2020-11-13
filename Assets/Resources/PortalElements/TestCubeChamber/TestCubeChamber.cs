using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace WasaaMP {
    public class TestCubeChamber : MonoBehaviourPun {

        public InteractiveHandle[] handles;
        public float maxLiftingHeightWithOneHandle;
        public InterfaceBase interfaceBase;
        bool firstTaken;
        float rotationOffset;

        void Start () {
            firstTaken = true;
        }

        // Update is called once per frame
        void Update () {
            // check if one face is grab
            bool taken = IsOneOrMoreHandleTaken ();
            SetAntiGravity (taken);

            if (taken) {
                NewPositionFromHandles ();
                NewRotationFromTools ();
                NewPositionFromLiftHeightAllowed ();
                if (transform.position.y >= LiftHeightAllowed ()) Shake (Mathf.Exp (GetDistanceFromFarthestHandle () - 1f) * 0.3f);
                firstTaken = false;
            } else {
                interfaceBase.transform.position = transform.position;
                firstTaken = true;
            }
        }

        void OnDestroy () {
            foreach (InteractiveHandle handle in handles) {
                Destroy (handle.gameObject);
            }
        }

        void SetAntiGravity (bool value) {
            this.GetComponent<Rigidbody> ().constraints = (value) ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;
        }

        public bool IsOneOrMoreHandleTaken () {
            return Array.Exists (handles, handle => handle.HasSupport ());
        }

        public int HandleTakenCount () {
            int i = 0;
            foreach (InteractiveHandle handle in handles) {
                if (handle.HasSupport ()) i++;
            }
            return i;
        }

        public float GetDistanceFromFarthestHandle () {
            float distanceMax = 0f;
            foreach (InteractiveHandle handle in handles) {
                float distance = Vector3.Distance (handle.gameObject.transform.position, this.transform.position);
                if (distance > distanceMax) distanceMax = distance;
            }
            return distanceMax;
        }

        float LiftHeightAllowed () {
            return maxLiftingHeightWithOneHandle * HandleTakenCount ();
        }

        void NewPositionFromHandles () {
            // sum of all interface position
            Vector3 sumPosition = new Vector3 ();
            foreach (InteractiveHandle handle in handles) {
                sumPosition += handle.transform.position;
            }

            // we check if this new position is possible without collision
            Vector3 newPosition = (sumPosition / handles.Length);
            interfaceBase.transform.position = newPosition;

            if (!interfaceBase.isColliding) {
                transform.localPosition = newPosition;
            }
        }

        void NewRotationFromTools () {
            Vector3 relativePos = new Vector3 ();
            foreach (InteractiveHandle handle in handles) {
                if (handle.HasSupport ()) {
                    if (firstTaken) rotationOffset = Quaternion.Angle (transform.rotation, handle.gameObject.transform.rotation);
                    var v = (transform.position - handle.GetTool ().transform.position);
                    relativePos += (HandleTakenCount () < 2) ? v : new Vector3 (
                        Mathf.Pow (v.x, 2f),
                        Mathf.Pow (v.y, 2f),
                        Mathf.Pow (v.z, 2f)
                    );       
                }
            }
            if (HandleTakenCount () > 1) {
                relativePos = new Vector3 (
                    Mathf.Sqrt (relativePos.x),
                    Mathf.Sqrt (relativePos.y),
                    Mathf.Sqrt (relativePos.z)
                );
            }
            transform.rotation = Quaternion.LookRotation (relativePos) * Quaternion.Euler (0, rotationOffset, 0);
        }

        void NewPositionFromLiftHeightAllowed () {
            var newPos = transform.localPosition;
            transform.localPosition = new Vector3 (newPos.x, (newPos.y > LiftHeightAllowed ()) ? LiftHeightAllowed () : newPos.y, newPos.z);
        }

        void Shake (float magnitude) {
            float shakeAngle = 5f;
            float speed = 50f;
            float angle = shakeAngle * magnitude * Mathf.Sin (Time.time * speed) * 0.008f;
            transform.position = new Vector3 (transform.position.x - angle, transform.position.y - angle, transform.position.z - angle);
        }
    }
}