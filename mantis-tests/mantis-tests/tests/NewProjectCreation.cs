using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NUnit.Framework;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class NewProjectCreation : TestBase
    {
        [Test]
        public void NewProjectCreationTest()
        {
            List<ProjectData> oldProjects = app.Projects.GetProjectList();

            ProjectData newProject = new ProjectData()
            {
                Name = "NewTest"
            };

            for (int i = 0; i < oldProjects.Count; i++)
            {
                if (oldProjects[i].Name == newProject.Name)
                {
                    app.Projects.Delete(i);
                    oldProjects.RemoveAt(i);
                }
            }

            app.Projects.Create(newProject);

            Thread.Sleep(1000);

            List<ProjectData> newProjects = app.Projects.GetProjectList();

            oldProjects.Add(newProject);
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
