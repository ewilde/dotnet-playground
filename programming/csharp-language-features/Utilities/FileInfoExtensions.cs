using System.IO;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.Utilities
{
    public static class FileInfoExtensions
    {
        public static FileInfo ChangeDrive(this FileInfo value, DriveInfo drive)
        {
            var oldDrive = new DriveInfo(value.FullName);
            return new FileInfo(value.FullName.Replace(oldDrive.Name, drive.Name));
        }
    }

    [TestFixture]
    public class FileInfoExtensionsTests
    {
        [Test]
        public void can_swap_drive_letters()
        {
           Assert.That(new FileInfo(@"c:\logs\web").ChangeDrive(new DriveInfo("d:")).FullName, Is.EqualTo(new FileInfo(@"d:\logs\web").FullName)); 
        }
    }
}