using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace WasaaMP {
	public class CursorTool : MonoBehaviourPun {
		float previousX, previousY = 0;
		bool active;
		public bool caught { get; private set; }
		public GameObject tool;
		public GameObject messageIcon;
		private Interactive target;
		MonoBehaviourPun targetParent;

		MonoBehaviourPun player;
		Camera cameraPlayer;
		Image InfoBox;
		Text InfoBoxText;

		void Start () {
			active = false;
			caught = false;
			player = (MonoBehaviourPun) this.GetComponentInParent (typeof (Navigation));
			cameraPlayer = (Camera) GameObject.FindObjectOfType (typeof (Camera));
			name = player.name + "_" + name;
			InfoBox = GameObject.Find ("Canvas/Bottom Panel/Info Box").GetComponent<Image> ();
			InfoBoxText = GameObject.Find ("Canvas/Bottom Panel/Info Box/Text").GetComponent<Text> ();

			// custom Color
			Color color = new Color (
				(float) player.photonView.Owner.CustomProperties["r"],
				(float) player.photonView.Owner.CustomProperties["g"],
				(float) player.photonView.Owner.CustomProperties["b"]
			);
			GetComponentInChildren<Renderer> ().material.color = color;
			tool.GetComponentInChildren<Renderer> ().material.color = color;
		}

		void Update () {
			// control of the 3D cursor
			if (player.photonView.IsMine || !PhotonNetwork.IsConnected) {
				if (Input.GetButtonDown ("Fire1")) {
					Fire1Pressed (Input.mousePosition.x, Input.mousePosition.y);
				}
				if (Input.GetButtonUp ("Fire1")) {
					Fire1Released (Input.mousePosition.x, Input.mousePosition.y);
				}
				if (active) {
					Fire1Moved (Input.mousePosition.x, Input.mousePosition.y, Input.mouseScrollDelta.y);
				}
				if (Input.GetKeyDown (KeyCode.C)) {
					SendCarryMessage ();
				}
				if (Input.GetKeyDown (KeyCode.V)) {
					SendPointMessage ();
				}
				if (Input.GetKeyDown (KeyCode.B)) {
					if (this.target != null) {
						if (this.target.GetType () == typeof (InteractiveHandle)) Catch ();
						if (this.target.GetType () == typeof (InteractiveButton)) Push ();
					}
				}
				if (Input.GetKeyDown (KeyCode.N)) {
					if (this.target.GetType () == typeof (InteractiveHandle)) Release ();
				}

				InfoBox.enabled = (this.target != null);
				if (this.target == null) InfoBoxText.text = "";
				else if (this.target.GetType () == typeof (InteractiveHandle)) InfoBoxText.text = (!this.caught) ? "Use 'B' to grab" : "Use 'N' to drop";
				else if (this.target.GetType () == typeof (InteractiveButton)) InfoBoxText.text = "Use 'B' to interract";
			}
		}

		public void Fire1Pressed (float mouseX, float mouseY) {
			active = true;
			previousX = mouseX;
			previousY = mouseY;
		}

		public void Fire1Released (float mouseX, float mouseY) {
			active = false;
		}

		public void Fire1Moved (float mouseX, float mouseY, float mouseZ) {
			float deltaX = (mouseX - previousX) / 100.0f;
			float deltaY = (mouseY - previousY) / 100.0f;
			float deltaZ = mouseZ / 10.0f;
			previousX = mouseX;
			previousY = mouseY;
			transform.Translate (deltaX, deltaY, deltaZ);
		}

		public void Push () {
			if (target != null && target.GetType () == typeof (InteractiveButton)) {
				photonView.RPC ("PushButton", RpcTarget.All, target.photonView.ViewID, photonView.ViewID);
				PhotonNetwork.SendAllOutgoingCommands ();
			}
		}

		public void Catch () {
			if (target != null && target.GetType () == typeof (InteractiveHandle)) {
				//var tb = target.GetComponent<Interactive> ();
				var support = ((InteractiveHandle) target).GetSupport ();
				if ((!caught) && (this != support)) { // pour ne pas prendre 2 fois l'objet et lui faire perdre son parent
					targetParent = support;
				}
				//print ("ChangeSupport of object " + target.photonView.ViewID + " to " + photonView.ViewID);
				photonView.RPC ("ChangeSupport", RpcTarget.All, target.photonView.ViewID, photonView.ViewID);
				target.photonView.TransferOwnership (PhotonNetwork.LocalPlayer);
				PhotonNetwork.SendAllOutgoingCommands ();
				caught = true;

			}
		}

		public void Release () {
			if (target != null && target.GetType () == typeof (InteractiveHandle)) {
				//var tb = target.GetComponent<Interactive> ();
				if (targetParent != null) {
					photonView.RPC ("ChangeSupport", RpcTarget.All, target.photonView.ViewID, targetParent.photonView.ViewID);
					targetParent = null;
				} else {
					photonView.RPC ("RemoveSupport", RpcTarget.All, target.photonView.ViewID);
				}
				PhotonNetwork.SendAllOutgoingCommands ();
				caught = false;
				target = null;

			}
		}

		void SendCarryMessage () {
			var msgObject = PhotonNetwork.Instantiate (messageIcon.name, transform.position, transform.rotation, 0);
			msgObject.transform.forward = cameraPlayer.transform.forward;
			photonView.RPC ("SendMessage", RpcTarget.All, msgObject.GetComponent<MessageIcon> ().photonView.ViewID, photonView.ViewID, "carry");
			PhotonNetwork.SendAllOutgoingCommands ();
		}

		void SendPointMessage () {
			var msgObject = PhotonNetwork.Instantiate (messageIcon.name, transform.position, transform.rotation, 0);
			msgObject.transform.forward = cameraPlayer.transform.forward;
			photonView.RPC ("SendMessage", RpcTarget.All, msgObject.GetComponent<MessageIcon> ().photonView.ViewID, photonView.ViewID, "point");
			PhotonNetwork.SendAllOutgoingCommands ();
		}

		void OnTriggerEnter (Collider other) {
			//print ("cursorTools Enter: " + other.gameObject + ", tags: " + other.tag);
			var t = other.gameObject.GetComponent<Interactive> ();
			if (t != null && !caught) {
				target = t;
			}
		}

		void OnTriggerExit (Collider other) {
			//print ("cursorTools Exit: " + other.gameObject);
			var t = other.gameObject.GetComponent<Interactive> ();
			if (t != null && !caught) {
				target = null;
			}
		}

		[PunRPC]
		public void ChangeSupport (int interactiveID, int newSupportID) {
			InteractiveHandle go = PhotonView.Find (interactiveID).gameObject.GetComponent<InteractiveHandle> ();
			MonoBehaviourPun s = PhotonView.Find (newSupportID).gameObject.GetComponent<MonoBehaviourPun> ();
			//print ("ChangeSupport of object " + go.name + " to " + s.name);
			go.SetSupport (s);
		}

		[PunRPC]
		public void RemoveSupport (int interactiveID) {
			var pv = PhotonView.Find (interactiveID);
			if (pv) {
				InteractiveHandle go = pv.gameObject.GetComponent<InteractiveHandle> ();
				//print ("RemoveSupport of object " + go.name);
				if (go) go.RemoveSupport ();
			}
		}

		[PunRPC]
		public void PushButton (int interactiveID, int newSupportID) {
			InteractiveButton go = PhotonView.Find (interactiveID).gameObject.GetComponent<InteractiveButton> ();
			MonoBehaviourPun s = PhotonView.Find (newSupportID).gameObject.GetComponent<MonoBehaviourPun> ();
			if (go) go.Push (s);
		}

		[PunRPC]
		public void SendMessage (int interactiveID, int newSupportID, string type) {
			MessageIcon go = PhotonView.Find (interactiveID).gameObject.GetComponent<MessageIcon> ();
			MonoBehaviourPun s = PhotonView.Find (newSupportID).gameObject.GetComponent<MonoBehaviourPun> ();
			if (go) {
				go.Send (s, type);
			}
		}

	}
}