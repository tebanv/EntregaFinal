using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace EntregaFinal.Skia.Tizen
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new TizenHost(() => new EntregaFinal.App(), args);
            host.Run();
        }
    }
}
