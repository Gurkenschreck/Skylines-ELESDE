﻿using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ELESDE
{
    public class ConfigurationButton : UIButton
    {
        //Fields
        VisualState visualState;
        LightEffectManager lem;
        UIDragHandle dh; //Draghandle

        public override void Start()
        {
            if (ELESDEMod.IsDebug) Log.Message("ConfigurationButton start");
            lem = new LightEffectManager(ref Light.GetLights(LightType.Directional, 0)[0]);
            visualState = VisualState.None;
            dh = (UIDragHandle)this.AddUIComponent(typeof(UIDragHandle)); // ?? this.AddUIComponent(typeof(UIDragHandle)) as UIDragHandle; //Activates the dragging of the window

            //this.text = "DROP";
            this.width = 50;
            this.height = 50;
            this.relativePosition = new Vector3(10, 80, 0);
            this.normalFgSprite = "IconPolicyRecreationalUse";
            this.normalBgSprite = "ButtonMenu";
            this.pressedFgSprite = "NotificationIconExtremelyHappy";
            this.disabledBgSprite = "ButtonMenuDisabled";
            this.hoveredBgSprite = "ButtonMenuHovered";
            this.focusedBgSprite = "ButtonMenuFocused";
            this.pressedBgSprite = "ButtonMenuPressed";
            this.textColor = new Color32(255, 51, 153, 150);
            this.disabledTextColor = new Color32(7, 7, 7, 200);
            this.hoveredTextColor = new Color32(255, 255, 255, 255);
            this.pressedTextColor = new Color32(204, 0, 0, 255);
            this.playAudioEvents = true;
            this.eventClick += ConfigurationButton_eventClick;
        }

        public override void Update()
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {   //Shortcut for switching modes is LeftAlt + D
                if(Input.GetKeyDown(KeyCode.D))
                    visualState = Next(visualState);

                if (Input.GetKeyDown(KeyCode.M))
                {   //Enable disable debug
                    ELESDEMod.IsDebug = !ELESDEMod.IsDebug;
                    Log.Message("IsDebug enabled: " + ELESDEMod.IsDebug);
                }

                if (Input.GetKeyDown(KeyCode.K))
                    Log.Message(String.Format("Is thread running: {0}, shall thread stop: {1}", lem.IsThreadRunning, lem.StopAllEffects));
            }
        }

        void ConfigurationButton_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            if (ELESDEMod.IsDebug) Log.Message("Eventclick");

            //At first every effect shall be interrupted
            lem.StopAllEffects = true;

            //Select next VisualState
            try
            {
                if (ELESDEMod.IsDebug) Log.Message("Old visualState: " + visualState);
                visualState = Next(visualState);
                if (ELESDEMod.IsDebug) Log.Message("New visualState: " + visualState);
            }
            catch (Exception ex)
            {
                Log.Error("Next Exception: Message: " + ex.Message + "; Exception: " + ex.ToString());
            }
        }

        public override void OnDisable()
        {
            if (ELESDEMod.IsDebug) Log.Message("OnDisable");
            visualState = VisualState.None;

            if (dh != null)
                UnityEngine.Object.Destroy(dh);

            //Log.Message((dh != null) ? "dh is not null" : "dh is null");

            lem.Reset();
        }

        /// <summary>
        /// Cycles through the VisualState enum and returns the next enum in row.
        /// </summary>
        /// <param name="visualState">VisualState to cycle through.</param>
        /// <returns>Next VisualState state.</returns>
        public VisualState Next(VisualState visualState)
        {
            switch (visualState)
            {
                case VisualState.None: //
                    lem.FadeColorSmoothInThread();
                    return VisualState.FadeColorSmooth;
                case VisualState.FadeColorSmooth:
                    lem.FadeColorInThread();
                    return VisualState.FadeColor;
                case VisualState.FadeColor:
                    lem.FlipShitInThread();
                    return VisualState.FlipShit;
                case VisualState.FlipShit:
                    lem.FlipShitHardInThread();
                    return VisualState.FlipShitHard;
                case VisualState.FlipShitHard: //After FlipShitHard go back to normal
                    lem.Reset();
                    return VisualState.None;
                default:
                    return VisualState.None;
            }
        }
    }
}
