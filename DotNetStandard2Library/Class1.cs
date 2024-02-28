using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

using DotNetStandard2Library.resources;

namespace DotNetStandard2Library {
    public static class Class1 {
        public static async Task Loop(int max) {

            var counter = 0;
            while (max is -1 || counter < max) {
                Console.WriteLine($"DNS2 Counter: {++counter}");
                await Task.Delay(TimeSpan.FromMilliseconds(1_000));
            }
        }

        public static void LogOs() {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Console.WriteLine("Current executing Assembly: " + assembly.FullName);
            Console.WriteLine($"OS Version: {Environment.OSVersion}");
            Console.WriteLine($"OS Platform: {Environment.OSVersion.Platform}");
            Console.WriteLine($"Target Platform: {(IntPtr.Size == 8 ? "64-bit" : "32-bit")}");
            Console.WriteLine($"Framework: {RuntimeInformation.FrameworkDescription}");
            
            Console.WriteLine($"ProcessArchitecture: {RuntimeInformation.ProcessArchitecture}");
            Console.WriteLine($"ImageRuntimeVersion: {assembly.ImageRuntimeVersion}");
        }

        public static void LogTranslatedStrings() {
            var languages = new[] { "de", "en-US", "da" };

            foreach (var language in languages) {

                // Change the current culture to the specified culture
                Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

                Console.WriteLine($"String for: ViewingTexts.AcceptTissue:  {ViewingTexts.AcceptTissue}");
            }
        }
    }
}
