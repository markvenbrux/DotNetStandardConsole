using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;

namespace DotNetStandard2Library
{


    public static class ResourceFactory
    {

        #region Private fields

        /// <summary>
        /// Dictionary of resource assemblies with their resource keys.
        /// </summary>
        private static readonly Dictionary<Assembly, IEnumerable<object>> resourceAssemblies =
            new Dictionary<Assembly, IEnumerable<object>>();


        #endregion

        #region Public methods

        /// <summary>
        /// Register the specified <see cref="Assembly"/> as source of XAML Icon resources.
        /// </summary>
        public static void RegisterResourceAssembly(Assembly assembly)
        {
            if (!resourceAssemblies.ContainsKey(assembly))
            {
                // Do not specify the resource keys yet, lazy evaluation.
                resourceAssemblies.Add(assembly, null);
            }
        }


        public static byte[] GetResource(string resourceName) {
            byte[] bitmapBytes = null;
            foreach (var assembly in resourceAssemblies.Keys) {
                bitmapBytes = GetBitmapResource(assembly, resourceName);
                if (bitmapBytes != null) {
                    break;
                }
            }
            return bitmapBytes;
        }


        public static byte[] GetBitmapResource(Assembly assembly, string resourceName)
        {
            // Get a list of all resources in the assembly
            var resources = assembly.GetManifestResourceNames();
            var fullResourceName = resources.FirstOrDefault(n => n.EndsWith(resourceName));
            if (fullResourceName == null) {
                return null;
            }
            // load the bitmap from the resources
            var bitmapStream =
                assembly.GetManifestResourceStream(fullResourceName);
            //  convert to byte array
            byte[] bitmapBytes = new byte[bitmapStream.Length];
            bitmapStream.Read(bitmapBytes, 0, bitmapBytes.Length);
            return bitmapBytes;
        }

       
        #endregion
    }


}
