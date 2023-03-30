using System;

namespace dz1_29._03
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        void Handle(Receiver reciever);
    }
    public abstract class PaymentHandler : IHandler
    {
        public IHandler Handler;
        public IHandler SetNext(IHandler handler)
        {
            Handler = handler;
            return handler;
        }

        public virtual void Handle(Receiver reciever)
        {
            if (Handler != null)
                Handler.Handle(reciever);
        }
    }
    public class MoneyPaymentHandler : PaymentHandler
    {
        public override void Handle(Receiver reciever)
        {
            if (reciever.MoneyTransfer)
            {
                Console.WriteLine("Transfer through money transfer systems\n");
                Handler.Handle(reciever);
            }
            else
                Handler.Handle(reciever);
        }
    }
    public class PayPalPaymentHandler : PaymentHandler
    {
        public override void Handle(Receiver reciever)
        {
            if (reciever.PayPalTransfer)
            {
                Console.WriteLine("Transfer via paypal\n");
                Handler.Handle(reciever);
            }
            else
                Handler.Handle(reciever);
        }
    }
    public class BankPaymentHandler : PaymentHandler
    {
        public override void Handle(Receiver reciever)
        {
            if (reciever.BankTransfer)
                Console.WriteLine("Bank transfer\n");
            else
                Handler.Handle(reciever);
        }
    }
    public class Receiver
    {
        public bool BankTransfer { get; set; }
        public bool MoneyTransfer { get; set; }
        public bool PayPalTransfer { get; set; }
        public Receiver(bool bt, bool mt, bool ppt)
        {
            BankTransfer = bt;
            MoneyTransfer = mt;
            PayPalTransfer = ppt;
        }
        public void Request(PaymentHandler h, Receiver reciever)
        {
            h.Handle(reciever);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var money = new MoneyPaymentHandler();
            var pal = new PayPalPaymentHandler();
            var bank = new BankPaymentHandler();
            money.SetNext(pal).SetNext(bank);
            Receiver receiver = new Receiver(true, false, true);
            receiver.Request(pal, receiver);
        }
    }
}
