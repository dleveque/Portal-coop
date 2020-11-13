using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace WasaaMP {
    public class SignalReceiver : MonoBehaviour {
        public SignalWire[] signalsIn;
        private Animator Controler;
        // Start is called before the first frame update
        void Start () {
            Controler = this.GetComponent<Animator> ();
        }

        // Update is called once per frame
        void Update () {
            if (Array.TrueForAll (signalsIn, IsActive)) Controler.SetBool ("open", true);
            else Controler.SetBool ("open", false);
        }

        bool IsActive (SignalWire signal) {
            return signal.IsActive ();
        }

        public MonoBehaviourPun GetHead () {
            return (signalsIn.Length > 0) ? signalsIn[0].head : null;
        }
    }
}