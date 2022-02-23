using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.cringejam.sticksandstones {

    public class Enemy : MonoBehaviour {

        #region Declares

        //Declare serializables
        [SerializeField] private int health = 0;

        //Declare publics
        [NonSerialized] public int fullHealth = 0;

        #endregion

        #region Start

        private void Start() {
            //Set
            fullHealth = health;
        }

        #endregion

    }

}