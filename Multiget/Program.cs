using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiget
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                if (args.Length != 1)
                {
                    throw new Exception($"You must specify a file of URLs.");
                }

                var urls = new List<string>();
                foreach (var line in File.ReadLines(args[0]).Where(x=>!string.IsNullOrEmpty(x)))
                {
                    urls.Add(line);
                }

                var getter = new UrlGetter(urls);
                await getter.GetAll();
            }
            catch (Exception ex)
            {
                var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;
                var progname = Path.GetFileNameWithoutExtension(fullname);
                Console.Error.WriteLine(progname + ": Error: " + ex.Message);
            }

        }
    }
}
