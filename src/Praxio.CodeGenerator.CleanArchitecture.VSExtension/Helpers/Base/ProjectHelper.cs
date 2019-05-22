using System;
using EnvDTE;
using EnvDTE80;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Commands;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers
{
    public static class ProjectHelper
    {
        private static DTE2 dte = MenuToolsCommandPackage.Instance.Dte;
        
        static private void FindProjectsIn(ProjectItem item, List<Project> results)
        {
            if (item.Object is Project)
            {
                var proj = (Project)item.Object;
                if (new Guid(proj.Kind) != Guid.Parse("66A26720-8FB5-11D2-AA7E-00C04F688DDE"))
                {
                    results.Add((Project)item.Object);
                }
                else
                {
                    foreach (ProjectItem innerItem in proj.ProjectItems)
                    {
                        FindProjectsIn(innerItem, results);
                    }
                }
            }
            if (item.ProjectItems != null)
            {
                foreach (ProjectItem innerItem in item.ProjectItems)
                {
                    FindProjectsIn(innerItem, results);
                }
            }
        }

        static private void FindProjectsIn(UIHierarchyItem item, List<Project> results)
        {
            if (item.Object is Project)
            {
                var proj = (Project)item.Object;
                if (new Guid(proj.Kind) != Guid.Parse("66A26720-8FB5-11D2-AA7E-00C04F688DDE"))
                {
                    results.Add((Project)item.Object);
                }
                else
                {
                    foreach (ProjectItem innerItem in proj.ProjectItems)
                    {
                        FindProjectsIn(innerItem, results);
                    }
                }
            }
            foreach (UIHierarchyItem innerItem in item.UIHierarchyItems)
            {
                FindProjectsIn(innerItem, results);
            }
        }

        static private IEnumerable<Project> GetEnvDTEProjectsInSolution()
        {
            var ret = new List<Project>();
            
            var hierarchy = dte.ToolWindows.SolutionExplorer;
            foreach (UIHierarchyItem innerItem in hierarchy.UIHierarchyItems)
            {
                FindProjectsIn(innerItem, ret);
            }
            return ret;
        }

        public static List<string> ListProjectsInSolution()
        {
            var listNames = new List<string>();
               
            foreach (Project project in GetEnvDTEProjectsInSolution())
                listNames.Add(project.Name);

            return listNames;            
        }

        public static string GetSolutionPath()
        {
            return Path.GetDirectoryName(GetSolutionName());
        }

        public static string GetSolutionName()
        {
            return dte.Solution.FullName;
        }
        
        public static InfoProjeto ProjectDetails(eProjeto project)
        {
            var layer = Enum.GetName(typeof(eProjeto), project);
            var layerInfo = dte.Solution.Projects.Cast<Project>().FirstOrDefault(p => p.Name.Contains($".{layer}"));
            var info = new InfoProjeto { Nome = layerInfo.Name, Diretorio = Path.GetDirectoryName(layerInfo.Properties.Item("FullPath").Value.ToString())};

            return info;
        }
    }
}
