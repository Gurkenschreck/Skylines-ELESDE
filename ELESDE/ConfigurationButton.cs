using ColossalFramework.UI;
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

            this.text = "DROP";
            this.width = 50;
            this.height = 50;
            this.relativePosition = new Vector3(20, 50, 0);
            this.normalBgSprite = "ButtonMenu";
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
            effectThread.Interrupt();

            //Select next VisualState
            visualState = Next(visualState);

            //TEST
            //Sets the visual state to the next level
            //visualState = (VisualState)Enum.ToObject(typeof(VisualState), ((int)visualState + 1));
            //OR
            //visualState = (VisualState)4;
            //Checks if the visual state is the last one
            //if ((int)visualState > Enum.GetNames(typeof(VisualState)).Length)
            //    visualState = VisualState.None;

            //Cycle through every option
            if(visualState == VisualState.None)
            {
                //Nothing happens
            }
            else if(visualState == VisualState.FadeColor)
            {
                effectThread = lem.FadeColorAsync();
            }
            else if (visualState == VisualState.FadeColorSmooth)
            {
                effectThread = lem.FadeColorSmoothAsync();
            }
            else if (visualState == VisualState.FlipShit)
            {
                effectThread = lem.FlipShitAsync();
            }
        }

        public override void OnDisable()
        {
            if (effectThread != null)
                if (effectThread.IsAlive)
                    effectThread.Interrupt();
        }

        /// <summary>
        /// Represents the current visual state.
        /// </summary>
        private enum VisualState
        {
            None = 0,
            FadeColor,
            FadeColorSmooth,
            FlipShit
        }

        /// <summary>
        /// Cycles through the VisualState enum and returns the next enum in row.
        /// </summary>
        /// <param name="visualState">VisualState to cycle through.</param>
        /// <returns>Next VisualState state.</returns>
        public static VisualState Next(this VisualState visualState)
        {
            switch (visualState)
            {
                case VisualState.None: //
                    return VisualState.FadeColor;
                case VisualState.FadeColor:
                    return VisualState.FadeColorSmooth;
                case VisualState.FadeColorSmooth:
                    return VisualState.FlipShit;
                case VisualState.FlipShit:
                    return VisualState.None;
                default:
                    return VisualState.None;
            }
        }
    }
}
