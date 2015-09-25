using System;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestAoo1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public const int pinCount = 16;

        public const byte PCA9685_REG_MODE1 = 0x0;
        public const byte PCA9685_REG_MODE2 = 0x1;

        public const byte PCA9685_REG_LED0_ON_L = 0x06;
        public const byte PCA9685_REG_LED0_ON_H = 0x07;
        public const byte PCA9685_REG_LED0_OFF_L = 0x08;
        public const byte PCA9685_REG_LED0_OFF_H = 0x09;

        public const byte PCA9685_REG_LED1_ON_L = 0x0A;
        public const byte PCA9685_REG_LED1_ON_H = 0x0B;
        public const byte PCA9685_REG_LED1_OFF_L = 0x0C;
        public const byte PCA9685_REG_LED1_OFF_H = 0x0D;

        public const byte PCA9685_REG_LED2_ON_L = 0x0E;
        public const byte PCA9685_REG_LED2_ON_H = 0x0F;
        public const byte PCA9685_REG_LED2_OFF_L = 0x10;
        public const byte PCA9685_REG_LED2_OFF_H = 0x11;

        public const byte PCA9685_REG_LED3_ON_L = 0x12;
        public const byte PCA9685_REG_LED3_ON_H = 0x13;
        public const byte PCA9685_REG_LED3_OFF_L = 0x14;
        public const byte PCA9685_REG_LED3_OFF_H = 0x15;

        public const byte PCA9685_REG_LED4_ON_L = 0x16;
        public const byte PCA9685_REG_LED4_ON_H = 0x17;
        public const byte PCA9685_REG_LED4_OFF_L = 0x18;
        public const byte PCA9685_REG_LED4_OFF_H = 0x19;

        public const byte PCA9685_REG_LED5_ON_L = 0x1A;
        public const byte PCA9685_REG_LED5_ON_H = 0x1B;
        public const byte PCA9685_REG_LED5_OFF_L = 0x1C;
        public const byte PCA9685_REG_LED5_OFF_H = 0x1D;

        public const byte PCA9685_REG_LED6_ON_L = 0x1E;
        public const byte PCA9685_REG_LED6_ON_H = 0x1F;
        public const byte PCA9685_REG_LED6_OFF_L = 0x20;
        public const byte PCA9685_REG_LED6_OFF_H = 0x21;

        public const byte PCA9685_REG_LED7_ON_L = 0x22;
        public const byte PCA9685_REG_LED7_ON_H = 0x23;
        public const byte PCA9685_REG_LED7_OFF_L = 0x24;
        public const byte PCA9685_REG_LED7_OFF_H = 0x25;

        public const byte PCA9685_REG_LED8_ON_L = 0x26;
        public const byte PCA9685_REG_LED8_ON_H = 0x27;
        public const byte PCA9685_REG_LED8_OFF_L = 0x28;
        public const byte PCA9685_REG_LED8_OFF_H = 0x29;

        public const byte PCA9685_REG_LED9_ON_L = 0x2A;
        public const byte PCA9685_REG_LED9_ON_H = 0x2B;
        public const byte PCA9685_REG_LED9_OFF_L = 0x2C;
        public const byte PCA9685_REG_LED9_OFF_H = 0x2D;

        public const byte PCA9685_REG_LED10_ON_L = 0x2E;
        public const byte PCA9685_REG_LED10_ON_H = 0x2F;
        public const byte PCA9685_REG_LED10_OFF_L = 0x30;
        public const byte PCA9685_REG_LED10_OFF_H = 0x31;

        public const byte PCA9685_REG_LED11_ON_L = 0x32;
        public const byte PCA9685_REG_LED11_ON_H = 0x33;
        public const byte PCA9685_REG_LED11_OFF_L = 0x34;
        public const byte PCA9685_REG_LED11_OFF_H = 0x35;

        public const byte PCA9685_REG_LED12_ON_L = 0x36;
        public const byte PCA9685_REG_LED12_ON_H = 0x37;
        public const byte PCA9685_REG_LED12_OFF_L = 0x38;
        public const byte PCA9685_REG_LED12_OFF_H = 0x39;

        public const byte PCA9685_REG_LED13_ON_L = 0x3A;
        public const byte PCA9685_REG_LED13_ON_H = 0x3B;
        public const byte PCA9685_REG_LED13_OFF_L = 0x3C;
        public const byte PCA9685_REG_LED13_OFF_H = 0x3D;

        public const byte PCA9685_REG_LED14_ON_L = 0x3E;
        public const byte PCA9685_REG_LED14_ON_H = 0x3F;
        public const byte PCA9685_REG_LED14_OFF_L = 0x40;
        public const byte PCA9685_REG_LED14_OFF_H = 0x41;

        public const byte PCA9685_REG_LED15_ON_L = 0x42;
        public const byte PCA9685_REG_LED15_ON_H = 0x43;
        public const byte PCA9685_REG_LED15_OFF_L = 0x44;
        public const byte PCA9685_REG_LED15_OFF_H = 0x45;

        public const byte PCA9685_REG_ALL_ON_L = 0xFA;
        public const byte PCA9685_REG_ALL_ON_H = 0xFB;
        public const byte PCA9685_REG_ALL_OFF_L = 0xFC;
        public const byte PCA9685_REG_ALL_OFF_H = 0xFD;

        public const byte PCA9685_REG_PRESCALE = 0xFE;

        public const byte __RESTART = 0x80; 
        public const byte __SLEEP   = 0x10; 
        public const byte __ALLCALL = 0x01; 
        public const byte __INVRT   = 0x10; 
        public const byte __OUTDRV  = 0x04; 


        public const string I2C_CONTROLLER_NAME = "I2C1";

        public const int controllerI2CSlaveAddress = 0x40;
        public const int topControllerI2CSlaveAddress = 0x41;

        public const int resetControllerI2CSlaveAddress = 0x0;

        public const short minFrequency = 40;
        public const short maxFrequency = 1000;
        public const long clockFrequency = 25000000L;

        public const int pulseResolution = 4096;
        public const Byte defaultPrescale = 0x1E;

        public I2cDevice bottomController;
        public I2cDevice topController;
        public I2cDevice resetController;

        public byte[,] PwmPinRegs = new byte[16, 4]
        {
            {PCA9685_REG_LED0_ON_L,   PCA9685_REG_LED0_ON_H,  PCA9685_REG_LED0_OFF_L,  PCA9685_REG_LED0_OFF_H  },
            { PCA9685_REG_LED1_ON_L,  PCA9685_REG_LED1_ON_H,  PCA9685_REG_LED1_OFF_L,  PCA9685_REG_LED1_OFF_H  },
            { PCA9685_REG_LED2_ON_L,  PCA9685_REG_LED2_ON_H,  PCA9685_REG_LED2_OFF_L,  PCA9685_REG_LED2_OFF_H  },
            { PCA9685_REG_LED3_ON_L,  PCA9685_REG_LED3_ON_H,  PCA9685_REG_LED3_OFF_L,  PCA9685_REG_LED3_OFF_H  },
            { PCA9685_REG_LED4_ON_L,  PCA9685_REG_LED4_ON_H,  PCA9685_REG_LED4_OFF_L,  PCA9685_REG_LED4_OFF_H  },
            { PCA9685_REG_LED5_ON_L,  PCA9685_REG_LED5_ON_H,  PCA9685_REG_LED5_OFF_L,  PCA9685_REG_LED5_OFF_H  },
            { PCA9685_REG_LED6_ON_L,  PCA9685_REG_LED6_ON_H,  PCA9685_REG_LED6_OFF_L,  PCA9685_REG_LED6_OFF_H  },
            { PCA9685_REG_LED7_ON_L,  PCA9685_REG_LED7_ON_H,  PCA9685_REG_LED7_OFF_L,  PCA9685_REG_LED7_OFF_H  },
            { PCA9685_REG_LED8_ON_L,  PCA9685_REG_LED8_ON_H,  PCA9685_REG_LED8_OFF_L,  PCA9685_REG_LED8_OFF_H  },
            { PCA9685_REG_LED9_ON_L,  PCA9685_REG_LED9_ON_H,  PCA9685_REG_LED9_OFF_L,  PCA9685_REG_LED9_OFF_H  },
            { PCA9685_REG_LED10_ON_L, PCA9685_REG_LED10_ON_H, PCA9685_REG_LED10_OFF_L, PCA9685_REG_LED10_OFF_H },
            { PCA9685_REG_LED11_ON_L, PCA9685_REG_LED11_ON_H, PCA9685_REG_LED11_OFF_L, PCA9685_REG_LED11_OFF_H },
            { PCA9685_REG_LED12_ON_L, PCA9685_REG_LED12_ON_H, PCA9685_REG_LED12_OFF_L, PCA9685_REG_LED12_OFF_H },
            { PCA9685_REG_LED13_ON_L, PCA9685_REG_LED13_ON_H, PCA9685_REG_LED13_OFF_L, PCA9685_REG_LED13_OFF_H },
            { PCA9685_REG_LED14_ON_L, PCA9685_REG_LED14_ON_H, PCA9685_REG_LED14_OFF_L, PCA9685_REG_LED14_OFF_H },
            { PCA9685_REG_LED15_ON_L, PCA9685_REG_LED15_ON_H, PCA9685_REG_LED15_OFF_L, PCA9685_REG_LED15_OFF_H }
        };


        //private double actualFrequency;
        //private Byte preScale;
        private bool[] topPinAcquired = new bool[pinCount];
        private bool[] bottomPinAcquired = new bool[pinCount];

        I2cDevice i2ctopboard;
        I2cDevice i2cbottomboard;

        //PwmController pwmController;

        public MainPage()
        {
            this.InitializeComponent();

            InitializeI2Cstem("I2C1", (byte)0x40, (byte)0x41);
        }


        private async void InitializeI2Cstem(string I2C_CONTROLLER_NAME, byte SERVO_PWM_BOARD1, byte SERVO_PWM_BOARD2)
        {
            // initialize I2C communications
            try
            {
                string deviceSelector = I2cDevice.GetDeviceSelector(I2C_CONTROLLER_NAME);
                var i2cDeviceControllers = await DeviceInformation.FindAllAsync(deviceSelector);

                //INITIALIZE I2C CONNECTION COMMUNICATION FOR PWM HAT (BOTTOM BOARD)
                var servoControlBoardBottom = new I2cConnectionSettings(SERVO_PWM_BOARD1);
                servoControlBoardBottom.BusSpeed = I2cBusSpeed.StandardMode;
                servoControlBoardBottom.SharingMode = I2cSharingMode.Shared;
                i2cbottomboard = await I2cDevice.FromIdAsync(i2cDeviceControllers[0].Id, servoControlBoardBottom);

                setPWMFreq(i2cbottomboard, 60);

                setPWM(i2cbottomboard, 0, 0, 0);
                setPWM(i2cbottomboard, 0, 0, 160);
                setPWM(i2cbottomboard, 0, 600, 0);
                
                //setPWM(i2cbottomboard, 0, 20, 100);
                //setPWM(i2cbottomboard, 0, 1, 250);

                //60% on and 40% off
                //setPWM(i2cbottomboard, 0, 2457, 1638);

                //55% on 45% off (NO)
                //setPWM(i2cbottomboard, 0, 2252, 1843);

                //35% on 65% off
                //setPWM(i2cbottomboard, 0, 1433, 2662);

                //30% on 70% off (NO)
                //setPWM(i2cbottomboard, 0, 1228, 2867);


                //47% on 53% off
                //setPWM(i2cbottomboard, 0, 1925, 2170);
                
                //46% on 54% off
                setPWM(i2cbottomboard, 0, 1884, 2211);
                
                //45% on 55% off
                setPWM(i2cbottomboard, 0, 1843, 2252);
                
                //44% on 56% off
                setPWM(i2cbottomboard, 0, 1802, 2293);
                
                //43% on 57% off
                setPWM(i2cbottomboard, 0, 1761, 2334);
                
                //42% on 58% off
                //setPWM(i2cbottomboard, 0, 1720, 2375);
                
                //41% on 59% off
                //setPWM(i2cbottomboard, 0, 1679, 2416);

                //40% on 60% off (NO)
                //setPWM(i2cbottomboard, 0, 1638, 2457);

                
                
                ////INITIALIZE I2C CONNECTION COMMUNICATION FOR PWM HAT (TOP BOARD)
                var servoControlBoardTop = new I2cConnectionSettings(SERVO_PWM_BOARD2);
                servoControlBoardTop.BusSpeed = I2cBusSpeed.FastMode;
                servoControlBoardTop.SharingMode = I2cSharingMode.Shared;
                i2ctopboard = await I2cDevice.FromIdAsync(i2cDeviceControllers[0].Id, servoControlBoardTop);
                
                setPWM(i2ctopboard, 0, 1638, 2457);

                //setServoPulse(i2cbottomboard, 0, 1);
                //setPWMFreq(i2cbottomboard, 5);
                //setServoPulse(i2ctopboard, 0, 1);
                //setPWMFreq(i2ctopboard, 5);

                //while (true)
                //{
                    //setPWM(i2cbottomboard, 0, 20, 100);
                    //setPWM(i2ctopboard, 0, 20, 100);
                    //setPWM(i2cbottomboard, 0, 1, 250);
                    //setPWM(i2ctopboard, 0, 1, 250)  
                //}


            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: {0}", e.Message);
                return;
            }
        }

        public void setServoPulse(I2cDevice board, int channel, float pulse)
        {
            float pulselength = 1000000;
            pulselength /= 60;
            pulselength /= 4096;
            pulse *= 1000;

            pulse /= pulselength;
            setPWM(board, channel, pulse, 0);
        }

        public void initializeServo(I2cDevice board)
        {
            setAllPWM(board, 0, 0);
            board.Write(new byte[] { PCA9685_REG_MODE2, __OUTDRV});
            board.Write(new byte[] { PCA9685_REG_MODE1, __ALLCALL });
            var mode1 = board.ReadPartial(new byte[] { PCA9685_REG_MODE1});
        }

        public void setPWMFreq(I2cDevice board, double freq)
        {
            double preScale = 25000000.0;
            preScale /= 4096;
            preScale /= (float)freq;
            preScale -= 1.0;

            preScale = Math.Floor(preScale + .5);
            var oldMode = board.ReadPartial(new byte[] { (PCA9685_REG_MODE1) });
            var newMode = (oldMode.BytesTransferred & 0x7F) | 0x10;
            board.Write(new byte[] { PCA9685_REG_MODE1, (byte)(newMode) });
            board.Write(new byte[] { PCA9685_REG_PRESCALE, (byte)(Math.Floor(preScale)) });
            board.Write(new byte[] { PCA9685_REG_MODE1, (byte)(oldMode.BytesTransferred) });
            board.Write(new byte[] { PCA9685_REG_MODE1, (byte)(oldMode.BytesTransferred | 0x80)});

        }

        public void setPWM(I2cDevice board, int channel, float on, float off)
        {
            board.Write(new byte[] { (byte)(PCA9685_REG_LED0_ON_L + 4 * channel), (byte)((byte)on & 0xFF)});
            board.Write(new byte[] { (byte)(PCA9685_REG_LED0_ON_H + 4 * channel), (byte)((byte)on >> 8) });
            board.Write(new byte[] { (byte)(PCA9685_REG_LED0_OFF_L + 4 * channel), (byte)((byte)off & 0xFF)});
            board.Write(new byte[] { (byte)(PCA9685_REG_LED0_OFF_H + 4 * channel), (byte)((byte)off >> 8) });
        }

        public void setAllPWM(I2cDevice board, int on, int off)
        {
            board.Write(new byte[] { PCA9685_REG_ALL_ON_L,  (byte)((byte)on & 0xFF )});
            board.Write(new byte[] { PCA9685_REG_ALL_ON_H,  (byte)((byte)on >> 8) });
            board.Write(new byte[] { PCA9685_REG_ALL_OFF_L, (byte)((byte)off & 0xFF) });
            board.Write(new byte[] { PCA9685_REG_ALL_OFF_H, (byte)((byte)off >> 8) });
        }
    }
}