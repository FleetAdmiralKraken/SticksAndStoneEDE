using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.cringejam.sticksandstones {

    public class InputCache : MonoBehaviour {

        #region Declares

        //Declare serializables
        public Axes keyboardAxes = new Axes();
        public Axes mouseAxes = new Axes();

        //Declare publics
        [NonSerialized] public bool runPressed = false;

        #endregion

        #region Classes

        [Serializable]
        public class Axes {
            //Declare
            public string[] stringAxes = null; //Horizontal, vertical, etc.
            [NonSerialized] public float[] floatAxes = null;
            //Constructor
            public void ExpandFloatAxes() {
                //Expand
                floatAxes = new float[stringAxes.Length];
            }
        }

        #endregion

        #region Start

        private void Start() {
            //Expand
            keyboardAxes.ExpandFloatAxes();
            mouseAxes.ExpandFloatAxes();
        }

        #endregion

        #region Public functions

        public void Cache() {
            //Set axes
            SetAxes(keyboardAxes);
            SetAxes(mouseAxes);
            //Set
            runPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        }

        private void SetAxes(Axes axes) {
            //Loop to set axis
            for (int i = 0; i < axes.stringAxes.Length; i++) {
                //Set axis
                axes.floatAxes[i] = Input.GetAxis(axes.stringAxes[i]);
            }
        }

        #endregion

    }

}