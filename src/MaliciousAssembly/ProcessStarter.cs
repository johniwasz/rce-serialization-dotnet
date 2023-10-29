using System.Diagnostics;

namespace MaliciousAssembly
{
    public class ProcessStarter
    {
        private string processName;

        public ProcessStarter()
        {

        }

        public string ProcessLaunch
        {
            get
            {
                return processName;
            }
            set
            {
                processName = value;
                Process.Start(value);
            }


        }
    }
    }
