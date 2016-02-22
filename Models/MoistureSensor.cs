using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.Foundation;

namespace BlinkyHeadlessCS
{
    public sealed class MoistureSensor
    {
        private GpioPin pinMOISTURE;
        private GpioPin pinMOISTUREpwr;

        /// <summary>
        /// Use this constructor when sensor has continous power
        /// </summary>
        /// <param name="MOISTURE_PIN_READING">GPIO Pin that sensor reading (out) is connected to</param>
        public MoistureSensor(int MOISTURE_PIN_READING)
        {
            //MOISTURE GPIO
            pinMOISTURE = GpioController.GetDefault().OpenPin(MOISTURE_PIN_READING);
            pinMOISTURE.SetDriveMode(GpioPinDriveMode.Input);            
        }

        /// <summary>
        /// Use this constructor when you would like to only power the sensor when taking a reading
        /// </summary>
        /// <param name="MOISTURE_PIN_POWER">GPIO Pin that sensor power is connected to</param>
        /// <param name="MOISTURE_PIN_READING">GPIO Pin that sensor reading (out) is connected to</param>
        public MoistureSensor(int MOISTURE_PIN_POWER, int MOISTURE_PIN_READING)
            :this(MOISTURE_PIN_READING)
        {
            //MOISTURE POWER
            pinMOISTUREpwr = GpioController.GetDefault().OpenPin(MOISTURE_PIN_POWER);
            pinMOISTUREpwr.Write(GpioPinValue.Low); //turn off the moisture sensor power pin
            pinMOISTUREpwr.SetDriveMode(GpioPinDriveMode.Output);
        }

        //Create the getReading() function as an extension method to expose async method
        //https://marcominerva.wordpress.com/2013/03/21/how-to-expose-async-methods-in-a-windows-runtime-component/
        public IAsyncOperation<int> getReading()
        {
            return getReadingHelper().AsAsyncOperation();
        }

        /// <summary>
        /// Reads Sensor
        /// </summary>
        /// <returns>int</returns>
        private async Task<int> getReadingHelper()
        {
            int reading = 1;    //default to OFF
            //send power to Moisture sensor
            pinMOISTUREpwr.Write(GpioPinValue.High);

            //get a reading
            await Task.Delay(500);  //delay reading to give sensor enough time to power
            reading = (int)pinMOISTURE.Read();
            
            //DelayOff();
            pinMOISTUREpwr.Write(GpioPinValue.Low);

            return reading;
        }
    }
}
