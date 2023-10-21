using System.Diagnostics;

namespace MaliciousAssembly
{
    public class ProcessStarter
    {

        static ProcessStarter()
        {
            Process.Start("calc.exe");
        }

    }
}
