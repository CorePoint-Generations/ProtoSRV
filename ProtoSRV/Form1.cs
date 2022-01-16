﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Management.Automation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Sql;
using System.Diagnostics;
using System.Management;
using System.Threading;
using System.Management.Automation.Runspaces;

namespace ProtoSRV
{

    public partial class ProtoSRV : Form
    {
        public string cmdOutput;

        public ProtoSRV()
        {
            InitializeComponent();
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = @"c:\_.PROTOMRCX\res\_.WINDOWSOSX\cmd";

            if (!Directory.Exists(path) & !Directory.Exists(path))
            {
                string dir = @"c:\_.PROTOMRCX\res\_.WINDOWSOSX\cmd";

                DirectoryInfo di = Directory.CreateDirectory(dir);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

                int ret = RandomNumber(0, 2147483647);
                string path0 = path + "_tmp.rki";
                string cmdAdder = "datset:\n    default:\n        cliname: 'protocli'\n        cliproc: 'rki_a9-64x'\n        clikey: 'fad93po49k3vik0-596gh49t4mv04ms-94nv93mf0wmvbwobc-58tn49vn'\ndatapi:\n    default:\n        ip: 'localhost'\n        port: '65565'\n        ret: '" + ret + "'";
                System.IO.File.WriteAllText(path0, cmdAdder);
            }
            else if (Directory.Exists(path) & Directory.Exists(path))
            {
                int ret = RandomNumber(0, 2147483647);
                string path0 = path + "_tmp.rki";
                string cmdAdder = "datset:\n    default:\n        cliname: 'protocli'\n        cliproc: 'rki_a9-64x'\n        clikey: 'fad93po49k3vik0-596gh49t4mv04ms-94nv93mf0wmvbwobc-58tn49vn'\ndatapi:\n    default:\n        ip: 'localhost'\n        port: '65565'\n        ret: '" + ret + "'";
                System.IO.File.WriteAllText(path0, cmdAdder);
            }
        }

        private void ProtoSRV_FormClosing(object sender, FormClosingEventArgs e)
        { 
            string filepath = @"c:\_.PROTOMRCX\res\_.WINDOWSOSX\cmd_tmp.rki";

            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

            System.Diagnostics.Process.Start("rasdial.exe", "ProtoSRV-VPN /d");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            label4.Visible = false;
            listBox1.Visible = true;
            textBox3.Visible = false;
            label3.Visible = true;
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button1.Enabled = false;
            button1.Text = "Connecting to CLI...";
            checkBox1.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;

            if (checkBox1.Checked)
            {
                //setIP1("10.10.28.30", "255.255.255.0", "10.10.28.1");
                //  return;

                ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection objMOC = objMC.GetInstances();

                int x = 1;
                foreach (ManagementObject objMO in objMOC)
                {

                    //  Response.Write("</tr>");
                    // txtObjvalue.Text = objMO["IPEnabled"].ToString();
                    // Response.Write(objMO["IPEnabled"].ToString() + "<br />");
                    x++;
                    if ((bool)objMO["IPEnabled"])
                    {

                        ManagementBaseObject setIP;
                        ManagementBaseObject newIP =
                            objMO.GetMethodParameters("EnableStatic");

                        string ip = "10.10.28.30";
                        string subnet = "255.255.255.0";
                        string gateway = "10.10.28.1";
                        newIP["IPAddress"] = new string[] { ip.Trim() };
                        newIP["SubnetMask"] = new string[] { subnet.Trim() };

                        setIP = objMO.InvokeMethod("EnableStatic", newIP, null);
                        System.Diagnostics.Process.Start("rasdial.exe", "ProtoSRV-VPN ProtoSRV ProtoSRV");
                    }

                }
            }
            else if (!checkBox1.Checked)
            {
                System.Diagnostics.Process.Start("rasdial.exe", "ProtoSRV-VPN /d");
            }

            progressBar1.Maximum = 100;
            int progress = 0;

            while (progressBar1.Value < 100)
            {
                await Task.Delay(500);
                if (progress >= 95)
                {
                    progress = progress + (100 - progress);
                }
                else
                {
                    int ret = RandomNumber(1, 5);
                    progress = progress + ret;
                    label3.Text = "Loading... (" + progressBar1.Value + "%)";
                    Application.DoEvents();
                    webBrowser1.Navigate(label1.Text + ":" + label2.Text);
                    if (progressBar1.Value < 19 && checkBox1.Checked)
                    {
                        DateTime now = DateTime.Now;
                        listBox1.Items.Add("[" + now + " INFO]: Starting VPN");
                    }
                    else if (progressBar1.Value < 19 && !checkBox1.Checked)
                    {
                        DateTime now = DateTime.Now;
                        listBox1.Items.Add("[" + now + " INFO]: Preparing");
                    }
                    else if (progressBar1.Value < 39)
                    {
                        DateTime now = DateTime.Now;
                        listBox1.Items.Add("[" + now + " INFO]: Connecting to host IP & port");
                    }
                    else if (progressBar1.Value < 69)
                    {
                        DateTime now = DateTime.Now;
                        listBox1.Items.Add("[" + now + " INFO]: Clearing cache");
                    }
                    else if (progressBar1.Value < 79)
                    {
                        DateTime now = DateTime.Now;
                        listBox1.Items.Add("[" + now + " INFO]: Launching command line");
                    }
                    else if (progressBar1.Value < 86)
                    {
                        DateTime now = DateTime.Now;
                        listBox1.Items.Add("[" + now + " INFO]: Fetching PKI Services");
                    }
                    else if (progressBar1.Value < 94)
                    {
                        DateTime now = DateTime.Now;
                        listBox1.Items.Add("[" + now + " INFO]: Enabling commands");
                    }
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    listBox1.SelectedIndex = -1;
                }

                progressBar1.Value = progress;

                if (progressBar1.Value == 100)
                {
                    progressBar1.Visible = false;
                    label3.Visible = false;
                    label4.Visible = true;
                    textBox3.Visible = true;
                    listBox1.Visible = true;
                    button2.Visible = true;
                    button3.Visible = true;
                    button4.Visible = true;
                    button5.Visible = true;
                    button6.Visible = true;
                    button1.Enabled = true;
                    button1.Text = "Connect to CLI";
                    checkBox1.Enabled = true;
                    textBox1.Enabled = true;
                    textBox2.Enabled = true;
                    DateTime now = DateTime.Now;
                    listBox1.Items.Add("[" + now + " INFO]: CLI started.");
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    listBox1.SelectedIndex = -1;
                }
            }
            

        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length < 2)
            {
                textBox3.Text = "# ";
                textBox3.Select(textBox3.Text.Length, 0);
            }
            else if (!(textBox3.Text.StartsWith("# ")))
            {
                textBox3.Text = "# ";
                textBox3.Select(textBox3.Text.Length, 0);
            }
        }

        private string RunScript(string script)
        {
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            Pipeline pip = runspace.CreatePipeline();
            pip.Commands.AddScript(script);
            pip.Commands.Add("Out-String");
            Collection<PSObject> results = pip.Invoke();
            runspace.Close();
            StringBuilder strB = new StringBuilder();
            foreach (PSObject pSObject in results)
                strB.AppendLine(pSObject.ToString());
            return strB.ToString();
            
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cmd = textBox3.Text;
                if (cmd.StartsWith("# msg "))
                {
                    string message = cmd.Replace("# msg ", "");
                    string pcName = System.Environment.MachineName;
                    DateTime now = DateTime.Now;
                    listBox1.Items.Add("[Message from " + pcName + " at " + now + "]: " + message);
                    textBox3.Text = "# ";
                    textBox3.Select(textBox3.Text.Length, 0);
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    listBox1.SelectedIndex = -1;
                    return;
                }
                else if (cmd.StartsWith("# client "))
                {
                    string cmd2 = cmd.Replace("# client ", "");
                    if (cmd2.StartsWith("-run "))
                    {
                        string cmd3 = cmd2.Replace("-run ", "");
                        if (cmd3.StartsWith("\\powershell.exe "))
                        {
                            string cmd4 = cmd3.Replace("\\powershell.exe ", "");
                            string Output = RunScript(cmd4);
                            DateTime now = DateTime.Now;
                            listBox1.Items.Add("[" + now + " INFO]: Executed \"" + cmd4 + "\" in powershell.exe");
                            Form2 output = new Form2(Output);
                            output.ShowDialog();
                            textBox3.Text = "# ";
                            textBox3.Select(textBox3.Text.Length, 0);
                            listBox1.SelectedIndex = listBox1.Items.Count - 1;
                            listBox1.SelectedIndex = -1;
                            return;
                        }
                    }
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    listBox1.SelectedIndex = -1;
                    return;
                }
                else if (cmd == "# exit")
                {
                    RunScript("taskkill /f /im ProtoSRV.exe");
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    listBox1.SelectedIndex = -1;
                }
                else if (cmd == "# cls" || cmd == "# clear")
                {
                    listBox1.Items.Clear();
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    listBox1.SelectedIndex = -1;
                }
                else if (cmd.StartsWith("# process "))
                {
                    string cmd2 = cmd.Replace("# process ", "");
                    if (cmd2.StartsWith("-start "))
                    {
                        string cmd3 = cmd2.Replace("-start ", "");
                        try
                        {
                            Process.Start(cmd3);
                        }
                        catch(Exception exc)
                        {
                            MessageBox.Show(exc.ToString(), "Error Starting Process");
                        }
                        return;
                    }

                    textBox3.Text = "";
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    listBox1.SelectedIndex = -1;
                    return;
                }
                else if (textBox3.Text == "# ")
                {
                    DateTime now = DateTime.Now;
                    listBox1.Items.Add("[" + now + " ERROR]: " + "Command: \"# \"");
                    listBox1.Items.Add("                                                                        ^^^^ command cannot be executed as empty text");
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    listBox1.SelectedIndex = -1;
                    return;
                }
                else
                {
                    DateTime now = DateTime.Now;
                    listBox1.Items.Add("[" + now + " ERROR]: " + "Command: \"" + cmd + "\" cannot be executed: command does not exist");
                    textBox3.Text = "# ";
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    listBox1.SelectedIndex = -1;
                    return;
                }
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                listBox1.SelectedIndex = -1;
                textBox3.Text = "# ";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                listBox1.Items.RemoveAt(listBox1.Items.Count - 1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                listBox1.Items.RemoveAt(listBox1.TopIndex);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Invalidate();
            this.Refresh();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string allItems = string.Join("\n", listBox1.Items.OfType<object>());
            string filePath = @"c:\_.PROTOMRCX\res\_.WINDOWSOSX\cmd\ConsoleLog-" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".txt";
            string fileText = allItems;
            System.IO.File.WriteAllText(filePath, fileText);
            Process.Start(filePath);
            MessageBox.Show("The log has been downloaded.", "Downloaded");
        }
    }
    class NetworkManagement
    {
        /// <summary> 
        /// Set's a new IP Address and it's Submask of the local machine 
        /// </summary> 
        /// <param name="ip_address">The IP Address</param> 
        /// <param name="subnet_mask">The Submask IP Address</param> 
        /// <remarks>Requires a reference to the System.Management namespace</remarks> 
        public void setIP(string ip_address, string subnet_mask)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    try
                    {
                        ManagementBaseObject setIP;
                        ManagementBaseObject newIP =
                            objMO.GetMethodParameters("EnableStatic");

                        newIP["IPAddress"] = new string[] { ip_address };
                        newIP["SubnetMask"] = new string[] { subnet_mask };

                        setIP = objMO.InvokeMethod("EnableStatic", newIP, null);
                    }
                    catch (Exception)
                    {
                        throw;
                    }


                }
            }
        }
        /// <summary> 
        /// Set's a new Gateway address of the local machine 
        /// </summary> 
        /// <param name="gateway">The Gateway IP Address</param> 
        /// <remarks>Requires a reference to the System.Management namespace</remarks> 
        public void setGateway(string gateway)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    try
                    {
                        ManagementBaseObject setGateway;
                        ManagementBaseObject newGateway =
                            objMO.GetMethodParameters("SetGateways");

                        newGateway["DefaultIPGateway"] = new string[] { gateway };
                        newGateway["GatewayCostMetric"] = new int[] { 1 };

                        setGateway = objMO.InvokeMethod("SetGateways", newGateway, null);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }
        /// <summary> 
        /// Set's the DNS Server of the local machine 
        /// </summary> 
        /// <param name="NIC">NIC address</param> 
        /// <param name="DNS">DNS server address</param> 
        /// <remarks>Requires a reference to the System.Management namespace</remarks> 
        public void setDNS(string NIC, string DNS)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    if (objMO["Caption"].Equals(NIC))
                    {
                        try
                        {
                            ManagementBaseObject newDNS =
                                objMO.GetMethodParameters("SetDNSServerSearchOrder");
                            newDNS["DNSServerSearchOrder"] = DNS.Split(',');
                            ManagementBaseObject setDNS =
                                objMO.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
        }
        /// <summary> 
        /// Set's WINS of the local machine 
        /// </summary> 
        /// <param name="NIC">NIC Address</param> 
        /// <param name="priWINS">Primary WINS server address</param> 
        /// <param name="secWINS">Secondary WINS server address</param> 
        /// <remarks>Requires a reference to the System.Management namespace</remarks> 
        public void setWINS(string NIC, string priWINS, string secWINS)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    if (objMO["Caption"].Equals(NIC))
                    {
                        try
                        {
                            ManagementBaseObject setWINS;
                            ManagementBaseObject wins =
                            objMO.GetMethodParameters("SetWINSServer");
                            wins.SetPropertyValue("WINSPrimaryServer", priWINS);
                            wins.SetPropertyValue("WINSSecondaryServer", secWINS);

                            setWINS = objMO.InvokeMethod("SetWINSServer", wins, null);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
        }
    }

    public class ContinuousProgressBar : ProgressBar
    {
        public ContinuousProgressBar()
        {
            this.Style = ProgressBarStyle.Continuous;
        }
        protected override void CreateHandle()
        {
            base.CreateHandle();
            try { SetWindowTheme(this.Handle, "", ""); }
            catch { }
        }
        [System.Runtime.InteropServices.DllImport("uxtheme.dll")]
        private static extern int SetWindowTheme(IntPtr hwnd, string appname, string idlist);
    }

}
