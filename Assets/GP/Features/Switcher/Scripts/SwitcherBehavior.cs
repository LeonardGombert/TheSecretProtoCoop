﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Networking;

namespace Gameplay
{
    public class SwitcherBehavior : MonoBehaviour, ISwitchable
    {
        [Header("---References---")]
        public CallableFunction _switch = default;
        public CallableFunction _sendSwitcherChange = default;
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        
        [SerializeField] private Image timerEnable;
        public List<Object> nodes = default;

        [Header("---IMPORTANT---")]
        [Range(0, 1), SerializeField] private int state;
        [Range(0, 1), SerializeField] private int power;
        [Range(0, 10)]
        public float ID = default;

        public enum SwitchTimer { None, Fixed }

        public SwitchTimer switchTimer = default;
        [HideInInspector]
        public float timer = 0;
        public bool hasJammer = false;
        [HideInInspector]
        public JammerBehavior jammer;
        private float currentTimer = 0;

        public int State 
        { 
            get { return state; }

            set {
                state = value;

                if(state == 0)
                {
                    TurnOff();
                }
                else
                {
                    if (Power == 0) TurnOff();
                    else TurnOn();
                }
            }
        }

        public int Power
        {
            get { return power; }

            set { 
                power = value;

                if (power == 1)
                {
                    if (state == 1) 
                    {
                        TurnOn();
                        
                    }
                    else 
                    { 
                        TurnOff(); 
                    }
                }
                else
                {
                    TurnOff();
                }
            }
        }

        public GameObject MyGameObject { get { return this.gameObject; } set { MyGameObject = value; } }

        public void OnEnable() { SwitcherManager.switchers.Add(this); }
        public void OnDisable() => SwitcherManager.switchers.Remove(this);

        public void StartSwitcher()
        {
            if (timerEnable != null)
            {
                if (switchTimer == SwitchTimer.Fixed) ResetTimer();
            }

            if (hasJammer) jammer.AddSwitcher(this);

            Power = power;
        }

        public void CallSwitchManager()
        {
            _switch.Raise(ID);
            TransmitterManager.instance.SendSwicherChangeToOthers(ID);
        }

        private void SwitchNode()
        {
            if (nodes.Count != 0)
            {
                foreach (ISwitchable node in nodes)
                {
                    if (node.Power == 1) node.Power = 0;
                    else node.Power = 1;

                    if (node.MyGameObject.GetComponent<SwitcherBehavior>() != null) node.MyGameObject.GetComponent<SwitcherBehavior>().SwitchChildrens();
                }
            }
        }

        public void TriggerSwitch()
        {
            if (switchTimer == SwitchTimer.Fixed)
            {
                StartCoroutine(DelaySwitchNode());
                DOTween.To(() => currentTimer, x => currentTimer = x, timer, timer).SetEase(Ease.Linear).OnUpdate(SetTimerDisplayer).OnComplete(ResetTimer);
            }
            else
            {
                SwitchNode();
            }
        }

        public void SwitchChildrens()
        {
            foreach (ISwitchable node in nodes)
            {
                if (Power == 1)
                {
                    if (node.Power == 1) node.TurnOn();
                    else node.TurnOff();
                }
                else
                {
                    node.TurnOff();
                }

                if (node.MyGameObject.GetComponent<SwitcherBehavior>() != null) node.MyGameObject.GetComponent<SwitcherBehavior>().SwitchChildrens();
            }
        }

        IEnumerator DelaySwitchNode()
        {
            SwitchNode();
            TurnOff();
            yield return new WaitForSeconds(timer);

            SwitchNode();
            TurnOn();
            yield break;
        }

        public void SearchReferences()
        {
            if (nodes.Count > 0) nodes.Clear();

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.GetComponent<ISwitchable>() != null)
                {
                    nodes.Add(transform.GetChild(i).gameObject.GetComponent<ISwitchable>() as Object);
                }
            }
        }

        public void TurnOn()
        {
            ChangeSwitch(true);
        }

        public void TurnOff()
        {
            ChangeSwitch(false);
        }

        void ChangeSwitch(bool buttonState)
        {
            if (button != null) button.interactable = buttonState;
        }

        private void SetTimerDisplayer()
        {
            if (nodes.Count == 0) timerEnable.fillAmount = currentTimer / timer;
        }

        private void ResetTimer() 
        {
            if (nodes.Count == 0)
            {
                currentTimer = 0;
                timerEnable.fillAmount = 0;
            }
        }

        public void ShowLines(bool value)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].GetType() == typeof(Gameplay.Mobile.ElectricalLineBehavior))
                {
                    Gameplay.Mobile.ElectricalLineBehavior node = nodes[i] as Gameplay.Mobile.ElectricalLineBehavior;
                    node.gameObject.GetComponent<LineRenderer>().enabled = value;
                }
            }
        }
    }
}
