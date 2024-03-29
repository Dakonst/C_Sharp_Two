﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        private StringBuilder verificationErrors;
        protected string baseURL;
        protected string baseURLProj;

        public RegistrationHelper Registration { get; set; }
        public FtpHelper FtpHelper { get; set; }
        
        protected LoginHelper loginHelper;

        protected ProjectHelper projectHelper;

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            baseURL = "http://localhost/mantisbt-2.25.3";
            baseURLProj = baseURL + "/manage_proj_page.php";
            Registration = new RegistrationHelper(this);
            FtpHelper = new FtpHelper(this);
            loginHelper = new LoginHelper(this);
            projectHelper = new ProjectHelper(this, baseURLProj);
            API = new APIHelper(this);
        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Driver.Url = newInstance.baseURL + "/login_page.php";
                app.Value = newInstance;
            }
            return app.Value;
        }
        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }
        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }
        public ProjectHelper Projects
        {
            get
            {
                return projectHelper;
            }
        }

        public APIHelper API { get; set; }

    }
}
