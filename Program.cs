using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
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


    // Example 3
    public class OrderEventArgs : EventArgs
    {
        public int OrderID { get; }
        public int OrderTotalPrice { get; }
        public string ClientEmail { get; }

        public OrderEventArgs(int OrderID, int OrderTotalPrice, string ClientEmail)
        {
            this.OrderID = OrderID;
            this.OrderTotalPrice = OrderTotalPrice;
            this.ClientEmail = ClientEmail;
        }
    }
    public class Order
    {
        public event EventHandler<OrderEventArgs> OnOrderCreated;

        public void Create(int OrderID, int OrderTotalPrice, string ClientEmail)
        {
            Console.WriteLine("New Order Created, now will notify everyone by rasing the event\n");

            if (OnOrderCreated != null)
            {
                OnOrderCreated(this, new OrderEventArgs(OrderID, OrderTotalPrice, ClientEmail));
            }
        }
    }
    public class EmailService
    {
        public void Subscribe(Order order)
        {
            order.OnOrderCreated += HandleNewOrder;
        }

        public void UnSubscribe(Order order)
        {
            order.OnOrderCreated -= HandleNewOrder;
        }

        public void HandleNewOrder(Object sender, OrderEventArgs e)
        {
            Console.WriteLine("------------Email Service------------");
            Console.WriteLine("Email Service object receibed a new order evetn");
            Console.WriteLine("Order ID:" + e.OrderID);
            Console.WriteLine("Order Price:" + e.OrderTotalPrice);
            Console.WriteLine("Email:" + e.ClientEmail);
            Console.WriteLine("Send Email");
            Console.WriteLine("-------------------------------------");

            // Here I can write the code of send email

            Console.WriteLine();
        }
    }
    public class SMSService
    {
        public void Subscribe(Order order)
        {
            order.OnOrderCreated += HandleNewOrder;
        }

        public void UnSubscribe(Order order)
        {
            order.OnOrderCreated -= HandleNewOrder;
        }

        public void HandleNewOrder(Object sender, OrderEventArgs e)
        {
            Console.WriteLine("------------SMS Service------------");
            Console.WriteLine("SMS Service object receibed a new order evetn");
            Console.WriteLine("Order ID:" + e.OrderID);
            Console.WriteLine("Order Price:" + e.OrderTotalPrice);
            Console.WriteLine("Email:" + e.ClientEmail);
            Console.WriteLine("Send SMS");
            Console.WriteLine("-------------------------------------");

            // Here I can write the code of send sms

            Console.WriteLine();
        }
    }
    public class ShippingService
    {
        public void Subscribe(Order order)
        {
            order.OnOrderCreated += HandleNewOrder;
        }

        public void UnSubscribe(Order order)
        {
            order.OnOrderCreated -= HandleNewOrder;
        }

        public void HandleNewOrder(Object sender, OrderEventArgs e)
        {
            Console.WriteLine("------------Shipping Service------------");
            Console.WriteLine("Shipping Service object receibed a new order evetn");
            Console.WriteLine("Order ID:" + e.OrderID);
            Console.WriteLine("Order Price:" + e.OrderTotalPrice);
            Console.WriteLine("Email:" + e.ClientEmail);
            Console.WriteLine("Send Shipping");
            Console.WriteLine("-------------------------------------");

            // Here I can write the code of send shipping

            Console.WriteLine();
        }
    }



    // ------------------- Delegates -------------------
    
    // Example 1, Logger
    public class Logger
    {
        public delegate void LogAction(string message);
        private LogAction _LogAction;

        public Logger(LogAction action)
        {
            _LogAction = action;
        }

        public void Log(string message)
        {
            _LogAction(message);
        }
    }


    public class Program
    {
        // Example 2, Func Delegate
        static int SquareMethod(int x)
        {
            return x * x;
        }

        static Func<int, int> square = SquareMethod;

        static void Main(string[] args)
        {
            int result = square(5);

            Console.WriteLine(result);
        }
    }
}