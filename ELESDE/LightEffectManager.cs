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
        private float cyclesPerSecond = 10;
        float rMax = 1f;
        float gMax = 1f;
        float bMax = 1f;
        float aMax = 1f;
        float rMin = 0f;
        float gMin = 0f;
        float bMin = 0f;
        float aMin = 0f;
        float rStep = 0.020f;
        float gStep = 0.029f;
        float bStep = 0.016f;
        float aStep = 0.004f;
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
        public LightEffectManager(ref Light light)
        {
            this.light = light;
            color = new Color(light.color.a, light.color.g, light.color.b, light.color.a);
        }

        //Methods
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
                Log.Message("FadeColor");
                StopAllEffects = true;
                while (StopAllEffects)
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
                Log.Error("Error FadeColor(): " + ex.ToString());
            }
        }

        /// <summary>
        /// Launches FadeColor in a separate thread to execute.
        /// </summary>
        /// <returns>Optional: The thread in which the FadeColor method is executed.</returns>
        public Thread FadeColorAsync()
        {
            Log.Message("FadeColorAsync");
            Thread thread = new Thread(FadeColor);
            thread.Start();
            return thread;
        }

        /// <summary>
        /// Fades the color of the light based on the cycles per second (x2) and the min and max values (/2) of the colors.
        /// </summary>
        public void FadeColorSmooth()
        {
            try
            {
                Log.Message("FadeColor");
                StopAllEffects = true;
                while (StopAllEffects)
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
                Log.Error("Error FadeColor(): " + ex.ToString());
            }
        }

        /// <summary>
        /// Launches FadeColorSmooth in a separate thread to execute.
        /// </summary>
        /// <returns>Optional: The thread in which the FadeColorSmooth method is executed.</returns>
        public Thread FadeColorSmoothAsync()
        {
            Log.Message("FadeColorSmoothAsync");
            Thread thread = new Thread(FadeColorSmooth);
            thread.Start();
            return thread;
        }

        /// <summary>
        /// Randomizes the color values.
        /// </summary>
        public void FlipShit()
        {
            try
            {
                Log.Message("FlipShit");
                System.Random rdm = new System.Random();
                float r;
                float g;
                float b;
                float a;
                StopAllEffects = true;
                while (StopAllEffects)
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
                    Thread.Sleep(TimeSpan.FromMilliseconds(1000 / CyclesPerSecond));
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error FadeColor(): " + ex.ToString());
            }
        }
        /// <summary>
        /// Launches FlipShit in a separate thread to execute.
        /// </summary>
        /// <returns>Optional: THe thread in which the FlipShit method is executed.</returns>
        public Thread FlipShitAsync()
        {
            Log.Message("FlipShitAsync");
            Thread thread = new Thread(FlipShit);
            thread.Start();
            return thread;
        }
    }
}
