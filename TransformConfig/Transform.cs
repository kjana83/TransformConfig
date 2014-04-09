using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransformConfig
{
    public class Transform : ITransform
    {
        public string ApplyTransform(string sourceFile, string transformFile)
        {
            const string TRANSFORM_FILE = @"<Project ToolsVersion=""4.0"" DefaultTargets=""Demo"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
    <UsingTask TaskName=""TransformXml"" AssemblyFile=""$(MSBuildExtensionsPath)\Microsoft\VisualStudio\v10.0\Web\Microsoft.Web.Publishing.Tasks.dll""/>
    <Target Name=""Transform""><TransformXml Source=""{source}"" Transform=""{transform}"" Destination=""{destination}""/></Target></Project>";

            using (var proj = File.Create(Path.GetTempFileName()))
            using (var output = File.Create(Path.GetTempFileName()))
            {
                //Create transformation project
                string project = TRANSFORM_FILE.Replace("{source}", sourceFile)
                                                  .Replace("{transform}", transformFile)
                                                  .Replace("{destination}", output.Name);
                File.WriteAllText(proj.Name, project);

                //Run the project
                var cmd = new ProcessStartInfo(@"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe", proj.Name + " /t:Transform")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                Process.Start(cmd)
                       .WaitForExit();

                //Return output
                return File.ReadAllText(output.Name);
            }
        }
    }
}
