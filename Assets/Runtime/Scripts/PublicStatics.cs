using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.cringejam.sticksandstones {

    public static class PublicStatics {

        #region Declares

        //Declare publics
        public static GameManager gameManager = null;
        public static InputCache inputCache = null;

        #endregion

        #region Public functions

        public static float RangeMapping(float a, float b, float x, float y, float z) {
            //Return
            return Mathf.Lerp(a, b, Mathf.InverseLerp(x, y, z));
        }

        #endregion

    }

}
