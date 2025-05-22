using System;

namespace MediatorPattern
{
    // Mediator Interface
    interface IATCMediator
    {
        void RegisterRunway(Runway runway);
        void RegisterFlight(Flight flight);
        bool IsLandingOk();
        void SetLandingStatus(bool status);
    }

    // Concrete Mediator
    class ATCMediator : IATCMediator
    {
        private Flight flight;
        private Runway runway;
        private bool land;

        public void RegisterRunway(Runway runway)
        {
            this.runway = runway;
        }

        public void RegisterFlight(Flight flight)
        {
            this.flight = flight;
        }

        public bool IsLandingOk()
        {
            return land;
        }

        public void SetLandingStatus(bool status)
        {
            land = status;
        }
    }

    // Command Interface
    interface ICommand
    {
        void Land();
    }

    // Colleague - Flight
    class Flight : ICommand
    {
        private IATCMediator atcMediator;

        public Flight(IATCMediator atcMediator)
        {
            this.atcMediator = atcMediator;
        }

        public void Land()
        {
            if (atcMediator.IsLandingOk())
            {
                Console.WriteLine("Successfully Landed.");
                atcMediator.SetLandingStatus(true);
            }
            else
            {
                Console.WriteLine("Waiting for landing.");
            }
        }

        public void GetReady()
        {
            Console.WriteLine("Ready for landing.");
        }
    }

    // Colleague - Runway
    class Runway : ICommand
    {
        private IATCMediator atcMediator;

        public Runway(IATCMediator atcMediator)
        {
            this.atcMediator = atcMediator;
            atcMediator.SetLandingStatus(true); // Grant permission immediately
        }

        public void Land()
        {
            Console.WriteLine("Landing permission granted.");
            atcMediator.SetLandingStatus(true);
        }
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            IATCMediator atcMediator = new ATCMediator();

            Flight sparrow101 = new Flight(atcMediator);
            Runway mainRunway = new Runway(atcMediator);

            atcMediator.RegisterFlight(sparrow101);
            atcMediator.RegisterRunway(mainRunway);

            sparrow101.GetReady();
            mainRunway.Land();
            sparrow101.Land();

            Console.ReadLine(); // Keep console window open
        }
    }
}
