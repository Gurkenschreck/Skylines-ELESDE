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
        Thread effectThread;

        public override void Start()
        {
            lem = new LightEffectManager(ref Light.GetLights(LightType.Directional, 0)[0]);
            visualState = VisualState.None;
            UIDragHandle dh = (UIDragHandle)this.AddUIComponent(typeof(UIDragHandle)); //Activates the dragging of the window


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

        void ConfigurationButton_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            //At first every effect shall be interrupted
            lem.StopAllEffects = true;

            //Select next VisualState
            try
            {
                visualState = Next(visualState);
            }
            catch (Exception ex)
            {
                Log.Error("Next Exception: Message: " + ex.Message + "; Exception: " + ex.ToString());
            }
        }

        public override void OnDisable()
        {
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
                    effectThread = lem.FadeColorSmoothInThread();
                    return VisualState.FadeColorSmooth;
                case VisualState.FadeColorSmooth:
                    effectThread = lem.FadeColorInThread();
                    return VisualState.FadeColor;
                case VisualState.FadeColor:
                    effectThread = lem.FlipShitInThread();
                    return VisualState.FlipShit;
                case VisualState.FlipShit:
                    effectThread = lem.FlipShitHardInThread();
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
