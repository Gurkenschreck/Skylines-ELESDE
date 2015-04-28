using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ELESDE
{

    public class LightEffectManager
    {
        //Fields
        #region Fields and Objects
        Light light;
        Color color;
        Thread effectThread;
        private float cyclesPerSecond = 15;
        float rMax = 1f;
        float gMax = 1f;
        float bMax = 1f;
        float aMax = 1f;
        float rMin = 0f;
        float gMin = 0f;
        float bMin = 0f;
        float aMin = 0f;
        float rOriginal;
        float gOriginal;
        float bOriginal;
        float aOriginal;
        float rStep = 0.023f;
        float gStep = 0.031f;
        float bStep = 0.016f;
        float aStep = 0.006f;
        bool rUp = true;
        bool gUp = true;
        bool bUp = true;
        bool aUp = true;
        bool stopAllEffects = false;
        #endregion

        //Properties
        public Light Light
        {
            get { return light; }
            set { light = value; }
        }
        public Color ColorLight
        {
            get { return light.color; }
            set { light.color = color; }
        }
        public Color Color
        {
            get { return light.color; }
            set { color = value; }
        }
        public Thread EffectThread 
        {
            get { return effectThread; } 
        }
        public bool IsThreadRunning 
        { 
            get 
            {
                if (EffectThread != null)
                    if (EffectThread.IsAlive)
                        return true;

                return false;
            } 
        }
        public bool StopAllEffects
        {
            get { return stopAllEffects; }
            set { stopAllEffects = value; }
        }
        public float CyclesPerSecond
        {
            get { return cyclesPerSecond; }
            set { cyclesPerSecond = value; }
        }
        public float RStep
        {
            get { return rStep; }
            set { rStep = value; }
        }
        public float GStep
        {
            get { return gStep; }
            set { gStep = value; }
        }
        public float BStep
        {
            get { return bStep; }
            set { bStep = value; }
        }
        public float AStep
        {
            get { return aStep; }
            set { aStep = value; }
        }


        //Constructor
        public LightEffectManager()
            : this(new Light())
        {  }

        public LightEffectManager(Light light)
        {
            this.light = light;
            color = new Color(light.color.a, light.color.g, light.color.b, light.color.a);
            rOriginal = color.r;
            gOriginal = color.g;
            bOriginal = color.b;
            aOriginal = color.a;
        }

        public LightEffectManager(ref Light light)
        {
            this.light = light;
            color = new Color(light.color.a, light.color.g, light.color.b, light.color.a);
            rOriginal = color.r;
            gOriginal = color.g;
            bOriginal = color.b;
            aOriginal = color.a;
        }

        //Methods
        /// <summary>
        /// Setss the colors to the native values.
        /// </summary>
        public void ResetColors()
        {
            light.color = new Color(rOriginal, gOriginal, bOriginal, aOriginal);
        }

        /// <summary>
        /// Pauses the thread in which the effects are handled.
        /// </summary>
        public void PauseEffects()
        {
            if (effectThread != null)
                if (effectThread.IsAlive)
                    effectThread.Interrupt();
        }

        /// <summary>
        /// Resets the effects and the colors.
        /// </summary>
        public void Reset()
        {
            try
            {
                StopAllEffects = true;
                ResetColors();

                //if (effectThread != null)
                //    if (effectThread.IsAlive)
                //        effectThread.Interrupt();
            }
            catch (Exception ex)
            {
                if (ELESDEMod.IsDebug) Log.Error("Exception in Reset: " + ex.Message);
            }
        }

        /// <summary>
        /// Sets the current steps of the colors.
        /// </summary>
        /// <param name="rStep">How the r value should change.</param>
        /// <param name="gStep">How the g value should change.</param>
        /// <param name="bStep">How the b value should change.</param>
        /// <param name="aStep">How the a value should change.</param>
        public void SetSteps(float rStep, float gStep, float bStep, float aStep)
        {
            this.RStep = rStep;
            this.GStep = gStep;
            this.BStep = bStep;
            this.AStep = aStep;
        }

        /// <summary>
        /// Fades the color of the light based on the cycles per second and the min and max values of the colors.
        /// </summary>
        public void FadeColor()
        {
            try
            {
                StopAllEffects = false;
                while (!StopAllEffects)
                {
                    if (rUp)
                    {
                        if ((color.r + RStep) <= rMax)
                            color.r += RStep;
                        else
                            rUp = false;
                    }
                    else
                    {
                        if ((color.r - RStep) >= rMin)
                            color.r -= RStep;
                        else
                            rUp = true;
                    }
                    if (gUp)
                    {
                        if ((color.g + GStep) <= gMax)
                            color.g += GStep;
                        else
                            gUp = false;
                    }
                    else
                    {
                        if ((color.g - GStep) >= gMin)
                            color.g -= GStep;
                        else
                            gUp = true;
                    }
                    if (bUp)
                    {
                        if ((color.b + BStep) <= bMax)
                            color.b += BStep;
                        else
                            bUp = false;
                    }
                    else
                    {
                        if ((color.b - BStep) >= bMin)
                            color.b -= BStep;
                        else
                            bUp = true;
                    }
                    if (aUp)
                    {
                        if ((color.a + AStep) <= aMax)
                            color.a += AStep;
                        else
                            aUp = false;
                    }
                    else
                    {
                        if ((color.a - AStep) >= aMin)
                            color.a -= AStep;
                        else
                            aUp = true;
                    }
                    light.color = color;
                    Thread.Sleep(TimeSpan.FromMilliseconds(1000 / CyclesPerSecond));
                }
            }
            catch (Exception ex)
            {
                if (ELESDEMod.IsDebug) Log.Error("Error FadeColor(): " + ex.ToString());
            }
        }

        /// <summary>
        /// Launches FadeColor in a separate thread to execute.
        /// </summary>
        /// <returns>Optional: The thread in which the FadeColor method is executed.</returns>
        public void FadeColorInThread()
        {
            effectThread = new Thread(FadeColor);
            effectThread.Start();
        }

        /// <summary>
        /// Fades the color of the light based on the cycles per second (x2) and the min and max values (/2) of the colors.
        /// </summary>
        public void FadeColorSmooth()
        {
            try
            {
                StopAllEffects = false;
                while (!StopAllEffects)
                {
                    if (rUp)
                    {
                        if ((color.r + (RStep / 2)) <= rMax)
                            color.r += RStep / 2;
                        else
                            rUp = false;
                    }
                    else
                    {
                        if ((color.r - (RStep / 2)) >= rMin)
                            color.r -= RStep / 2;
                        else
                            rUp = true;
                    }
                    if (gUp)
                    {
                        if ((color.g + (GStep / 2)) <= gMax)
                            color.g += GStep / 2;
                        else
                            gUp = false;
                    }
                    else
                    {
                        if ((color.g - (GStep / 2)) >= gMin)
                            color.g -= GStep / 2;
                        else
                            gUp = true;
                    }
                    if (bUp)
                    {
                        if ((color.b + (BStep / 2)) <= bMax)
                            color.b += BStep / 2;
                        else
                            bUp = false;
                    }
                    else
                    {
                        if ((color.b - (BStep / 2)) >= bMin)
                            color.b -= BStep / 2;
                        else
                            bUp = true;
                    }
                    if (aUp)
                    {
                        if ((color.a + (AStep / 2)) <= aMax)
                            color.a += AStep / 2;
                        else
                            aUp = false;
                    }
                    else
                    {
                        if ((color.a - (AStep / 2)) >= aMin)
                            color.a -= AStep / 2;
                        else
                            aUp = true;
                    }
                    light.color = color;
                    Thread.Sleep(TimeSpan.FromMilliseconds((1000 / CyclesPerSecond) * 2));
                }
            }
            catch (Exception ex)
            {
                if (ELESDEMod.IsDebug) Log.Error("Error FadeColor(): " + ex.ToString());
            }
        }

        /// <summary>
        /// Launches FadeColorSmooth in a separate thread to execute.
        /// </summary>
        /// <returns>Optional: The thread in which the FadeColorSmooth method is executed.</returns>
        public void FadeColorSmoothInThread()
        {
            effectThread = new Thread(FadeColorSmooth);
            effectThread.Start();
        }

        /// <summary>
        /// Randomizes the color values.
        /// </summary>
        public void FlipShit(object obj)
        {
            bool flipHard = (bool)obj;
            try
            {
                System.Random rdm = new System.Random();
                float r;
                float g;
                float b;
                float a;
                StopAllEffects = false;
                while (!StopAllEffects)
                {
                    r = (float)rdm.NextDouble();
                    g = (float)rdm.NextDouble();
                    b = (float)rdm.NextDouble();
                    a = (float)rdm.NextDouble();
                    if (rUp)
                    {
                        if ((color.r + r) <= rMax)
                            color.r += r;
                        else
                            rUp = false;
                    }
                    else
                    {
                        if ((color.r - r) >= rMin)
                            color.r -= r;
                        else
                            rUp = true;
                    }
                    if (gUp)
                    {
                        if ((color.g + g) <= gMax)
                            color.g += g;
                        else
                            gUp = false;
                    }
                    else
                    {
                        if ((color.g - g) >= gMin)
                            color.g -= g;
                        else
                            gUp = true;
                    }
                    if (bUp)
                    {
                        if ((color.b + b) <= bMax)
                            color.b += b;
                        else
                            bUp = false;
                    }
                    else
                    {
                        if ((color.b - b) >= bMin)
                            color.b -= b;
                        else
                            bUp = true;
                    }
                    if (aUp)
                    {
                        if ((color.a + a) <= aMax)
                            color.a += a;
                        else
                            aUp = false;
                    }
                    else
                    {
                        if ((color.a - a) >= aMin)
                            color.a -= a;
                        else
                            aUp = true;
                    }
                    light.color = color;
                    if (flipHard)
                        Thread.Sleep(TimeSpan.FromMilliseconds(1000 / (CyclesPerSecond * 2)));
                    else
                        Thread.Sleep(TimeSpan.FromMilliseconds(1000 / CyclesPerSecond));
                }
            }
            catch (Exception ex)
            {
                if (ELESDEMod.IsDebug) Log.Error("Error FadeColor(): " + ex.ToString());
            }
        }

        /// <summary>
        /// Launches FlipShit in a separate thread to execute.
        /// </summary>
        /// <returns>Optional: THe thread in which the FlipShit method is executed.</returns>
        public void FlipShitInThread()
        {
            ParameterizedThreadStart pts = new ParameterizedThreadStart(this.FlipShit);

            effectThread = new Thread(pts);
            effectThread.Start(false);
        }
        public void FlipShitHardInThread()
        {
            ParameterizedThreadStart pts = new ParameterizedThreadStart(this.FlipShit);

            effectThread = new Thread(pts);
            effectThread.Start(true);
        }
    }
}
