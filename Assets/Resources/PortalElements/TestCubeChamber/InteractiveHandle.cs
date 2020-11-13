using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace WasaaMP {
  public class InteractiveHandle : Interactive {
    // Start is called before the first frame update
    MonoBehaviourPun support = null;
    GameObject parent;
    Vector3 initialPosition;

    new void Start () {
      base.Start ();
      parent = this.transform.parent.gameObject;
      initialPosition = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update () {
      this.transform.LookAt (this.parent.transform);
      if (this.support) {
        var dist = Vector3.Distance (this.parent.transform.position, this.transform.position);
        if (dist > 2f) ReleaseRequest ();

        // change the alpha handle color 
        var c = holdColor;
        c.a = 1f - dist / 2f;
        render.material.color = c;
      }
    }

    void OnDestroy () {
      ReleaseRequest ();
    }

    void ReleaseRequest () {
      if (this.support) support.GetComponentInParent<CursorTool> ().Release ();
    }

    public void SetSupport (MonoBehaviourPun support) {
      this.support = support;
      if (support != null && support) {
        transform.SetParent (support.transform);
        render.material.color = holdColor;
      }
    }

    public void RemoveSupport () {
      if (parent) transform.SetParent (parent.transform);
      transform.localPosition = this.initialPosition;
      render.material.color = hoverColor;
      support = null;
    }

    public MonoBehaviourPun GetSupport () {
      return support;
    }

    public GameObject GetTool () {
      return (this.support) ? support.GetComponentInParent<CursorTool> ().tool : null;
    }

    public bool HasSupport () {
      return this.support != null;
    }
  }

}