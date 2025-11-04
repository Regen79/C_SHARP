using System;

public class bankaccount
{
    public decimal saldo { get; private set; }
    public string wlasciciel;


    public bankaccount(string wlasciciel, decimal saldo)
    {
        this.wlasciciel = wlasciciel;
        this.saldo = saldo;

    }
    public void wplata(decimal kwota)
    {

        saldo = saldo + kwota;
        Console.WriteLine("Twoje saldo: " + saldo);
    }


    public void wyplata(decimal kwota)
    {


        if (kwota > saldo)
        {
            Console.WriteLine("Za malo srodkow na koncie");
            return;
        }
        else
        {
            saldo = saldo - kwota;
        }
        Console.WriteLine("Twoje saldo: " + saldo);
    }
}

public class program

{
    static void Main()
    {
        bankaccount konto = new bankaccount("Jan kowalski", 2000);
        konto.wyplata(1000);
        konto.wplata(5000);
    }

}


