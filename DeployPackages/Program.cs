using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DeployPackages
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Environment.CurrentDirectory);

            var frameworkVersion = (args.Length > 0) ? args[0] : "net40";
            var packagesPath = (args.Length > 1)     ? args[1] : "packages";
            var binFolderPath = (args.Length > 2)    ? args[2] : "web\\bin";

            if (!Directory.Exists(packagesPath))
            {
                Console.WriteLine("Packages path does not exist: " + packagesPath);
                Environment.Exit(1);
            }

            if (!Directory.Exists(binFolderPath))
            {
                Console.WriteLine("Bin folder path does not exist: " + binFolderPath);
                Environment.Exit(2);
            }

            var frameworkVersionNumber = 0;
            try
            {
                frameworkVersionNumber = int.Parse(frameworkVersion.Substring(3));
            }
            catch
            {
                Console.WriteLine("Framework must be in the format 'netxx': " + frameworkVersion);
                Environment.Exit(3);
            }

            var files = Directory.GetFiles(packagesPath, "*.dll", SearchOption.AllDirectories)
                .Select(f =>
                    {
                        return new
                        {
                            Filename = f,
                            Version = ExtractVersion(f)
                        };
                    })
                .Where(f => f.Version <= frameworkVersionNumber)
                .OrderBy(f => f.Version)
                .Select(f => f.Filename)
                .ToList();

            foreach (var file in files)
            {
                var dest = Path.Combine(binFolderPath, Path.GetFileName(file));
                Console.WriteLine("Copying " + file + "  =>  " + dest);
                File.Copy(file, dest, true);
            }
        }

        private static int ExtractVersion(string filename)
        {
            var regex = new Regex(@"\\net(\d\d)\\[^\\]*\.dll");
            var match = regex.Match(filename);

            if (!match.Success) return 0;

            return int.Parse(match.Groups[1].Value);
        }
    }
}
