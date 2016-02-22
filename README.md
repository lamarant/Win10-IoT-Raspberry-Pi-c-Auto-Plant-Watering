# Windows 10 IoT Raspberry PI Plant Watering System - Visual Studio 2015 with C Sharp

System currently includes a moisture sensor to take periodic moisture readings (currently digital only) and Red and Blue LEDs to indicate Dry/Wet soil.

Future features will include:
- Obtain analog moisture reading (Digital is not good enough. need high and low moisture thresholds)
- Display current moisture reading in LCD display
- Send periodic reading to web service that will store all sensor readings
- Solenoid valve to turn on/off water source
- Set low threshold to start watering
- When watering continuously take readings until high threshold is met (in a "while" loop) then turn off valve
- Build in a safety mechanism to stop watering (eg: if watering for x seconds, stop and place device in error state)
- Send email (via Mandrill?) when watering starts and ends
- Create API that allows external device (Amazon Alexa maybe?) to send command to start watering, get status, etc.
- Check time of day and weather forecast prior to watering (eg: don't water midday or if significant rain is forecasted)
