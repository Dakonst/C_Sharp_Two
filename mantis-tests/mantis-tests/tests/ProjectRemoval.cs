using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Threading;

namespace mantis_tests
{
    public class ProjectRemoval : TestBase
    {
        [Test]
        public void ProjectRemovalTest()
        {
            List<ProjectData> oldProjects = app.Projects.GetProjectList();

            if (oldProjects.Count == 0)
            {
                app.Projects.Create(new ProjectData()
                {
                    Name = "NewTest"
                });
                oldProjects = app.Projects.GetProjectList();
            }

            app.Projects.Delete();

            Thread.Sleep(1000);

            List<ProjectData> newProjects = app.Projects.GetProjectList();
            oldProjects.RemoveAt(0);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
