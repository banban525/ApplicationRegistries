using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistries.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var registies = new Registries();
            Console.WriteLine(registies.IsDebug);
            Console.WriteLine(registies.Proxy);
            Console.WriteLine(registies.InstallDir);
            Console.WriteLine(registies.ApplicationName);

            registies.AddOverrideBehavior("InstallDir", () => "C:\\");

            Console.WriteLine(registies.InstallDir);
        }
    }
}
