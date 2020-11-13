using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace WasaaMP {
    public class InteractiveButton : Interactive {

        Signal signalOut;
        Animator animator;

        [Tooltip ("unit = second")]
        public float timeOut = 5f;
        float previousTime;

        new void Start () {
            base.Start ();
            signalOut = GetComponentInParent<Signal> ();
            animator = GetComponentInParent<Animator> ();
        }

        void Update () {
            if (signalOut.IsActive ()) {
                if (Time.time - previousTime > timeOut) {
                    signalOut.Deactivate ();
                    render.material.color = defaultColor;
                } else {
                    render.material.color = holdColor;
                }
            }
        }

        public void Push (MonoBehaviourPun h) {
            signalOut.Activate (h);
            previousTime = Time.time;
            animator.SetTrigger("push");
        }
    }
}