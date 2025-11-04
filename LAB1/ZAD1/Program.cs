
using System;



public class osoba
{
    private string imie;
    public string Imie
    {
        get { return imie; }
        set
        {
            if (value.Length >= 2)
            {
                imie = value;
            }
            else
            {
                Console.WriteLine("Za krotka nazwa");
            }
        }
    }
    private string nazwisko;
    public string Nazwisko
    {
        get { return nazwisko; }
        set
        {
            if (value.Length >= 2)
            {
                nazwisko = value;
            }
            else
            {
                Console.WriteLine("Za krotka nazwa");
            }
        }
    }
    private int wiek;
    public int Wiek
    {
        get { return wiek; }
        set
        {
            if (value > 0)
            {
                wiek = value;
            }
            else
            {
                Console.WriteLine("Za malo lat");
            }
        }
    }
    public osoba(string imie, string nazwisko, int wiek)
    {
        Imie = imie;
        Nazwisko = nazwisko;
        Wiek = wiek;
    }
    public void wyswietlinformacje()
    {
        Console.WriteLine("imie:" + imie + ", nazwisko:" + nazwisko + ", wiek:" + wiek);
    }
}


public class program
{
    static void Main()
    {
        osoba osoba1 = new osoba("jan", "pawel", 25);
        osoba osoba2 = new osoba("a", "pazdzioch", 89);
        osoba1.wyswietlinformacje();
        osoba2.wyswietlinformacje();
    }

}