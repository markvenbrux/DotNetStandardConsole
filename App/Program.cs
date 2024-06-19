using System;
using System.Drawing;
using System.Threading.Tasks;

using FirstDotNetStandard2Library;

using SecondDotNetStandard2Library;

using RegisterResourcesClass1 = FirstDotNetStandard2Library.RegisterResourcesClass;
using RegisterResourcesClass2 = SecondDotNetStandard2Library.RegisterResourcesClass;

static class Program
{
    static async Task Main(string[] args)
    {
        var max = args.Length != 0 ? Convert.ToInt32(args[0]) : -1;

        Class1.LogOs();
        Class1.LogTranslatedStrings();

        // Resource handling:
        //     Image1 is embedded in FirstDotNetStandard2Library,
        //     Image2 is embedded in SecondDotNetStandard2Library.
        // The ResourceFactory is implemented in FirstDotNetStandard2Library.
        // This class supports loading resources from any assembly that has embedded resources.
        // The assemblies must first be registered with the ResourceFactory class.
        // By using the ResourceFactory to load Image2, loading from SecondDotNetStandard2Library is demonstrated.
        // This is locale independent and does not use the ResourceDictionary type.

        // Register resources from both assemblies.
        RegisterResourcesClass1.RegisterResources();
        RegisterResourcesClass2.RegisterResources();

        // Load image1
        var imageBytes1 = ResourceFactory.GetResource("Image1.bmp");
        var bitmap1 = new Bitmap(new System.IO.MemoryStream(imageBytes1));
        bitmap1.Save(@"c:\temp\bitmap1.bmp");

        // Load image2
        var imageBytes2 = ResourceFactory.GetResource("Image2.bmp");
        var bitmap2 = new Bitmap(new System.IO.MemoryStream(imageBytes2));
        bitmap2.Save(@"c:\temp\bitmap2.bmp");


        await Class1.Loop(max);
    }

}
