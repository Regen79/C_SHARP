using System;
using System.Linq;

public class student
{
    public string imie;
    public string nazwisko;
    public int[] oceny = { 1, 4, 1, 3, 6 };
    public double sredniaocen
    {
        get
        {
            if (oceny.Length > 0)
            {
                double suma = oceny.Sum();
                return suma / oceny.Length;
            }
            else
                return 0;
        }
    }


    public void dodajocene(int ocena)
    {
        int[] nowa = new int[oceny.Length + 1];
        for (int i = 0; i < oceny.Length; i++)
        {
            nowa[i] = oceny[i];
        }
        nowa[nowa.Length - 1] = ocena;
        oceny = nowa;
    }

    public student(string imie, string nazwisko)
    {
        this.imie = imie;
        this.nazwisko = nazwisko;
    }

}

public class program
{
    static void Main()
    {
        student student1 = new student("Jan", "Kowalski");
        Console.WriteLine("Imie: " + student1.imie + ", Nazwisko: " + student1.nazwisko);
        Console.WriteLine("Oceny ucznia: " + string.Join(", ", student1.oceny));
        student1.dodajocene(4);
        Console.WriteLine("Oceny ucznia: " + string.Join(", ", student1.oceny));
        Console.WriteLine("Srednia ocen: " + student1.sredniaocen);

    }
}