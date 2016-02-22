// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Windows.Devices.Gpio;
using Windows.System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BlinkyHeadlessCS
{
    public sealed class StartupTask : IBackgroundTask
    {
        BackgroundTaskDeferral deferral;
        private const int LED_PIN_BLUE = 19;
        private const int LED_PIN_RED = 13;
        private const int MOISTURE_PIN_READING = 6;
        private const int MOISTURE_PIN_POWER = 5;

        //private GpioPin pinLED;
        private LedController LEDs;
        private MoistureSensor MoistureSensor;

        //private ThreadPoolTimer timerLED;
        private ThreadPoolTimer timerMOISTURE;
        private int MoistureReading = 1;

        private int timePeriod = 3000;  //takes a reading every x seconds

        //TODO
        /*
        - Obtain analog moisture reading (Digital is not good enough. need high and low moisture thresholds)
        - Send periodic reading to web service that will store all sensor readings
        - Create Solenoid valve controller
        - Set low threshold to start watering
        - When watering continuously take readings until high threshold is met (in a "while" loop) then turn off valve
        - Program in a safety function to catch issues and stop watering (eg: if watering for x seconds, stop and place device in error state)
        - Send email (via Mandrill?) when watering starts and ends
        - Create API that allows external device (Alexa?) to send command to start watering
        - Check time of day and weather prior to watering

        */
        
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();
            Init();
        }

        private void Init()
        {
            LEDs = new LedController(LED_PIN_BLUE, LED_PIN_RED);
            MoistureSensor = new MoistureSensor(MOISTURE_PIN_POWER, MOISTURE_PIN_READING);
            timerMOISTURE = ThreadPoolTimer.CreatePeriodicTimer(GetMoistureReading, TimeSpan.FromMilliseconds(timePeriod));
        }

        private async void GetMoistureReading(ThreadPoolTimer timer)
        {
            MoistureReading = await MoistureSensor.getReading();
            
            //Blue (watered) or Red (Needs Water) LED?
            LEDs.update(MoistureReading);

            //log the reading
            Debug.WriteLine(DateTime.Now.ToString() + ": " + MoistureReading.ToString());
            
        }
        
    }
}
