using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager)
            : base(manager)
        {
        }

        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
            MantisConnect.MantisConnectPortTypeClient client = new MantisConnect.MantisConnectPortTypeClient();
            MantisConnect.IssueData issue = new MantisConnect.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new MantisConnect.ObjectRef();
            issue.project.id = project.Id;
            client.mc_issue_add(account.Name, account.Password, issue);
        }

        public List<ProjectData> GetProjectsListByAPI(AccountData account)
        {
            MantisConnect.MantisConnectPortTypeClient client = new MantisConnect.MantisConnectPortTypeClient();
            MantisConnect.ProjectData[] projects = client.mc_projects_get_user_accessible(account.Name, account.Password);

            List<ProjectData> projectCache = new List<ProjectData>();
            for (int i = 0; i < projects.Length; i++)
            {
                projectCache.Add(new ProjectData()
                {
                    Name = projects[i].name
                }) ;
            }
            return new List<ProjectData>(projectCache);
        }
        public void CreateProjectByAPI(AccountData account, ProjectData project)
        {
            MantisConnect.MantisConnectPortTypeClient client = new MantisConnect.MantisConnectPortTypeClient();
            MantisConnect.ProjectData mantisProject = new MantisConnect.ProjectData();
            mantisProject.name = project.Name;
            client.mc_project_add(account.Name, account.Password, mantisProject);
        }
    }
}
