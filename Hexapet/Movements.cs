using System;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;

namespace Hexapet
{
    public class Movements
    {
        // constants for the servo control hat registers
        private const byte PCA9685_REG_MODE1 = 0x0;
        private const byte PCA9685_REG_MODE2 = 0x1;

        private const byte PCA9685_REG_LED0_ON_L = 0x06;
        private const byte PCA9685_REG_LED0_ON_H = 0x07;
        private const byte PCA9685_REG_LED0_OFF_L = 0x08;
        private const byte PCA9685_REG_LED0_OFF_H = 0x09;

        private const byte PCA9685_REG_LED1_ON_L = 0x0A;
        private const byte PCA9685_REG_LED1_ON_H = 0x0B;
        private const byte PCA9685_REG_LED1_OFF_L = 0x0C;
        private const byte PCA9685_REG_LED1_OFF_H = 0x0D;

        private const byte PCA9685_REG_LED2_ON_L = 0x0E;
        private const byte PCA9685_REG_LED2_ON_H = 0x0F;
        private const byte PCA9685_REG_LED2_OFF_L = 0x10;
        private const byte PCA9685_REG_LED2_OFF_H = 0x11;

        private const byte PCA9685_REG_LED3_ON_L = 0x12;
        private const byte PCA9685_REG_LED3_ON_H = 0x13;
        private const byte PCA9685_REG_LED3_OFF_L = 0x14;
        private const byte PCA9685_REG_LED3_OFF_H = 0x15;

        private const byte PCA9685_REG_LED4_ON_L = 0x16;
        private const byte PCA9685_REG_LED4_ON_H = 0x17;
        private const byte PCA9685_REG_LED4_OFF_L = 0x18;
        private const byte PCA9685_REG_LED4_OFF_H = 0x19;

        private const byte PCA9685_REG_LED5_ON_L = 0x1A;
        private const byte PCA9685_REG_LED5_ON_H = 0x1B;
        private const byte PCA9685_REG_LED5_OFF_L = 0x1C;
        private const byte PCA9685_REG_LED5_OFF_H = 0x1D;

        private const byte PCA9685_REG_LED6_ON_L = 0x1E;
        private const byte PCA9685_REG_LED6_ON_H = 0x1F;
        private const byte PCA9685_REG_LED6_OFF_L = 0x20;
        private const byte PCA9685_REG_LED6_OFF_H = 0x21;

        private const byte PCA9685_REG_LED7_ON_L = 0x22;
        private const byte PCA9685_REG_LED7_ON_H = 0x23;
        private const byte PCA9685_REG_LED7_OFF_L = 0x24;
        private const byte PCA9685_REG_LED7_OFF_H = 0x25;

        private const byte PCA9685_REG_LED8_ON_L = 0x26;
        private const byte PCA9685_REG_LED8_ON_H = 0x27;
        private const byte PCA9685_REG_LED8_OFF_L = 0x28;
        private const byte PCA9685_REG_LED8_OFF_H = 0x29;

        private const byte PCA9685_REG_LED9_ON_L = 0x2A;
        private const byte PCA9685_REG_LED9_ON_H = 0x2B;
        private const byte PCA9685_REG_LED9_OFF_L = 0x2C;
        private const byte PCA9685_REG_LED9_OFF_H = 0x2D;

        private const byte PCA9685_REG_LED10_ON_L = 0x2E;
        private const byte PCA9685_REG_LED10_ON_H = 0x2F;
        private const byte PCA9685_REG_LED10_OFF_L = 0x30;
        private const byte PCA9685_REG_LED10_OFF_H = 0x31;

        private const byte PCA9685_REG_LED11_ON_L = 0x32;
        private const byte PCA9685_REG_LED11_ON_H = 0x33;
        private const byte PCA9685_REG_LED11_OFF_L = 0x34;
        private const byte PCA9685_REG_LED11_OFF_H = 0x35;

        private const byte PCA9685_REG_LED12_ON_L = 0x36;
        private const byte PCA9685_REG_LED12_ON_H = 0x37;
        private const byte PCA9685_REG_LED12_OFF_L = 0x38;
        private const byte PCA9685_REG_LED12_OFF_H = 0x39;

        private const byte PCA9685_REG_LED13_ON_L = 0x3A;
        private const byte PCA9685_REG_LED13_ON_H = 0x3B;
        private const byte PCA9685_REG_LED13_OFF_L = 0x3C;
        private const byte PCA9685_REG_LED13_OFF_H = 0x3D;

        private const byte PCA9685_REG_LED14_ON_L = 0x3E;
        private const byte PCA9685_REG_LED14_ON_H = 0x3F;
        private const byte PCA9685_REG_LED14_OFF_L = 0x40;
        private const byte PCA9685_REG_LED14_OFF_H = 0x41;

        private const byte PCA9685_REG_LED15_ON_L = 0x42;
        private const byte PCA9685_REG_LED15_ON_H = 0x43;
        private const byte PCA9685_REG_LED15_OFF_L = 0x44;
        private const byte PCA9685_REG_LED15_OFF_H = 0x45;

        private const byte PCA9685_REG_ALL_ON_L = 0xFA;
        private const byte PCA9685_REG_ALL_ON_H = 0xFB;
        private const byte PCA9685_REG_ALL_OFF_L = 0xFC;
        private const byte PCA9685_REG_ALL_OFF_H = 0xFD;

        private const byte PCA9685_REG_PRESCALE = 0xFE;

        private const byte __RESTART = 0x80;
        private const byte __SLEEP = 0x10;
        private const byte __ALLCALL = 0x01;
        private const byte __INVRT = 0x10;
        private const byte __OUTDRV = 0x04;

        // constants for the I2C connections to the servo control hats
        private const string I2C_CONTROLLER_NAME = "I2C1"; // specific to RPI2 
        private const byte SERVO_HAT_BOTTOM_I2C_ADDRESS = 0x40; //address of the bottom servo controll hat 
        private const byte SERVO_HAT_TOP_I2C_ADDRESS = 0x41; // address of the top servo controll hat 

        private static I2cDevice i2cServoHatBottom; // bottom servo controll hat
        private static I2cDevice i2cServoHatTop; // top servo controll hat

        public static async void InitializeHats()
        {
            string deviceSelector = I2cDevice.GetDeviceSelector(I2C_CONTROLLER_NAME);
            var i2cDeviceControllers = await DeviceInformation.FindAllAsync(deviceSelector);

            // connect to bottom servo controller hat via I2C bus
            var servoHatBottomSettings = new I2cConnectionSettings(SERVO_HAT_BOTTOM_I2C_ADDRESS);
            servoHatBottomSettings.BusSpeed = I2cBusSpeed.FastMode;
            i2cServoHatBottom = await I2cDevice.FromIdAsync(i2cDeviceControllers[0].Id, servoHatBottomSettings);

            // connect to top servo controller hat via I2C bus
            var servoHatTopSettings = new I2cConnectionSettings(SERVO_HAT_TOP_I2C_ADDRESS);
            servoHatTopSettings.BusSpeed = I2cBusSpeed.FastMode;
            i2cServoHatTop = await I2cDevice.FromIdAsync(i2cDeviceControllers[0].Id, servoHatTopSettings);

            // set the PWM frequency for the servo control boards
            // 50Hz equates to a cycle time of 20ms
            // at 4096 bits of resolution, 1 ms is approximately 200
            setPWMFreq(i2cServoHatBottom, 50);
            setPWMFreq(i2cServoHatTop, 50);

            Stand();
            Task.Delay(1000).Wait();

//            Crouch();
//            Task.Delay(1000).Wait();

//            Raise();
//            Task.Delay(1000).Wait();

//            Stand();
//            Task.Delay(1000).Wait();

        }

        // set the PWM frequency for the servos on the specified servo controll board 
        private static void setPWMFreq(I2cDevice board, int freq)
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

        // send movement commmand to the specified servo - i.e. channel - on the specified servo controll board
        // movement is controlled by the duration the duty cycle is high - i.e. off minus on
        private static void setPWM(I2cDevice board, int channel, int on, int off)
        {
            board.Write(new byte[] { (byte)(PCA9685_REG_LED0_ON_L + (4 * channel)), (byte)(on & 0xFF) });
            board.Write(new byte[] { (byte)(PCA9685_REG_LED0_ON_H + (4 * channel)), (byte)(on >> 8) });
            board.Write(new byte[] { (byte)(PCA9685_REG_LED0_OFF_L + (4 * channel)), (byte)(off & 0xFF) });
            board.Write(new byte[] { (byte)(PCA9685_REG_LED0_OFF_H + (4 * channel)), (byte)(off >> 8) });
        }

        // send movement command to all servos on the specified board 
        // movement is controlled by the duration the duty cycle is high - i.e. off minus on
        private static void setAllPWM(I2cDevice board, int on, int off)
        {
            board.Write(new byte[] { PCA9685_REG_ALL_ON_L, (byte)((byte)on & 0xFF) });
            board.Write(new byte[] { PCA9685_REG_ALL_ON_H, (byte)((byte)on >> 8) });
            board.Write(new byte[] { PCA9685_REG_ALL_OFF_L, (byte)((byte)off & 0xFF) });
            board.Write(new byte[] { PCA9685_REG_ALL_OFF_H, (byte)((byte)off >> 8) });
        }

        public static void Stand()
        {
            // move all legs to standing position
            for (int i = 0; i < 6; i++)
            {
                // center horizontal hip
                center(i, "hHip");

                // center vertical hip
                // so that upper leg is almost horizontal
                center(i, "vHip");

                // center knee
                // so that lower leg is almost vertical
                center(i, "knee");
            }

            // wait .5 seconds so that all servos can complete movement
            Task.Delay(500).Wait();
        }

        public static void Raise()
        {
            // move all legs
            for (int i = 0; i < 6; i++)
            {
                // center horizontal hip
                center(i, "hHip");

                // adduct vertical hip
                // so that upper leg is pointing down
                adduct(i, "vHip", 45);

                // abduct knee
                // so that lower leg is pointing out
                abduct(i, "knee");
            }

            // wait .5 seconds so that all servos can complete movement
            Task.Delay(500).Wait();
        }

        public static void Crouch()
        {
            // move all legs
            for (int i = 0; i < 6; i++)
            {
                // center horizontal hip
                center(i, "hHip");

                // abduct vertical hip
                // so that upper leg is pointing up
                abduct(i, "vHip");

                // adduct knee
                // so that lower leg is pointing in
                adduct(i, "knee", 45);
            }

            // wait .5 seconds so that all servos can complete movement
            Task.Delay(500).Wait();
        }

        public static void Walk(string direction)
        {
            // pick up every other leg by abducting vertical hip
            for (int i = 0; i < 6; i += 2)
                abduct(i, "vHip");

            Task.Delay(250).Wait();

            // for legs still on the ground 
            // rotate the horizontal hip backward (adduct) to move body forward
            // rotate the horizontal hip foreward (abduct) to move the body backword
            for (int i = 1; i < 6; i += 2)
            {
                if (direction == "forward")
                    adduct(i, "hHip", 45);
                else
                    abduct(i, "hHip");
            }

            Task.Delay(250).Wait();

            // lower the raised legs back down
            for (int i = 0; i < 6; i += 2)
                center(i, "vHip");

            Task.Delay(250).Wait();

            // raise legs whose hips just rotated
            // and return their hips back to their center position
            for (int i = 1; i < 6; i += 2)
            {
                abduct(i, "vHip");
                center(i, "hHip");
            }

            Task.Delay(250).Wait();

            // for legs still on the ground 
            // rotate the horizontal hip backward (adduct) to move body forward
            // rotate the horizontal hip foreward (abduct) to move the body backword
            for (int i = 0; i < 6; i += 2)
            {
                if (direction == "forward")
                    adduct(i, "hHip", 45);
                else
                    abduct(i, "hHip");
            }

            Task.Delay(250).Wait();

            // lower the raised legs back down
            for (int i = 1; i < 6; i += 2)
                center(i, "vHip");

            Task.Delay(250).Wait();

            // raise legs whose hips just rotated
            // and return their hips back to their center position
            for (int i = 0; i < 6; i += 2)
            {
                abduct(i, "vHip");
                center(i, "hHip");
            }

            Task.Delay(250).Wait();

            // lower the raised legs back down
            for (int i = 0; i < 6; i += 2)
                center(i, "vHip");

            Task.Delay(250).Wait();
        }

        public static void Turn(string direction)
        {
            int[] leadingLegs = new int[3];
            int[] trailingLegs = new int[3];

            // define leading and trailing legs
            // based on direction of turn

            // if turning right then legs should be
            //     leadingLegs = { 1, 3, 5 };
            //     trailingLegs = { 0, 2, 4 };

            // if turning left then legs should be
            //     leadingLegs = { 4, 2, 0 };
            //     trailingLegs = { 1, 3, 5 };

            // leg order in the array is important for leading legs
            // because the horizontal hip on the first leg in the leading array
            // (the leg on the same side of the body as the turn)
            // turns foreward while the horizontal hips on the other two legs turn backward
            // leg order is irrelevant for trailing legs

            if (direction == "right")
            {
                leadingLegs[0] = 1;
                leadingLegs[1] = 3;
                leadingLegs[2] = 5;
                trailingLegs[0] = 0;
                trailingLegs[1] = 2;
                trailingLegs[2] = 4;
            }
            else if (direction == "left")
            {
                leadingLegs[0] = 4;
                leadingLegs[1] = 2;
                leadingLegs[2] = 0;
                trailingLegs[0] = 1;
                trailingLegs[1] = 3;
                trailingLegs[2] = 5;
            }
            else return;

            // raise upper leg on trailing legs
            foreach (int i in trailingLegs)
                abduct(i, "vHip");
            Task.Delay(500).Wait();

            // twist horizontal hip foreward on first leading leg
            // and horizontal hips backward on 2nd and 3rd leading legs
            abduct(leadingLegs[0], "hHip");
            adduct(leadingLegs[1], "hHip", 45);
            adduct(leadingLegs[2], "hHip", 45);
            Task.Delay(500).Wait();

            // lower upper leg on trailing legs back down to horizontal position
            foreach (int i in trailingLegs)
                center(i, "vHip");
            Task.Delay(500).Wait();

            // raise upper leg on leading legs
            foreach (int i in leadingLegs)
                abduct(i, "vHip");
            Task.Delay(500).Wait();

            // move horizontal hips back to center on leading legs
            foreach (int i in leadingLegs)
                center(i, "hHip");
            Task.Delay(500).Wait();

            // lower upper legs on leading legs back down to horizontal position
            foreach (int i in leadingLegs)
                center(i, "vHip");
            Task.Delay(500).Wait();
        }

        private static void center(int leg, string joint)
        {
            int jointOffset = 0;

            switch (joint)
            {
                case "hHip":
                    jointOffset = 0;
                    break;
                case "vHip":
                    jointOffset = 1;
                    break;
                case "knee":
                    jointOffset = 2;
                    break;
            }

            // turn servo to center point
            if (leg < 5)
                // use the top servo control board for legs 0 through 4
                // horizontal hip servo for leg 0 is on channel 0
                // vertical hip servo for leg 0 is on channel 1
                // knee servo for leg 0 is on channel 2
                // corresponding joints on subsequent legs are every 3rd channel
                setPWM(i2cServoHatTop, (3 * leg) + jointOffset, 0, 300);
            else
                // use the bottom servo control board for leg 5
                // horizontal hip servo for leg 5 is on channel 13
                // vertical hip servo for leg 5 is on channel 14
                // knee servo for leg 5 is on channel 15
                setPWM(i2cServoHatBottom, 13 + jointOffset, 0, 300);
        }

        private static void abduct(int leg, string joint)
        {
            int jointOffset = 0;
            int jointDirection = 0;

            switch (joint)
            {
                case "hHip":
                    jointOffset = 0;
                    jointDirection = 1;
                    break;
                case "vHip":
                    jointOffset = 1;
                    jointDirection = 1;
                    break;
                case "knee":
                    jointOffset = 2;
                    jointDirection = -1;
                    break;
            }

            // use the top servo control board for legs 0 through 4
            // use the bottom servo control board for leg 5

            // horizontal hip servo for leg 0 is on channel 0
            // vertical hip servo for leg 0 is on channel 1
            // knee servo for leg 0 is on channel 2
            // corresponding joints on subsequent legs are every 3rd channel
            // except for leg 5 where servos are on channel 13 - 15

            // turn servo 45 degrees
            // direction depends on leg and joint

            if (leg < 3)
                setPWM(i2cServoHatTop, (3 * leg) + jointOffset, 0, 300 + (jointDirection*100));
            else if (leg < 5)
                setPWM(i2cServoHatTop, (3 * leg) + jointOffset, 0, 300 - (jointDirection*100));
            else
                setPWM(i2cServoHatBottom, 13 + jointOffset, 0, 300 - (jointDirection * 100));
        }

        private static void adduct(int leg, string joint, int degrees)
        {
            int jointOffset = 0;
            int jointDirection = 0;

            switch (joint)
            {
                case "hHip":
                    jointOffset = 0;
                    jointDirection = 1;
                    break;
                case "vHip":
                    jointOffset = 1;
                    jointDirection = 1;
                    break;
                case "knee":
                    jointOffset = 2;
                    jointDirection = -1;
                    break;
            }

            // use the top servo control board for legs 0 through 4
            // use the bottom servo control board for leg 5

            // horizontal hip servo for leg 0 is on channel 0
            // vertical hip servo for leg 0 is on channel 1
            // knee servo for leg 0 is on channel 2
            // corresponding joints on subsequent legs are every 3rd channel
            // except for leg 5 where servos are on channel 13 - 15

            // turn servo specified number of degrees
            // direction depends on leg and joint

            if (leg < 3)
                setPWM(i2cServoHatTop, (3 * leg) + jointOffset, 0, 300 - (jointDirection * degrees / 45 * 100));
            else
            {
                if (leg < 5)
                    setPWM(i2cServoHatTop, 3 * leg, 0, 300 + (jointDirection * degrees / 45 * 100));
                else
                    setPWM(i2cServoHatBottom, 13, 0, 300 + (jointDirection * degrees / 45 * 100));
            }
        }

    }
}
