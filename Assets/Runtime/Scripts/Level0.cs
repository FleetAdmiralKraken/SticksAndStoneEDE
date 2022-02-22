using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.cringejam.sticksandstones {

    public class Level0 : MonoBehaviour {

        #region Declares

        //Declare serializables
        [SerializeField] private DoorScale doorScale = null;
        [SerializeField] private Door door = null;
        [SerializeField] private FloorScaleSpikes floorScaleSpikes = null;

        #endregion

        #region Start

        private void Start() {
            //Random
            if (Random.Range(0, 2) == 0) {
                //Real door
                door.SetToShow();
            } else {
                //Fake door
                doorScale.SetToShow();
            }

            floorScaleSpikes.SetToShow();

        }

        #endregion

    }

}
