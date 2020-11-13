using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WasaaMP {
    public class CheckZone : MonoBehaviour {

        public GameManager gameManager;
        Signal signalOut;
        List<GameObject> players = new List<GameObject> ();

        // Start is called before the first frame update
        void Start () {
            signalOut = gameObject.GetComponent<Signal> ();
        }

        // Update is called once per frame
        void Update () {
            if (players.Count >= gameManager.GetPlayerCount ()) signalOut.Activate ();
            else signalOut.Deactivate ();
        }

        void OnTriggerStay (Collider other) {
            if (other.gameObject.tag == "Player" && !players.Contains (other.gameObject)) {
                players.Add (other.gameObject);
            }
        }
    }
}