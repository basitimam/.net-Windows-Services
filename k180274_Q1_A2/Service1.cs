using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System.Timers;


namespace k180274_Q1_A2
{
    public partial class Service1 : ServiceBase
    {
        public System.Timers.Timer timer2 = new System.Timers.Timer();
        public Service1()
        {
            InitializeComponent();
        }

        public string jsonPath = ConfigurationManager.AppSettings["jsonFiles"].ToString();
        public string password = ConfigurationManager.AppSettings["password"].ToString();
        public string logPath = ConfigurationManager.AppSettings["logPath"].ToString();
        public string logFile = ConfigurationManager.AppSettings["logFile"].ToString();
        public string email = ConfigurationManager.AppSettings["email"].ToString();


        protected override void OnStart(string[] args)
        {
            timer2.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer2.Interval = 60000;
            timer2.Enabled = true;
            timer2.Start();



        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {

            if (Directory.Exists(jsonPath))
            {

                string[] jsonFiles = Directory.GetFiles(jsonPath, ".json");

                if (jsonFiles.Length != 0)
                {



                    MailMessage EmailService = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    List<string> jsonDetails = new List<string>();

                    EmailService.From = new MailAddress(email);

                    foreach (string json in jsonFiles)
                    {
                        JObject jsonInfo = JObject.Parse(File.ReadAllText(json));
                        foreach (JProperty props in jsonInfo.Properties())
                        {
                            jsonDetails.Add(props.Value.ToString());
                        }

                        EmailService.To.Add(new MailAddress(jsonDetails[0]));
                        EmailService.Subject = jsonDetails[1];
                        EmailService.Body = jsonDetails[2];
                        EmailService.IsBodyHtml = true;
                        SmtpServer.Port = 465;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(email, password);
                        SmtpServer.EnableSsl = true;

                        SmtpServer.Send(EmailService);
                    }

                }
                else
                {
                    Console.WriteLine("Empty Directory");
                }
            }
            else
            {
                Directory.CreateDirectory(jsonPath);


            }



        }

       
        protected override void OnStop()
        {
            timer2.Enabled = false;
            
        }

        public void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

        }
    }
}
  

