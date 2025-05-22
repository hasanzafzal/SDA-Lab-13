using System;

namespace NotificationIteratorPattern
{
    // A simple Notification class
    class Notification
    {
        private string notification;

        public Notification(string notification)
        {
            this.notification = notification;
        }

        public string GetNotification()
        {
            return notification;
        }
    }

    // Iterator interface
    interface IIterator
    {
        bool HasNext();
        object Next();
    }

    // Collection interface
    interface ICollection
    {
        IIterator CreateIterator();
    }

    // Collection of notifications
    class NotificationCollection : ICollection
    {
        private static readonly int MAX_ITEMS = 6;
        private int numberOfItems = 0;
        private Notification[] notificationList;

        public NotificationCollection()
        {
            notificationList = new Notification[MAX_ITEMS];

            // Add some dummy notifications
            AddItem("Notification 1");
            AddItem("Notification 2");
            AddItem("Notification 3");
        }

        public void AddItem(string str)
        {
            Notification notification = new Notification(str);
            if (numberOfItems >= MAX_ITEMS)
                Console.Error.WriteLine("Full");
            else
            {
                notificationList[numberOfItems] = notification;
                numberOfItems++;
            }
        }

        public IIterator CreateIterator()
        {
            return new NotificationIterator(notificationList);
        }
    }

    // Notification iterator
    class NotificationIterator : IIterator
    {
        private Notification[] notificationList;
        private int pos = 0;

        public NotificationIterator(Notification[] notificationList)
        {
            this.notificationList = notificationList;
        }

        public bool HasNext()
        {
            return pos < notificationList.Length && notificationList[pos] != null;
        }

        public object Next()
        {
            Notification notification = notificationList[pos];
            pos++;
            return notification;
        }
    }

    // Contains collection of notifications
    class NotificationBar
    {
        private NotificationCollection notifications;

        public NotificationBar(NotificationCollection notifications)
        {
            this.notifications = notifications;
        }

        public void PrintNotifications()
        {
            IIterator iterator = notifications.CreateIterator();
            Console.WriteLine("-------NOTIFICATION BAR------------");
            while (iterator.HasNext())
            {
                Notification n = (Notification)iterator.Next();
                Console.WriteLine(n.GetNotification());
            }
        }
    }

    // Main driver
    class Program
    {
        static void Main(string[] args)
        {
            NotificationCollection nc = new NotificationCollection();
            NotificationBar nb = new NotificationBar(nc);
            nb.PrintNotifications();

            Console.ReadLine(); // To keep console open
        }
    }
}
