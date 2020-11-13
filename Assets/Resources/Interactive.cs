using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace WasaaMP {
  public class Interactive : MonoBehaviourPun {
    // Start is called before the first frame update
    protected Renderer render;
    protected Color defaultColor = new Color32 (255, 255, 255, 0);
    protected Color hoverColor = new Color32 (0, 120, 255, 100);
    protected Color holdColor = new Color32 (253, 102, 0, 100);

    protected void Start () {
      render = GetComponentInChildren<Renderer> ();
      render.material.color = defaultColor;
      render.material.shader = Shader.Find ("Transparent/Diffuse");
    }

    protected void OnTriggerEnter (Collider other) {
      var cursor = other.gameObject.GetComponent<CursorTool> ();
      if (cursor != null) {
        render.material.color = hoverColor;
      }
    }

    protected void OnTriggerExit (Collider other) {
      var cursor = other.gameObject.GetComponent<CursorTool> ();
      if (cursor != null) {
        render.material.color = defaultColor;
      }
    }

  }
}