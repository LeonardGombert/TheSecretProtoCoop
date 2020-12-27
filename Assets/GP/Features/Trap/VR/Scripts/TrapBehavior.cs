﻿using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.VR
{
    [RequireComponent(typeof(BoxCollider))]
    public class TrapBehavior : MonoBehaviour, ISwitchable
    {
        [Range(0, 1), SerializeField] private int state;
        [Range(0, 1), SerializeField] private int power;
        public GameObject MyGameObject { get { return this.gameObject; } set { MyGameObject = value; } }
        public int State { get { return state; } set { state = value; } }
        public int Power
        {
            get { return power; }
            set
            {
                power = value;
                if (power == 1) if (state == 1) TurnOn(); else TurnOff();
                else TurnOff();
            }
        }

        private void Start()
        {
            Power = power;
        }

        public void TurnOff() { GetComponent<BoxCollider>().enabled = false; }
        public void TurnOn() { GetComponent<BoxCollider>().enabled = true; }

        private void OnTriggerStay(Collider other)
        {
            if (other.transform.parent.GetComponent<IKillable>() != null) 
                other.transform.parent.GetComponent<IKillable>().Die();
        }
    }
}
