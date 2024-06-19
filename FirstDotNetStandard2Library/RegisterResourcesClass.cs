using System.Reflection;

namespace FirstDotNetStandard2Library {
    public static class RegisterResourcesClass {

        /// <summary>
        /// Register the assembly with the resource factory.
        /// </summary>
        static RegisterResourcesClass() {
            // Viewing
            ResourceFactory.RegisterResourceAssembly(
                Assembly.GetAssembly(typeof(RegisterResourcesClass)));
        }

        /// <summary>
        /// Make sure the Viewing assembly is registered with the icon factory.
        /// </summary>
        public static void RegisterResources() {
            // Calling this method will trigger the static constructor to be called, which does the actual work.
        }

    }
}
