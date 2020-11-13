using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace WasaaMP {
    public class MessageIcon : MonoBehaviourPun {

        public float speed;
        public float lifeDuration;
        bool stopMoving = false;

        void Start () {
            Invoke ("Destroy", 10f);
        }

        void Update () {
            // move
            if (!stopMoving) transform.position += -transform.forward * speed * Time.deltaTime;

            // follow the correct player
            var players = GameObject.FindGameObjectsWithTag ("Player");
            foreach (GameObject player in players) {
                var view = player.GetComponent<PhotonView> ();
                if (view.IsMine) {
                    var cameraPlayer = player.GetComponent<Navigation> ().camera;
                    transform.LookAt (cameraPlayer.transform);

                    float size = Mathf.Log ((cameraPlayer.transform.position - transform.position).magnitude) * 0.8f;
                    transform.localScale = new Vector3 (size, size, size);
                }
            }
        }

        void OnTriggerEnter (Collider other) {
            if (other.gameObject.tag != "Passable") {
                stopMoving = true;
                Invoke ("Destroy", lifeDuration);
            }
        }

        void Destroy () {
            PhotonNetwork.Destroy (gameObject);
        }

        public void Send (MonoBehaviourPun sender, string type) {
            // appply sender color
            GetComponent<SpriteRenderer> ().color = new Color (
                (float) sender.photonView.Owner.CustomProperties["r"],
                (float) sender.photonView.Owner.CustomProperties["g"],
                (float) sender.photonView.Owner.CustomProperties["b"],
                0.4f
            );

            // apply type icon
            GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("PortalElements/MessageIcon/" + type);
        }
    }
}