using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FirstDotNetStandard2Library
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
        /// Register the specified <see cref="Assembly"/> as source of resources.
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
            byte[] resourceBytes = null;
            foreach (var assembly in resourceAssemblies.Keys) {
                resourceBytes = GetResource(assembly, resourceName);
                if (resourceBytes != null) {
                    break;
                }
            }
            return resourceBytes;
        }


        public static byte[] GetResource(Assembly assembly, string resourceName)
        {
            // Get a list of all resources in the assembly
            var resources = assembly.GetManifestResourceNames();
            var fullResourceName = resources.FirstOrDefault(n => n.EndsWith(resourceName));
            if (fullResourceName == null) {
                return null;
            }
            // load the byte array from the resources
            var resourceStream =
                assembly.GetManifestResourceStream(fullResourceName);
            //  convert to byte array
            byte[] resourceBytes = new byte[resourceStream.Length];
            resourceStream.Read(resourceBytes, 0, resourceBytes.Length);
            return resourceBytes;
        }

       
        #endregion
    }


}
