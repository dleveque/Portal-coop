using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace WasaaMP {
    public class Signal : MonoBehaviour {
        protected bool active = false;
        public MonoBehaviourPun head { get; protected set; }

        public void Activate (MonoBehaviourPun head = null) {
            active = true;
            this.head = head;
        }

        public void Deactivate () {
            active = false;
        }

        public bool IsActive () {
            return active;
        }
    }
}