using System;
using System.Diagnostics;
using Windows.Devices.Enumeration;
using Windows.Devices.HumanInterfaceDevice;

namespace Hexapet
{
    /// <summary>
    /// **** Controllers Class ****
    /// HID Controller devices - XBox controller
    ///   Data transfer helpers: message parsers, direction to motor value translatores, etc.
    /// </summary>
    public class Controllers
    {
        public static bool FoundLocalControlsWorking = false;

        #region ----- Xbox HID-Controller -----

        private static XboxHidController controller;
        private static int lastControllerCount = 0;
        public static async void XboxJoystickInit()
        {
            string deviceSelector = HidDevice.GetDeviceSelector(0x01, 0x05);
            DeviceInformationCollection deviceInformationCollection = await DeviceInformation.FindAllAsync(deviceSelector);

            if (deviceInformationCollection.Count == 0)
            {
                Debug.WriteLine("No Xbox360 controller found!");
            }
            lastControllerCount = deviceInformationCollection.Count;

            foreach (DeviceInformation d in deviceInformationCollection)
            {
                Debug.WriteLine("Device ID: " + d.Id);

                HidDevice hidDevice = await HidDevice.FromIdAsync(d.Id, Windows.Storage.FileAccessMode.Read);

                if (hidDevice == null)
                {
                    try
                    {
                        var deviceAccessStatus = DeviceAccessInformation.CreateFromId(d.Id).CurrentStatus;

                        if (!deviceAccessStatus.Equals(DeviceAccessStatus.Allowed))
                        {
                            Debug.WriteLine("DeviceAccess: " + deviceAccessStatus.ToString());
                            FoundLocalControlsWorking = true;
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Xbox init - " + e.Message);
                    }

                    Debug.WriteLine("Failed to connect to the controller!");
                }

                controller = new XboxHidController(hidDevice);
                controller.DirectionChanged += Controller_DirectionChanged;
            }
        }

        public static async void XboxJoystickCheck()
        {
            string deviceSelector = HidDevice.GetDeviceSelector(0x01, 0x05);
            DeviceInformationCollection deviceInformationCollection = await DeviceInformation.FindAllAsync(deviceSelector);
            if (deviceInformationCollection.Count != lastControllerCount)
            {
                lastControllerCount = deviceInformationCollection.Count;
                XboxJoystickInit();
            }
        }

        private static void Controller_DirectionChanged(ControllerVector sender)
        {
            FoundLocalControlsWorking = true;
            Debug.WriteLine("Direction: " + sender.Direction + ", Magnitude: " + sender.Magnitude);
            XBoxToRobotDirection((sender.Magnitude < 2500) ? ControllerDirection.None : sender.Direction, sender.Magnitude);
        }

        static void XBoxToRobotDirection(ControllerDirection dir, int magnitude)
        {
            switch (dir)
            {
                case ControllerDirection.Down: Movements.Walk("backward"); break;
                case ControllerDirection.Up: Movements.Walk("forward"); break;
                case ControllerDirection.Left: Movements.Turn("left"); break;
                case ControllerDirection.Right: Movements.Turn("right"); break;
                //case ControllerDirection.DownLeft: Movements.Turn("backleft"); break;
                case ControllerDirection.DownRight: Movements.Crouch(); break;
                //case ControllerDirection.UpLeft: Movements.Turn("forleft"); break;
                case ControllerDirection.UpRight: Movements.Raise(); break;
                default: Movements.Stand(); break;
            }
        }
        #endregion
    }
}
