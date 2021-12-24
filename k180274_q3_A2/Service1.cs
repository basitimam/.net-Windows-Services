using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Timers;

namespace k180274_q3_A2
{
    public partial class Service1 : ServiceBase
    {

       public System.Timers.Timer timer = new System.Timers.Timer();
        public Service1()
        {
            InitializeComponent();
        }

        public string FileMonitorPath = ConfigurationManager.AppSettings["FilePath"];
        public string BackupFolder = ConfigurationManager.AppSettings["BackupFolder"];
        public string LogPath = ConfigurationManager.AppSettings["LogPath"];
        public string LogFile = ConfigurationManager.AppSettings["LogFile"];

        List<string> previousState = new List<string>();
        List<string> currentState = new List<string>();



        protected override void OnStart(string[] args)
        {
            WriteToFile("Service Started");
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 60000;
            timer.Enabled = true;
            timer.Start();

            
        }



    

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {

            WriteToFile("Service Elapsing after interval");
            Boolean flag = false;
            if (Directory.Exists(FileMonitorPath))
            {

                string[] files = Directory.GetFiles(FileMonitorPath);
                if (previousState.Count == 0)
                {
                    foreach (string file in files)
                    {
                        DateTime lastModified = File.GetLastWriteTime(file);
                        string[] data = { file, lastModified.ToString("dd/MM/yy HH:mm:ss") };

                        previousState.AddRange(data);
                        currentState.AddRange(data);
                    }


                }

                else
                {
                    List<string> temp = new List<string>();

                    foreach (string file in files)
                    {
                        DateTime lastModified = File.GetLastWriteTime(file);
                        string[] data = { file, lastModified.ToString("dd/MM/yy HH:mm:ss") };

                        temp.AddRange(data);
                        

                    }
                   

                    for(int i = 0; i < temp.Count; i += 2)
                    {
                        for(int j = 0; j < currentState.Count; j += 2)
                        {
                            if (temp[i] == currentState[j])
                            {
                                WriteToFile(temp[i]);
                                WriteToFile(currentState[j]);
                                if (temp[i + 1] == currentState[j + 1])
                                {
                                    break;
                                }
                                else
                                {

                                    flag = true;
                               
                                    CopyFiles(temp[i]);
                                    break;
                                }
                            }
                         
                            else
                            {
                                continue;
                            }
                        }
                    }

                    for(int i = 0; i<temp.Count; i+=2)
                    {
                        if (currentState.Contains(temp[i]))
                        {
                           
                            continue;
                        }
                        else
                        {
                            flag = true;
                        
                            CopyFiles(temp[i]);

                        }
                    }
                    if (flag == false)
                    {
                        if (timer.Interval < 3600000)
                        {
                            timer.Interval += 120000;
                        }
                        else
                        {
                            timer.Interval = 60000;
                        }
                    }

                    previousState.Clear();
                    for(int i = 0; i < currentState.Count; i++)
                    {
                        previousState[i] = currentState[i];
                    }

                    currentState.Clear();

                    for(int i = 0; i < temp.Count; i++)
                    {
                        currentState[i] = temp[i];
                    }






                }
            }



            else
            {
                Directory.CreateDirectory(FileMonitorPath);


            }

        }

        private void WriteToFile(string message)
        {
            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }
            if (!File.Exists(LogFile))
            {
                using(StreamWriter sw = File.CreateText(LogFile))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using(StreamWriter sw = File.AppendText(LogFile))
                {
                    sw.WriteLine(message);
                }
            }


        }
        private void CopyFiles(string file)
        {
            string[] name = file.Split('\\');
            if (Directory.Exists(BackupFolder))
            {
                File.Copy(file, BackupFolder + '\\' + name[name.Length - 1], true);
            }
            else
            {
                Directory.CreateDirectory(BackupFolder);
                File.Copy(file, BackupFolder + '\\' + name[name.Length - 1], true);


            }
            

        }

        protected override void OnStop()
        {
            timer.Enabled = false;
            WriteToFile("Service Stopped");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}

