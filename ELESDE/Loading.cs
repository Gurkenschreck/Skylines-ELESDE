using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ELESDE
{
    class Loading : LoadingExtensionBase
    {
        //Fields
        private static Thread manageVisualsThread;

        //Properties
        public static Thread ManageVisualsThread
        {
            get { return Loading.manageVisualsThread; }
            set { Loading.manageVisualsThread = value; }
        }

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
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "abc");

            Log.Message("Starting Thread.");
            manageVisualsThread = new Thread(ManageVisuals);
            manageVisualsThread.Start();
            Log.Message("Stopping Thread.");
        }

        void ManageVisuals()
        {
            bool goOn = true;


            Light[] lightArray = Light.GetLights(LightType.Directional, 0);
            Light mainLight = lightArray[0];
            foreach (Light light in lightArray)
            {
                Log.Message(String.Format("Light Name: {0} and Tag: {1}", light.name, light.tag));

            }
            mainLight.color = new Color(0f, 1f, 0f, 0.1f);

            while (goOn)
            {
                try
                {
                    Log.Message(String.Format("Light Name: {0} and Tag: {1} and Color: {2}", lightArray[0].name, lightArray[0].tag, lightArray[0].color));
                    Thread.Sleep(2000);
                }
                catch (Exception ex)
                {
                    Log.Error("Error in SepThread. Message: " + ex.Message);
                }
            }
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
