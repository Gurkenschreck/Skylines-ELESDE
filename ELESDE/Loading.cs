using ICities;
using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ELESDE
{
    public class Loading : LoadingExtensionBase
    {
        //Fields
        LightEffectManager cem;

        //Properties

        /// <summary>
        /// Thread: Main
        /// Invoked when the extension initializes
        /// </summary>
        /// <param name="loading"></param>
        public override void OnCreated(ILoading loading) //Nachdem man start gedrückt hat
        {
        }


        /// <summary>
        /// Thread: Main
        /// Invoked when a level has completed the loading process.
        /// </summary>
        /// <param name="mode">Defines what kind of level was just loaded.</param>*
        public override void OnLevelLoaded(LoadMode mode)
        {
            //if (mode != LoadMode.NewGame)
            //    return;

            Log.Message("Initialize LightEffectManager");
            cem = new LightEffectManager(ref Light.GetLights(LightType.Directional, 0)[0]);
            cem.Light.shadowStrength = 1;
            cem.CyclesPerSecond = 20;
            Log.Message("Starting FlipshitThread Async.");
            Thread threadToStop = cem.FlipShitInThread();
            threadToStop.Interrupt();
            Log.Message("Started Thread Async.");
        }

        /// <summary>
        /// Thread: Main
        /// Invoked when the level is unloading (typically when going back to the main menu
        /// or prior to loading a new level)
        /// </summary>
        public override void OnLevelUnloading()
        {
        }

        /// <summary>
        /// Thread: Main
        /// Invoked when the extension deinitializes.
        /// </summary>
        public override void OnReleased()
        {
        }
    }
}
