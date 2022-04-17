using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.FtpClient;

namespace mantis_tests
{


    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [OneTimeSetUp]
        public void setUpConfig()
        {
            app.FtpHelper.BackupFile("/config_innc.php");
            using (Stream localFile = File.Open("config_innc.php", FileMode.Open))
            {
                app.FtpHelper.Upload("/config_innc.php", localFile);
            } 
        }

        [Test]
        public void TesAccountRegistration()
        {
            AccountData account = new AccountData("testuser", "password", "testuser@localhost.localdomain");
            app.Registration.Register(account);
        }

        [OneTimeTearDown]
        public void restoreConfig()
        {
            app.FtpHelper.RestoreBackupFile("/config_innc.php");
        }
    }
}
