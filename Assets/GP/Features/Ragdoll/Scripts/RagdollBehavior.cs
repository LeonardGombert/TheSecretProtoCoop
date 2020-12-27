﻿using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class RagdollBehavior : MonoBehaviour
    {
        public Rigidbody rb;
        public Rigidbody[] allRB;
        public float velocity;
        public GameObject go;

        [Button]
        void GetRB()
        {
            allRB = GetComponentsInChildren<Rigidbody>();
        }

        private void Awake()
        {

            for (int i = 0; i < allRB.Length; i++)
            {
                allRB[i].isKinematic = true;
            }
        }

        public void ActivateRagdoll()
        {
            for (int i = 0; i < allRB.Length; i++)
            {
                allRB[i].isKinematic = false;
            }
        }

        public void ActivateRagdollWithForce(Vector3 force, ForceMode forceMode)
        {
            for (int i = 0; i < allRB.Length; i++)
            {
                allRB[i].isKinematic = false;
                allRB[i].AddForce(force, ForceMode.Impulse);
            }
        }
    }
}
