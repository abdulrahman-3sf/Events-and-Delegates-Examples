using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // Example 1
    public class TemperatureChangedEventArgs : EventArgs
    {
        public double OldTemperature { get; }
        public double NewTemperature { get; }
        public double Difference { get; }

        public TemperatureChangedEventArgs(double OldTemperature, double NewTemperature)
        {
            this.OldTemperature = OldTemperature;
            this.NewTemperature = NewTemperature;
            Difference = NewTemperature - OldTemperature;
        }
    }
    public class Thermostat
    {
        public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged;

        private double OldTemperature;
        private double CurrentTemperature;

        public void SetTemperature(double NewTemperature)
        {
            if (NewTemperature != CurrentTemperature)
            {
                OldTemperature = CurrentTemperature;
                CurrentTemperature = NewTemperature;
                OnTemperatureChanged(OldTemperature, CurrentTemperature);
            }
        }

        public void OnTemperatureChanged(double OldTemperature, double CurrentTemperature)
        {
            OnTemperatureChanged(new TemperatureChangedEventArgs(OldTemperature, CurrentTemperature));
        }

        public virtual void OnTemperatureChanged(TemperatureChangedEventArgs e)
        {
            TemperatureChanged?.Invoke(this, e);
        }
    }
    public class Display
    {
        public void Subscribe(Thermostat thermostat)
        {
            thermostat.TemperatureChanged += HandleTemperatureChange;
        }

        public void HandleTemperatureChange(object sender, TemperatureChangedEventArgs e)
        {
            Console.WriteLine("\n\nTemperature Changed:");
            Console.WriteLine("Temperature Changed from: " + e.OldTemperature);
            Console.WriteLine("Temperature Changed to: " + e.NewTemperature);
            Console.WriteLine("Temperature Differance: " + e.Difference);
        }
    }


    // Example 2
    public class NewsArticle
    {
        public string Title { get; }
        public string Content { get; }

        public NewsArticle(string Title, string Content)
        {
            this.Title = Title;
            this.Content = Content;
        }
    }
    public class NewsPublisher
    {
        public event EventHandler<NewsArticle> NewNewsPublished;

        public void PublishNews(string Title, string Content)
        {
            NewsArticle Article = new NewsArticle(Title, Content);

            OnNewNewsPublished(Article);
        }

        public virtual void OnNewNewsPublished(NewsArticle Article)
        {
            NewNewsPublished?.Invoke(this, Article);
        }
    }
    public class NewsSubscriber
    {
        public string Name { get; }

        public NewsSubscriber(string Name)
        {
            this.Name = Name;
        }

        public void Subscribe(NewsPublisher publisher)
        {
            publisher.NewNewsPublished += HandleNewNews;
        }

        public void UnSubscribe(NewsPublisher publisher)
        {
            publisher.NewNewsPublished -= HandleNewNews;
        }

        public void HandleNewNews(object sender, NewsArticle article)
        {
            Console.WriteLine($"{Name} received a new news article:");
            Console.WriteLine($"Title: {article.Title}");
            Console.WriteLine($"Content: {article.Content}");
            Console.WriteLine();
        }
    }


    public class Program
    {
        static void Main(string[] args)
        {
            NewsPublisher publisher = new NewsPublisher();
            NewsSubscriber subscriber1 = new NewsSubscriber("Sub1");
            NewsSubscriber subscriber2 = new NewsSubscriber("Sub2");

            subscriber1.Subscribe(publisher);
            subscriber2.Subscribe(publisher);

            publisher.PublishNews("News", "Some text here nothing else");

            subscriber1.UnSubscribe(publisher);

            publisher.PublishNews("News2", "Some text here nothing else");

            subscriber2.UnSubscribe(publisher);

            publisher.PublishNews("News3", "Some text here nothing else");
        }
    }
}
