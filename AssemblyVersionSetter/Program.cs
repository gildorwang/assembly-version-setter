using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AssemblyVersionSetter
{
    class Program
    {
        private static Regex _assemblyVersionRegex = new Regex(@"(?<=\[assembly\:\ AssemblyVersion\("")(.*?)(?=""\)\])", RegexOptions.Compiled);
        private static Regex _fileVersionRegex = new Regex(@"(?<=\[assembly\:\ AssemblyFileVersion\("")(.*?)(?=""\)\])", RegexOptions.Compiled);

        static void Main(string[] args)
        {
            //            var test = "[assembly: AssemblyVersion(\"1.1.3.0\")]";
            //            var match = _assemblyVersionRegex.Match(test);
            //            Console.WriteLine(match);
            //            Console.WriteLine(Replace(test, "1.1.5.0"));

            string version = args[0];
            foreach(var file in Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "AssemblyInfo.cs", SearchOption.AllDirectories))
            {
                Console.WriteLine("Processing {0}", file);
                var text = File.ReadAllText(file);
                var replaced = Replace(text, version);
                File.WriteAllText(file, replaced);
            }
        }

        private static string Replace(string text, string version)
        {
            return _fileVersionRegex.Replace(_assemblyVersionRegex.Replace(text, version), version);
        }
    }
}
