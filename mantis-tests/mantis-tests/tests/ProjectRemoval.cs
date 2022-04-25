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
            //List<ProjectData> oldProjects = app.Projects.GetProjectList();
            List<ProjectData> oldProjects = app.API.GetProjectsListByAPI(new AccountData("administrator", "root", String.Empty));

            if (oldProjects.Count == 0)
            {
                /*app.Projects.Create(new ProjectData()
                {
                    Name = "NewTest"
                });
                oldProjects = app.Projects.GetProjectList();*/

                app.API.CreateProjectByAPI(new AccountData("administrator", "root", String.Empty), new ProjectData()
                {
                    Name = "NewTest"
                });
                oldProjects = app.API.GetProjectsListByAPI(new AccountData("administrator", "root", String.Empty));
            }

            string Name = oldProjects[0].Name;
            app.Projects.Delete(Name);

            Thread.Sleep(1000);

            //List<ProjectData> newProjects = app.Projects.GetProjectList();
            List<ProjectData> newProjects = app.API.GetProjectsListByAPI(new AccountData("administrator", "root", String.Empty));


            oldProjects.RemoveAt(0);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
