using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace BlinkyHeadlessCS
{
    public sealed class LedController
    {
        private GpioPin pinLEDBlue;
        private GpioPin pinLEDRed;

        /// <summary>
        /// Constructor that initializes LEDs
        /// </summary>
        /// <param name="LED_PIN_BLUE">GPIO power pin that Blue LED is connected to</param>
        /// <param name="LED_PIN_RED">GPIO power pin that Red LED is connected to</param>
        public LedController(int LED_PIN_BLUE, int LED_PIN_RED)
        {
            //Blue LED
            pinLEDBlue = GpioController.GetDefault().OpenPin(LED_PIN_BLUE);
            pinLEDBlue.Write(GpioPinValue.Low);
            pinLEDBlue.SetDriveMode(GpioPinDriveMode.Output);

            //Red LED
            pinLEDRed = GpioController.GetDefault().OpenPin(LED_PIN_RED);
            pinLEDRed.Write(GpioPinValue.Low);
            pinLEDRed.SetDriveMode(GpioPinDriveMode.Output);
        }

        /// <summary>
        /// Toggles the LEDs based on reading state
        /// </summary>
        /// <param name="isDry">integer that specifies if plant is dry</param>
        public void update(int isDry)
        {
            if (isDry == 1)  //moisture sensor reads dry
            {
                //turn on red led
                pinLEDRed.Write(GpioPinValue.High);

                //turn off blue led
                pinLEDBlue.Write(GpioPinValue.Low);
            }
            else
            {
                //turn on blue led
                pinLEDBlue.Write(GpioPinValue.High);

                //turn off red led
                pinLEDRed.Write(GpioPinValue.Low);

            }
        }
    }
}
