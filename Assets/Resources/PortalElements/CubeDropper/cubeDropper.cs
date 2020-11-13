using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace WasaaMP {
    public class cubeDropper : MonoBehaviour {
        public GameObject interactiveObjectToInstanciate;
        public bool destroyPreviousCube;
        GameObject gameObjectSpawnPoint;
        GameObject cubeDropped;
        WaitingArea area;
        SignalReceiver signalReceiver;

        void Start () {
            gameObjectSpawnPoint = GameObject.Find ("cubeDropper/spawnPoint");
            area = GameObject.Find ("cubeDropper/waitingArea").GetComponent<WaitingArea> ();
            signalReceiver = GetComponent<SignalReceiver> ();
        }

        public void CreateInteractiveCube () {
            if (!area.HasWaitingCube ()) {
                MonoBehaviourPun head = signalReceiver.GetHead ();
                if (PhotonNetwork.CurrentRoom.PlayerCount == 1 || (head != null && head.photonView.IsMine)) {
                    PhotonNetwork.Instantiate (interactiveObjectToInstanciate.name, gameObjectSpawnPoint.transform.position, Quaternion.identity, 0);
                }
            }
        }

        public void DestroyInteractiveCube () {
            if (destroyPreviousCube) {
                if (cubeDropped != null) {
                    cubeDropped.GetComponent<Animator> ().SetTrigger ("play");
                    Destroy (cubeDropped, 1f);
                }
                cubeDropped = area.GetWaitingCube ();
            }
        }
    }
}