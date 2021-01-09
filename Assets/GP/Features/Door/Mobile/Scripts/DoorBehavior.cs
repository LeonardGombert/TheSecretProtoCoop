﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
namespace Gameplay.Mobile
{
    public class DoorBehavior : MonoBehaviour, ISwitchable
    {
        public enum LockState { Locked, Unlocked }
        public LockState lockState;

        [Range(0, 1), SerializeField] private int state;
        [Range(0, 1), SerializeField] private int power;

        [Header("---References---")]
        [SerializeField] private Color color_Open_Unlocked;
        [SerializeField] private Color color_Open_Locked;
        [SerializeField] private Color color_Close;
        [SerializeField] private Outline outline;
        [SerializeField] private Animator anim;
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
        private void OnEnable()
        {
            CheckLockState();
        }
        private void Start() => Power = power;


        public void TurnOn()
        {
            DOTween.To(() => outline.OutlineColor, x => outline.OutlineColor = x, color_Close, .5f);
        }

        public void TurnOff()
        {
            if (lockState == LockState.Locked)
            {
                DOTween.To(() => outline.OutlineColor, x => outline.OutlineColor = x, color_Open_Locked, .5f);
            }
            else
            {
                DOTween.To(() => outline.OutlineColor, x => outline.OutlineColor = x, color_Open_Unlocked, .5f);
            }
        }

        [Button("Unlock")]
        public void Unlock()
        {

            lockState = LockState.Unlocked;
            FeedbackUnlock();
            if (Power == 0)
            {
                TurnOff();
            }
        }

        private void CheckLockState()
        {
            if(lockState == LockState.Locked)
            {
                
                //GetComponent<Button>().enabled = true;
                //anim.enabled = true;
            }
            else
            {
                FeedbackUnlock();
            }
        }

        private void FeedbackUnlock()
        {
            
            //GetComponent<Button>().enabled = false;
            //anim.enabled = false;

            if (power == 1) TurnOn();
            else TurnOff();
        }

        public void RaiseLoadSymbols(CallableFunction loadSymbols) { loadSymbols.Raise(); }

        // Only for Debug !!
        [Button]
        public void ChangePower()
        {
            if (Power == 1) Power = 0;
            else Power = 1;
        }


    }
}

