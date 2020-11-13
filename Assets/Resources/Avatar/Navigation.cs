using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace WasaaMP {
    public class Navigation : MonoBehaviourPunCallbacks {

        #region Public Fields

        // to be able to manage the offset of the camera
        public Vector3 cameraPositionOffset = new Vector3 (0, 1.6f, 0);
        public Quaternion cameraOrientationOffset = new Quaternion ();

        [Tooltip ("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;
        public new Camera camera { get; private set; }
        Transform cameraTransform;

        #endregion
        void Awake () {
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine) {
                LocalPlayerInstance = this.gameObject;
            }
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            //DontDestroyOnLoad (this.gameObject) ;
        }

        // Start is called before the first frame update
        void Start () {
            if (photonView.IsMine) { // || ! PhotonNetwork.IsConnected) {
                // attach the camera to the navigation rig
                camera = (Camera) GameObject.FindObjectOfType (typeof (Camera));
                cameraTransform = camera.transform;
                cameraTransform.SetParent (transform);
                cameraTransform.localPosition = cameraPositionOffset;
                cameraTransform.localRotation = cameraOrientationOffset;
            }
        }

        // Update is called once per frame
        void Update () {
            if (photonView.IsMine) { // || ! PhotonNetwork.IsConnected) {
                var x = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
                var z = Input.GetAxis ("Vertical") * Time.deltaTime * 6.0f;
                transform.Rotate (0, x, 0);
                transform.Translate (0, 0, z);
            }
        }
    }

}