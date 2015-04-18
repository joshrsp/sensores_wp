using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;

namespace sensores
{
    public partial class MainPage : PhoneApplicationPage
    {
        Accelerometer accelSensor;
        Compass compassSensor;
        Gyroscope gyroSensor;
        Motion sensor;
        String  runningMessage="";
        System.Windows.Threading.DispatcherTimer timer;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            button1.Content= "Start";
            textBlock1.Text = "";
            compassX.Text = "";
            compassY.Text = "";
            compassZ.Text = "";
            heading.Text ="";
            xTextBlock.Text = "";
            yTextBlock.Text = "";
            zTextBlock.Text = "";
            textBlock2.Text = "";
            textBlock3.Text = "";
            textBlock4.Text = "";
            textBlock5.Text = "";
            gyroX.Text = "";
            gyroY.Text = "";
            gyroZ.Text = "";
            gravityX.Text = "";
            gravityY.Text = "";
            gravityZ.Text = "";
            attitudeX.Text = "";
            attitudeY.Text = "";
            attitudeZ.Text = ""; 
            /*accelerometer = new Accelerometer();
            accelerometer.ReadingChanged += new EventHandler<AccelerometerReadingEventArgs>(AccelReadingChanged);
            accelerometer.Start();*/
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(66);
            if (!Accelerometer.IsSupported)
            {
                // The device on which the application is running does not support
                // the accelerometer sensor. Alert the user and disable the
                // Start and Stop buttons.
                textBlock1.Text = "device does not support accelerometer";
                button1.IsEnabled = false;
               // stopButton.IsEnabled = false;*/
            }
            if (Accelerometer.IsSupported)
            {
                accelSensor = new Accelerometer();
                accelSensor.TimeBetweenUpdates = TimeSpan.FromMilliseconds(66);
            }
            
            if (Compass.IsSupported)
            {
                compassSensor = new Compass();
                compassSensor.TimeBetweenUpdates = TimeSpan.FromMilliseconds(66);
            }
            
            if (Gyroscope.IsSupported)
            {
                gyroSensor = new Gyroscope();
                gyroSensor.TimeBetweenUpdates = TimeSpan.FromMilliseconds(66);
            }
            
            if (Motion.IsSupported)
            {
                sensor = new Motion();
                sensor.TimeBetweenUpdates = TimeSpan.FromMilliseconds(66);
                
            }
            
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (!timer.IsEnabled)
            {
                string runningMessage = "Reading Acelerometro: ";
                textBlock2.Text = "Reading Magnetic: ";
                textBlock3.Text = "Reading Gyroscope: ";
                textBlock4.Text = "Reading Gravity: ";
                textBlock5.Text = "Reading Attitude: ";
                timer.Start();
                textBlock1.Text = runningMessage;
                
            }
            if (Accelerometer.IsSupported)
            {
                accelSensor.Start();
                runningMessage += "Accelerometer ";
            }
            if (Compass.IsSupported)
            {
                compassSensor.Start();
                
            }
            else
            {
                compassX.Text = "Magnetic no soportada";
            }
            if (Gyroscope.IsSupported)
            {
                gyroSensor.Start();
              
            }
            else
            {
                gyroX.Text = "Gyroscope no soportada";
            }
            if(Motion.IsSupported)
            {
                sensor.Start();

            }
            else
            {
                attitudeX.Text = "Attude no soportada";
                gravityX.Text = "Gravity no soportada";
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            ReadAccelerometerData();
            ReadCompassData();
            ReadGyroscopeData();
            ReadMotion();
        }
        private void ReadAccelerometerData()
        {
            if (Accelerometer.IsSupported)
            {
                AccelerometerReading reading = accelSensor.CurrentValue;
                Vector3 acceleration = reading.Acceleration;
                xTextBlock.Text = ""+acceleration.X;
                yTextBlock.Text = ""+acceleration.Y;
                zTextBlock.Text = ""+acceleration.Z;
            }
        }
        void ReadCompassData()
        {
            if (Compass.IsSupported)
            {
                CompassReading reading = compassSensor.CurrentValue;
                Vector3 magnetic = reading.MagnetometerReading;
                compassX.Text = ""+magnetic.X;
                compassY.Text = "" + magnetic.Y;
                compassZ.Text = "" + magnetic.Z;
                heading.Text = string.Format("Compass (µT) : Heading {0} +/- {1} degrees",reading.TrueHeading, reading.HeadingAccuracy);
            }
        }
      void ReadGyroscopeData()
        {
         if (Gyroscope.IsSupported)
         {
             GyroscopeReading reading = gyroSensor.CurrentValue;
             Vector3 rotation = reading.RotationRate;
             gyroX.Text = "" + rotation.X;
             gyroY.Text = "" + rotation.Y;
             gyroZ.Text = "" + rotation.Z;
         }
        }

      void ReadMotion()
      {
          if (Motion.IsSupported)
          {
              MotionReading reading = sensor.CurrentValue;
              Vector3 gravity = reading.Gravity;
              gravityX.Text = "" + gravity.X;
              gravityY.Text = "" + gravity.Y;
              gravityZ.Text = "" + gravity.Z;

              AttitudeReading attitude = reading.Attitude;
              attitudeX.Text = "" + attitude.Pitch;
              attitudeY.Text = "" + attitude.Roll;
              attitudeZ.Text = "" + attitude.Yaw; 
              
          }
      }

      
    }
}