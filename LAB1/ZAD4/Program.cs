using System;

public class licz
{
    private double value;
    public void stan()
    {
        Console.WriteLine("Aktualna wartosc: " + value);
    }


    public void dodaj(double param)
    {
        Console.WriteLine("Wartosc przed operacja: " + value);
        value += param;
        Console.WriteLine("Wartosc po dodaniu: " + param + " jest rowna: " + value);
    }
    public void odejmij(double param)
    {
        Console.WriteLine("Wartosc " + value);
        value -= param;
        Console.WriteLine("Wartosc po odjeciu: " + param + " jest rowna: " + value);
    }
    public licz(double value)
    {
        this.value = value;
    }
}

public class program
{
    static void Main()
    {
        licz licz1 = new licz(46);
        licz1.dodaj(72);
        licz1.odejmij(38);
        licz licz2 = new licz(12343);
        licz2.dodaj(34456);
        licz2.odejmij(1423543645);
        licz1.stan();
        licz2.stan();

    }
}