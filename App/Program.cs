using System;
using System.Threading.Tasks;

using DotNetStandard2Library;

static class Program {
        static async Task Main(string[] args) {
            var max = args.Length != 0 ? Convert.ToInt32(args[0]) : -1;

            Class1.LogOs();
            Class1.LogTranslatedStrings();
            await Class1.Loop(max);
        }

 }
