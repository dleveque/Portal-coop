using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WasaaMP {
    public class SignalWire : Signal {
        public Sprite spriteOn;
        public Sprite spriteOff;
        public Signal signalIn;
        SpriteRenderer _sprite;

        // Update is called once per frame
        void Update () {
            if (signalIn != null) {
                active = signalIn.IsActive ();
                head = signalIn.head;
            }
            _sprite = this.GetComponent<SpriteRenderer> ();
            _sprite.sprite = (active) ? spriteOn : spriteOff;
        }
    }
}