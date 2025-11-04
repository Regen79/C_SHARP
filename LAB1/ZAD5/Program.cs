using System;
using System.Linq;
using System.Collections.Generic;

public class sumator
{
    private int[] tablica;
    public double suma1;
    public double suma2;
    public sumator(int[] tablica)
    {
        this.tablica = tablica;
    }
    public void suma()
    {
        suma1 = tablica.Sum();
        Console.WriteLine("Suma tablicy: " + suma1);
    }
    public void podziel()
    {
        List<int> podzielne = new List<int>();
        for (int i = 0; i < tablica.Length; i++)
        {
            if (tablica[i] % 2 == 0)
            {
                podzielne.Add(tablica[i]);
            }
        }
        suma2 = podzielne.Sum();
        Console.WriteLine("Suma podzielnych przez dwa: " + suma2);
    }

    public int ileelementow()
    {
        return tablica.Length;
    }
    public void wypisz()
    {
        Console.WriteLine("Tablica: " + string.Join(", ", tablica));
    }
    public void indeksy(int lowindex, int highindex)
    {
        List<int> tablica2 = new List<int>();
        if (lowindex < 0)
        {
            lowindex = 0;
        }
        if (highindex >= tablica.Length)
        {
            highindex = tablica.Length - 1;
        }
        for (int i = 0; i < tablica.Length; i++)
        {

            if (i >= lowindex && i <= highindex)
            {
                tablica2.Add(tablica[i]);
            }

        }
        Console.WriteLine("Liczby w podanych indeksach: " + string.Join(", ", tablica2));
    }
}
public class program
{
    static void Main()
    {
        int[] dane = { 2, 4, 68, 67, 7 };
        sumator sumator1 = new sumator(dane);
        sumator1.wypisz();
        sumator1.suma();
        sumator1.podziel();
        Console.WriteLine("Dlugosc tablicy: " + sumator1.ileelementow());
        sumator1.indeksy(1, 2);
    }
}