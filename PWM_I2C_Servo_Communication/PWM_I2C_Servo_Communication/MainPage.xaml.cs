using System;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PWM_I2C_Servo_Communication
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
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
        public const byte __SLEEP = 0x10;
        public const byte __ALLCALL = 0x01;
        public const byte __INVRT = 0x10;
        public const byte __OUTDRV = 0x04;


        public const string I2C_CONTROLLER_NAME = "I2C1";

        public const int BOTTOM_CONTROLLER_I2C_SLAVE_ADDRESS = 0x40;
        public const int TOP_CONTROLLER_I2C_SLAVE_ADDRESS = 0x41;

        I2cDevice I2C_TOP_BOARD;
        I2cDevice I2C_BOTTOM_BOARD;

        public MainPage()
        {
            this.InitializeComponent();

            try
            {
                //INITIALIZE I2C CONNECTION WITH THE PWM PI HATS 
                InitializeI2Cstem(I2C_CONTROLLER_NAME, (byte)BOTTOM_CONTROLLER_I2C_SLAVE_ADDRESS, (byte)TOP_CONTROLLER_I2C_SLAVE_ADDRESS);

                //SET THE FREQUENCY OF THE TOP PWM PI HAT BOARD
                setPWMFreq(I2C_TOP_BOARD, 50);

                //SET THE FREQUENCY OF THE BOOTOM PWM PI HAT BOARD
                setPWMFreq(I2C_BOTTOM_BOARD, 50);

                //TURN SERVO TO MIDDLE POSITION BETWEEN END POINTS (90 DEGREES)
                setPWM(I2C_TOP_BOARD, 1, 0, 300);
                setPWM(I2C_BOTTOM_BOARD, 1, 0, 300);

                //TURN SERVO COUNTER-CLOCKWISE TO THE OPPOSITE END POINT (0 DEGREES)
                setPWM(I2C_TOP_BOARD, 1, 0, 100);
                setPWM(I2C_BOTTOM_BOARD, 1, 0, 100);

                //TURN SERVO CLOCKWISE 45 DEGREES FROM END POINT (45 DEGREES)
                setPWM(I2C_TOP_BOARD, 1, 0, 200);
                setPWM(I2C_BOTTOM_BOARD, 1, 0, 200);

                //TURN SERVO CLOCKWISE TO MIDDLE POSITION BETWEEN END POINTS (90 DEGREES)
                setPWM(I2C_TOP_BOARD, 1, 0, 300);
                setPWM(I2C_BOTTOM_BOARD, 1, 0, 300);

                //TURN SERVO CLOCKWISE 45 DEGREES FROM MIDDLE POSITION (135 DEGREES)
                setPWM(I2C_TOP_BOARD, 1, 0, 400);
                setPWM(I2C_BOTTOM_BOARD, 1, 0, 400);

                //TURN SERVO CLOCKWISE 45 DEGREES TO THE SECOND END POINT (180 DEGREES)
                setPWM(I2C_TOP_BOARD, 1, 0, 500);
                setPWM(I2C_BOTTOM_BOARD, 1, 0, 500);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: {0}", e.Message);
                return;
            }
        }


        private async void InitializeI2Cstem(string I2C_CONTROLLER_NAME, byte SERVO_PWM_BOARD1, byte SERVO_PWM_BOARD2)
        {
            //INITIALIZE I2C COMMUNICATION
            string deviceSelector = I2cDevice.GetDeviceSelector(I2C_CONTROLLER_NAME);
            var i2cDeviceControllers = await DeviceInformation.FindAllAsync(deviceSelector);

            //INITIALIZE I2C CONNECTION COMMUNICATION FOR PWM HAT (BOTTOM BOARD)
            var servoControlBoardBottom = new I2cConnectionSettings(SERVO_PWM_BOARD1);
            servoControlBoardBottom.BusSpeed = I2cBusSpeed.StandardMode;
            servoControlBoardBottom.SharingMode = I2cSharingMode.Shared;
            I2C_BOTTOM_BOARD = await I2cDevice.FromIdAsync(i2cDeviceControllers[0].Id, servoControlBoardBottom);

            //INITIALIZE I2C CONNECTION COMMUNICATION FOR PWM HAT (TOP BOARD)
            var servoControlBoardTop = new I2cConnectionSettings(SERVO_PWM_BOARD2);
            servoControlBoardTop.BusSpeed = I2cBusSpeed.FastMode;
            servoControlBoardTop.SharingMode = I2cSharingMode.Shared;
            I2C_TOP_BOARD = await I2cDevice.FromIdAsync(i2cDeviceControllers[0].Id, servoControlBoardTop);
        }

        //SET THE PWM FREQUENCY OF THE SERVOS ON THE SPECIFIC PWM PI HAT
        public void setPWMFreq(I2cDevice board, int freq)
        {
            double preScale = 25000000.0;
            preScale /= 4096;
            preScale /= (float)freq;
            preScale -= 1.0;

            preScale = Math.Floor(preScale + .5);
            var oldMode = board.ReadPartial(new byte[] { (byte)(PCA9685_REG_MODE1) });
            var newMode = (oldMode.BytesTransferred & 0x7F) | 0x10;
            board.Write(new byte[] { (byte)PCA9685_REG_MODE1, (byte)(newMode) });
            board.Write(new byte[] { (byte)PCA9685_REG_PRESCALE, (byte)(Math.Floor(preScale)) });
            board.Write(new byte[] { (byte)PCA9685_REG_MODE1, (byte)(oldMode.BytesTransferred) });
            board.Write(new byte[] { (byte)PCA9685_REG_MODE1, (byte)(oldMode.BytesTransferred | 0x80) });
        }

        //SET THE ON AND OFF SIGNALS SENT TO A SPECIFIC SERVO ON A SPECIFIED CHANNEL (OR ADDRESS ON THE BOARD (0-15)) THAT ALLOWS THE SERVO TO ROTATE
        public void setPWM(I2cDevice board, int channel, int on, int off)
        {
            board.Write(new byte[] { (byte)(PCA9685_REG_LED0_ON_L + (4 * channel)), (byte)(on & 0xFF) });
            board.Write(new byte[] { (byte)(PCA9685_REG_LED0_ON_H + (4 * channel)), (byte)(on >> 8) });
            board.Write(new byte[] { (byte)(PCA9685_REG_LED0_OFF_L + (4 * channel)), (byte)(off & 0xFF) });
            board.Write(new byte[] { (byte)(PCA9685_REG_LED0_OFF_H + (4 * channel)), (byte)(off >> 8) });

            //PAUSE TO ALLOW SERVO ROTATION TASK TO COMPLETE
            Task.Delay(TimeSpan.FromSeconds(1));
        }

        //SEND THE ON AND OFF SIGNALS THAT WILL BE SENT TO ALL CHANNELS (OR ADDRESSES ON THE BOARD(0-15)) THAT ALLOWS THE SERVO TO ROTATE
        public void setAllPWM(I2cDevice board, int on, int off)
        {
            board.Write(new byte[] { PCA9685_REG_ALL_ON_L, (byte)((byte)on & 0xFF) });
            board.Write(new byte[] { PCA9685_REG_ALL_ON_H, (byte)((byte)on >> 8) });
            board.Write(new byte[] { PCA9685_REG_ALL_OFF_L, (byte)((byte)off & 0xFF) });
            board.Write(new byte[] { PCA9685_REG_ALL_OFF_H, (byte)((byte)off >> 8) });

            //PAUSE TO ALLOW SERVO ROTATION TASK TO COMPLETE
            Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}