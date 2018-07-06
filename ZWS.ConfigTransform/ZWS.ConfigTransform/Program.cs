using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWS.ConfigTransform
{
    class Program
    {
        static void Main(string[] args)
        {
            var configName = args[0];
            var sourceDir = args[1];
            var targetFile = args[2];

            ConfigTransformer.Transform(configName, sourceDir, targetFile);
        }
    }
}
