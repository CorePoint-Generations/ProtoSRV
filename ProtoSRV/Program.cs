using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management.Automation;

namespace ProtoSRV
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            using (PowerShell PowerShellInst = PowerShell.Create())
            {
                string criteria = "system*";
                PowerShellInst.AddScript("Get-Service -DisplayName " + criteria);
                System.Collections.ObjectModel.Collection<PSObject> PSOutput = PowerShellInst.Invoke();
                foreach (PSObject obj in PSOutput)
                {
                    if (obj != null)
                    {
                        Console.Write(obj.Properties["Status"].Value.ToString() + " - ");
                        Console.WriteLine(obj.Properties["DisplayName"].Value.ToString());
                    }
                }
                Console.WriteLine("Done");
                Console.Read();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ProtoSRV());
        }
    }
}
