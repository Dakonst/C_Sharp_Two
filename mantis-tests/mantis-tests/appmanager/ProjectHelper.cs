using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager) : base(manager) { }

        

        public ProjectHelper Create(ProjectData project)
        {
            OpenProjectManagerPage();
            InitProjectCreation();
            FillProjectCreationForm(project);
            SubmitProjectCreation();
            return this;
        }

        //Удаляет 1ый в списке проект
        public ProjectHelper Delete()
        {
            OpenProjectManagerPage();
            SelectFirstProjectInList();
            InitProjectRemoval();
            SubmitProjectRemoval();
            return this;
        }

        public ProjectHelper Delete(int i)
        {
            OpenProjectManagerPage();
            selectProjectById(i);
            InitProjectRemoval();
            SubmitProjectRemoval();
            return this;
        }


        private void OpenProjectManagerPage()
        {
            driver.Navigate().GoToUrl("http://localhost/mantisbt-2.25.3/manage_proj_page.php");
        }
        private void InitProjectCreation()
        {
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }
        private void FillProjectCreationForm(ProjectData project)
        {
            driver.FindElement(By.Id("project-name")).Click();
            driver.FindElement(By.Id("project-name")).Clear();
            driver.FindElement(By.Id("project-name")).SendKeys(project.Name);
        }

        private void SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@value='Добавить проект']")).Click();
        }

        private void SelectFirstProjectInList()
        {
            driver.FindElement(By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div[2]/div[2]/div/div[2]/table/tbody/tr/td/a")).Click();
        }

        private void selectProjectById(int i)
        {
            driver.FindElement(By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div[2]/div[2]/div/div[2]/table/tbody/tr[" + (i + 1) + "]/td/a")).Click();
        }

        private void InitProjectRemoval()
        {
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
        }

        private void SubmitProjectRemoval()
        {
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
        }

        private List<ProjectData> projectCache = null;
        public List<ProjectData> GetProjectList()
        {
           /* if (projectCache == null)
            {*/
                projectCache = new List<ProjectData>();
                OpenProjectManagerPage();
                ICollection<IWebElement> elements = driver.FindElements(By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div[2]/div[2]/div/div[2]/table/tbody/tr/td[1]"));
                int count = elements.Count();
                for (int i = 0; i < count; i++)
                {
                    projectCache.Add(new ProjectData()
                    {
                        Name = elements.ElementAt(i).Text
                    }) ;
                }
           // }
            return new List<ProjectData>(projectCache);
        }
    }
}
