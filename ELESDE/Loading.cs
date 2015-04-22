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

            UIView v = UIView.GetAView();

            uiComponent = (UIComponent)v.AddUIComponent(typeof(ConfigurationButton));
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
