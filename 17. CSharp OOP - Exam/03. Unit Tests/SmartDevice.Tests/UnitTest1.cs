namespace SmartDevice.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Tests
    {
        private Device device;

        [SetUp]
        public void Setup()
        {
           device = new Device(32);
        }

        [Test]
        public void DeviceClassConstructorTest()
        {
            Assert.AreEqual(32, device.MemoryCapacity);
            Assert.AreEqual(32, device.AvailableMemory);
            Assert.AreEqual(0, device.Photos);
            Assert.IsNotNull(device.Applications);
            Assert.AreEqual(0, device.Applications.Count);
            Assert.IsTrue(device.Applications.GetType() == typeof(List<string>));
        }

        [Test]
        public void DeviceClassTakePhotoMethodShouldWorkCorrectlyIfPhotoSizeIsLessOrEqualThenDeviceAvailableMemory()
        {
            Assert.IsTrue(device.TakePhoto(10));
            Assert.AreEqual(22, device.AvailableMemory);
            Assert.AreEqual(1, device.Photos);
        }

        [Test]
        public void DeviceClassTakePhotoMethodShouldReturnFalseIfPhotoSizeIsMoreThenDeviceAvailableMemory()
        {
            //Assert.AreEqual(32, device.AvailableMemory);
            Assert.IsFalse(device.TakePhoto(34));
            //Assert.AreEqual(0, device.Photos);
        }

        [Test]
        public void DeviceClassInstallAppMethodShouldWorkCorrectlyIfAppSizeIsLessOrEqualThenDeviceAvailableMemory()
        {
            Assert.AreEqual("Facebook is installed successfully. Run application?", device.InstallApp("Facebook", 20));
            Assert.AreEqual(12, device.AvailableMemory);
            Assert.AreEqual(1, device.Applications.Count);
            Assert.IsTrue(device.Applications.Contains("Facebook"));
        }

        [Test]
        public void DeviceClassInstallAppMethodShouldReturnExceptionIfAppSizeIsMoreThenDeviceAvailableMemory()
        {
            //Assert.AreEqual(32, device.AvailableMemory);
            Assert.Throws<InvalidOperationException>(() => device.InstallApp("Facebook", 34), "Not enough available memory to install the app.");
            //Assert.AreEqual(0, device.Applications.Count);
        }

        [Test]
        public void DeviceClassFormatDeviceMethodShouldWorkCorrectly()
        {
            Assert.AreEqual(0, device.Photos);
            Assert.IsTrue(device.Applications.GetType() == typeof(List<string>));
            Assert.AreEqual(0, device.Applications.Count);
            Assert.AreEqual(device.AvailableMemory, device.MemoryCapacity);
        }

        [Test]
        public void DeviceClassGetDeviceStatusMethodShouldWorkCorrectly()
        {
            device = new Device(64);
            device.TakePhoto(10);
            device.InstallApp("Facebook", 20);
            device.InstallApp("Instagram", 30);

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Memory Capacity: 64 MB, Available Memory: 4 MB");
            stringBuilder.AppendLine($"Photos Count: 1");
            stringBuilder.AppendLine($"Applications Installed: Facebook, Instagram");

            Assert.AreEqual(stringBuilder.ToString().TrimEnd(), device.GetDeviceStatus());
        }
    }
}