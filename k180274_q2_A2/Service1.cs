using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using CodeHollow.FeedReader;

namespace k180274_q2_A2
{

    public class NewsItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PublishedDate { get; set; }
        public string NewsChannel { get; set; }


    }
    public partial class Service1 : ServiceBase
    {
        List<NewsItem> newsItem1 = new List<NewsItem>();
        List<NewsItem> newsItem2 = new List<NewsItem>();
        Timer watch = new Timer();
        public string fileLocation1 = ConfigurationManager.AppSettings["XmlPath"];
        public string fileLocation2;
        public string fileLocation3;
        public string url1 = ConfigurationManager.AppSettings["NewsLink1"];
        public string url2 = ConfigurationManager.AppSettings["NewsLink2"];
        public Service1()
        {
            InitializeComponent();
        }

        private void Reader(object obj, EventArgs e)
        {

            fileLocation2 = fileLocation1 + "Output1.xml";
            fileLocation3 = fileLocation1 + "Output2.xml";

            newsItem1.Clear();
            newsItem2.Clear();


            if (File.Exists(fileLocation2))
            {

                var feedreader1 = FeedReader.ReadAsync(url1);


                foreach (var item in feedreader1.Result.Items)
                {
                    NewsItem obj1 = new NewsItem();
                    obj1.Title = item.Title;
                    obj1.Description = item.Description;
                    obj1.PublishedDate = item.PublishingDateString;
                    obj1.NewsChannel = feedreader1.Result.Title;
                    newsItem1.Add(obj1);
                }



                XmlSerializer xmlserailize1 = new XmlSerializer(typeof(List<NewsItem>));
                FileStream fileobj1 = new FileStream(fileLocation2, FileMode.Open, FileAccess.Write);
                xmlserailize1.Serialize(fileobj1, newsItem1);
                fileobj1.Close();


            }
            else
            {

                File.Create(fileLocation2);

                var feedreader2 = FeedReader.ReadAsync(url1);

                foreach (var item in feedreader2.Result.Items)
                {
                    NewsItem obj2 = new NewsItem();
                    obj2.Title = item.Title;
                    obj2.Description = item.Description;
                    obj2.PublishedDate = item.PublishingDateString;
                    obj2.NewsChannel = feedreader2.Result.Title;
                    newsItem1.Add(obj2);
                }


                XmlSerializer xmlserailize2 = new XmlSerializer(typeof(List<NewsItem>));
                FileStream fileObj2 = new FileStream(fileLocation2, FileMode.Open, FileAccess.Write);
                xmlserailize2.Serialize(fileObj2, newsItem1);
                fileObj2.Close();


            }


            if (File.Exists(fileLocation3))
            {

                var feedreader3 = FeedReader.ReadAsync(url2);


                foreach (var item in feedreader3.Result.Items)
                {
                    NewsItem obj3 = new NewsItem();
                    obj3.Title = item.Title;
                    obj3.Description = item.Description;
                    obj3.PublishedDate = item.PublishingDateString;
                    obj3.NewsChannel = feedreader3.Result.Title;
                    newsItem2.Add(obj3);
                }


                XmlSerializer xmlserailize3 = new XmlSerializer(typeof(List<NewsItem>));
                FileStream fileobj3 = new FileStream(fileLocation3, FileMode.Open, FileAccess.Write);
                xmlserailize3.Serialize(fileobj3, newsItem2);
                fileobj3.Close();


            }
            else
            {

                File.Create(fileLocation3);

                var feedreader4 = FeedReader.ReadAsync(url2);

                foreach (var item in feedreader4.Result.Items)
                {
                    NewsItem obj4 = new NewsItem();
                    obj4.NewsChannel = feedreader4.Result.Title;
                    obj4.Title = item.Title;
                    obj4.PublishedDate = item.PublishingDateString;
                    obj4.Description = item.Description;
                    newsItem2.Add(obj4);
                }


                XmlSerializer xmlserailize4 = new XmlSerializer(typeof(List<NewsItem>));
                FileStream fileobj4 = new FileStream(fileLocation3, FileMode.Open, FileAccess.Write);
                xmlserailize4.Serialize(fileobj4, newsItem2);
                fileobj4.Close();


            }
        }

        protected override void OnStart(string[] args)
        {

            fileLocation2 = fileLocation1 + "Output1.xml";
            fileLocation3 = fileLocation1 + "Output2.xml";

            watch.Elapsed += new ElapsedEventHandler(Reader);
            watch.Interval = 300000;
            watch.Enabled = true;

            newsItem1.Clear();
            newsItem2.Clear();


            if (File.Exists(fileLocation2))
            {

                var feedreader1 = FeedReader.ReadAsync(url1);


                foreach (var item in feedreader1.Result.Items)
                {
                    NewsItem obj1 = new NewsItem();
                    obj1.Title = item.Title;
                    obj1.Description = item.Description;
                    obj1.PublishedDate = item.PublishingDateString;
                    obj1.NewsChannel = feedreader1.Result.Title;
                    newsItem1.Add(obj1);
                }



                XmlSerializer xmlserailize1 = new XmlSerializer(typeof(List<NewsItem>));
                FileStream fileobj1 = new FileStream(fileLocation2, FileMode.Open, FileAccess.Write);
                xmlserailize1.Serialize(fileobj1, newsItem1);
                fileobj1.Close();

            }
            else
            {

                File.Create(fileLocation2);

                var feedreader2 = FeedReader.ReadAsync(url1);

                foreach (var item in feedreader2.Result.Items)
                {
                    NewsItem obj2 = new NewsItem();
                    obj2.Title = item.Title;
                    obj2.Description = item.Description;
                    obj2.PublishedDate = item.PublishingDateString;
                    obj2.NewsChannel = feedreader2.Result.Title;
                    newsItem1.Add(obj2);
                }


                XmlSerializer xmlserailize2 = new XmlSerializer(typeof(List<NewsItem>));
                FileStream fileObj2 = new FileStream(fileLocation2, FileMode.Open, FileAccess.Write);
                xmlserailize2.Serialize(fileObj2, newsItem1);
                fileObj2.Close();



            }


            if (File.Exists(fileLocation3))
            {

                var feedreader3 = FeedReader.ReadAsync(url2);


                foreach (var item in feedreader3.Result.Items)
                {
                    NewsItem obj3 = new NewsItem();
                    obj3.Title = item.Title;
                    obj3.Description = item.Description;
                    obj3.PublishedDate = item.PublishingDateString;
                    obj3.NewsChannel = feedreader3.Result.Title;
                    newsItem2.Add(obj3);
                }


                XmlSerializer xmlserailize3 = new XmlSerializer(typeof(List<NewsItem>));
                FileStream fileobj3 = new FileStream(fileLocation3, FileMode.Open, FileAccess.Write);
                xmlserailize3.Serialize(fileobj3, newsItem2);
                fileobj3.Close();


            }
            else
            {


                File.Create(fileLocation3);

                var feedreader4 = FeedReader.ReadAsync(url2);

                foreach (var item in feedreader4.Result.Items)
                {
                    NewsItem obj4 = new NewsItem();
                    obj4.NewsChannel = feedreader4.Result.Title;
                    obj4.Title = item.Title;
                    obj4.PublishedDate = item.PublishingDateString;
                    obj4.Description = item.Description;
                    newsItem2.Add(obj4);
                }


                XmlSerializer xmlserailize4 = new XmlSerializer(typeof(List<NewsItem>));
                FileStream fileobj4 = new FileStream(fileLocation3, FileMode.Open, FileAccess.Write);
                xmlserailize4.Serialize(fileobj4, newsItem2);
                fileobj4.Close();


            }
        }

        protected override void OnStop()
        {
        }
    }
}
