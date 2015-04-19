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
        Light light;
        Color color;
        public float cyclesPerSecond = 10;
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

        //Constructor
        public LightEffectManager(ref Light light)
        {
            this.light = light;
            color = new Color(light.color.a, light.color.g, light.color.b, light.color.a);
        }

        //Methods
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
                        if ((color.r + rStep) <= rMax)
                            color.r += rStep;
                        else
                            rUp = false;
                    }
                    else
                    {
                        if ((color.r - rStep) >= rMin)
                            color.r -= rStep;
                        else
                            rUp = true;
                    }
                    if (gUp)
                    {
                        if ((color.g + gStep) <= gMax)
                            color.g += gStep;
                        else
                            gUp = false;
                    }
                    else
                    {
                        if ((color.g - gStep) >= gMin)
                            color.g -= gStep;
                        else
                            gUp = true;
                    }
                    if (bUp)
                    {
                        if ((color.b + bStep) <= bMax)
                            color.b += bStep;
                        else
                            bUp = false;
                    }
                    else
                    {
                        if ((color.b - bStep) >= bMin)
                            color.b -= bStep;
                        else
                            bUp = true;
                    }
                    if (aUp)
                    {
                        if ((color.a + aStep) <= aMax)
                            color.a += aStep;
                        else
                            aUp = false;
                    }
                    else
                    {
                        if ((color.a - aStep) >= aMin)
                            color.a -= aStep;
                        else
                            aUp = true;
                    }
                    light.color = color;
                    Thread.Sleep(TimeSpan.FromMilliseconds(1000 / cyclesPerSecond));
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
                        if ((color.r + (rStep / 2)) <= rMax)
                            color.r += rStep / 2;
                        else
                            rUp = false;
                    }
                    else
                    {
                        if ((color.r - (rStep / 2)) >= rMin)
                            color.r -= rStep / 2;
                        else
                            rUp = true;
                    }
                    if (gUp)
                    {
                        if ((color.g + (gStep / 2)) <= gMax)
                            color.g += gStep / 2;
                        else
                            gUp = false;
                    }
                    else
                    {
                        if ((color.g - (gStep / 2)) >= gMin)
                            color.g -= gStep / 2;
                        else
                            gUp = true;
                    }
                    if (bUp)
                    {
                        if ((color.b + (bStep / 2)) <= bMax)
                            color.b += bStep / 2;
                        else
                            bUp = false;
                    }
                    else
                    {
                        if ((color.b - (bStep / 2)) >= bMin)
                            color.b -= bStep / 2;
                        else
                            bUp = true;
                    }
                    if (aUp)
                    {
                        if ((color.a + (aStep / 2)) <= aMax)
                            color.a += aStep / 2;
                        else
                            aUp = false;
                    }
                    else
                    {
                        if ((color.a - (aStep / 2)) >= aMin)
                            color.a -= aStep / 2;
                        else
                            aUp = true;
                    }
                    light.color = color;
                    Thread.Sleep(TimeSpan.FromMilliseconds((1000 / cyclesPerSecond) * 2));
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
                    Thread.Sleep(TimeSpan.FromMilliseconds(1000 / cyclesPerSecond));
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
