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
        UIComponent uiComponent;
        UIView v;
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
            if (mode == LoadMode.LoadMap || mode == LoadMode.LoadAsset || mode == LoadMode.NewAsset || mode == LoadMode.NewMap)
                return;

            try
            {
                v = UIView.GetAView();
                uiComponent = (UIComponent)v.AddUIComponent(typeof(ConfigurationButton)) ?? v.AddUIComponent(typeof(ConfigurationButton)) as ConfigurationButton;
            }
            catch(Exception ex)
            {
                if (ELESDEMod.IsDebug) Log.Error("OnLevelLoaded Error: " + ex.ToString());
            }
        }

        /// <summary>
        /// Thread: Main
        /// Invoked when the level is unloading (typically when going back to the main menu
        /// or prior to loading a new level)
        /// </summary>
        public override void OnLevelUnloading()
        {
            try
            {
                if (uiComponent != null)
                    UnityEngine.Object.Destroy(uiComponent);
            }
            catch (Exception ex)
            {
                if (ELESDEMod.IsDebug) Log.Error("Trying to release. " + ex.Message);
            }
        }

        /// <summary>
        /// Thread: Main
        /// Invoked when the extension deinitializes.
        /// </summary>
        public override void OnReleased()
        {
            try
            {
                if (uiComponent != null)
                    UnityEngine.Object.Destroy(uiComponent);
            }
            catch(Exception ex)
            {
                if (ELESDEMod.IsDebug) Log.Error("Trying to release. " + ex.Message);
            }
        }
    }
}
