using System;
using System.Collections.Generic;
using System.IO;

#region Uzytkownicy
public class Uzytkownik
{
    public int id;
    public string imie;
    public string nazwisko;

    public Uzytkownik(int id, string imie, string nazwisko)
    {
        this.id = id;
        this.imie = imie;
        this.nazwisko = nazwisko;
    }

    public void WyswietlDane()
    {
        Console.WriteLine($"ID: {id}, Imię: {imie}, Nazwisko: {nazwisko}");
    }
}

public class Administrator : Uzytkownik
{
    public Administrator(int id, string imie, string nazwisko) : base(id, imie, nazwisko) { }
}

public class Ochroniarz : Uzytkownik
{
    public Ochroniarz(int id, string imie, string nazwisko) : base(id, imie, nazwisko) { }
}

public class Mieszkaniec : Uzytkownik
{
    public Mieszkaniec(int id, string imie, string nazwisko) : base(id, imie, nazwisko) { }
}
#endregion

#region Czujniki
public abstract class Czujnik
{
    public int id;
    public string nazwa;
    public string lokalizacja;

    public Czujnik(int id, string nazwa, string lokalizacja)
    {
        this.id = id;
        this.nazwa = nazwa;
        this.lokalizacja = lokalizacja;
    }

    public abstract string Odczyt();
}

public class CzujnikTemperatury : Czujnik
{
    public CzujnikTemperatury(int id, string lokalizacja) : base(id, "Czujnik temperatury", lokalizacja) { }

    public override string Odczyt()
    {
        return $"Temperatura w {lokalizacja}: 22°C";
    }
}

public class CzujnikWody : Czujnik
{
    public CzujnikWody(int id, string lokalizacja) : base(id, "Czujnik zużycia wody", lokalizacja) { }

    public override string Odczyt()
    {
        return $"Zużycie wody w {lokalizacja}: 10L/h";
    }
}
#endregion

#region Zgloszenia
public class ZgloszenieUsterki
{
    public int id;
    public string opis;
    public string zglaszajacy;
    public string status;
    public DateTime data;

    public ZgloszenieUsterki(int id, string opis, string zglaszajacy)
    {
        this.id = id;
        this.opis = opis;
        this.zglaszajacy = zglaszajacy;
        this.status = "Otwarte";
        this.data = DateTime.Now;
    }

    public void ZwrotDanychUsterki()
    {
        Console.WriteLine($"ID: {id}, Opis: {opis}, Zgłaszający: {zglaszajacy}, Status: {status}, Data: {data}");
    }
}
#endregion

#region Baza danych
public class BazaDanych
{
    private string sciezkaUsterki = "usterki.txt";
    private string sciezkaLogi = "logi.txt";

    public void ZapiszUsterki(List<ZgloszenieUsterki> usterki)
    {
        using (StreamWriter sw = new StreamWriter(sciezkaUsterki))
        {
            foreach (var u in usterki)
                sw.WriteLine($"{u.id}|{u.opis}|{u.zglaszajacy}|{u.status}|{u.data}");
        }
    }

    public List<ZgloszenieUsterki> WczytajUsterki()
    {
        List<ZgloszenieUsterki> usterki = new List<ZgloszenieUsterki>();
        if (!File.Exists(sciezkaUsterki)) return usterki;

        foreach (var line in File.ReadAllLines(sciezkaUsterki))
        {
            var dane = line.Split('|');
            ZgloszenieUsterki u = new ZgloszenieUsterki(int.Parse(dane[0]), dane[1], dane[2]);
            u.status = dane[3];
            u.data = DateTime.Parse(dane[4]);
            usterki.Add(u);
        }
        return usterki;
    }

    public void ZapiszLogi(List<string> logi)
    {
        File.WriteAllLines(sciezkaLogi, logi);
    }

    public List<string> WczytajLogi()
    {
        if (!File.Exists(sciezkaLogi)) return new List<string>();
        return new List<string>(File.ReadAllLines(sciezkaLogi));
    }
}
#endregion

#region Kontrola dostepu
public class KontrolaDostepu
{
    private List<string> logi = new List<string>();
    private BazaDanych baza = new BazaDanych();

    public KontrolaDostepu()
    {
        logi = baza.WczytajLogi();
    }

    public void Wejscie(Uzytkownik u)
    {
        string wpis = $"{DateTime.Now}: {u.imie} {u.nazwisko} wszedł do budynku";
        logi.Add(wpis);
        Console.WriteLine(wpis);
        baza.ZapiszLogi(logi);
    }

    public void Wyjscie(Uzytkownik u)
    {
        string wpis = $"{DateTime.Now}: {u.imie} {u.nazwisko} wyszedł z budynku";
        logi.Add(wpis);
        Console.WriteLine(wpis);
        baza.ZapiszLogi(logi);
    }

    public void PokazLogi()
    {
        Console.WriteLine("=== Logi Kontroli Dostępu ===");
        foreach (var wpis in logi) Console.WriteLine(wpis);
    }
}
#endregion

#region Aplikacja
public class Aplikacja
{
    private List<Uzytkownik> uzytkownicy = new List<Uzytkownik>();
    private List<Czujnik> czujniki = new List<Czujnik>();
    private List<ZgloszenieUsterki> usterki = new List<ZgloszenieUsterki>();
    private KontrolaDostepu kontrola = new KontrolaDostepu();
    private BazaDanych baza = new BazaDanych();
    private Uzytkownik Zalogowany = null;

    public void Menu()
    {
        usterki = baza.WczytajUsterki();

        while (true)
        {
            if (Zalogowany == null)
            {
                Logowanie();
                if (Zalogowany == null) continue;
            }

            Console.Clear();
            Console.WriteLine($"=== Witaj {Zalogowany.imie} ({Zalogowany.GetType().Name}) ===");

            if (Zalogowany is Administrator)
            {
                Console.WriteLine("1. Użytkownicy");
                Console.WriteLine("2. Czujniki");
                Console.WriteLine("3. Zgłoszenia usterek");
                Console.WriteLine("4. Kontrola dostępu");
                Console.WriteLine("5. Wyloguj");
            }
            else if (Zalogowany is Mieszkaniec)
            {
                Console.WriteLine("1. Dodaj zgłoszenie usterki");
                Console.WriteLine("2. Wejście/Wyjście z budynku");
                Console.WriteLine("3. Wyloguj");
            }
            else if (Zalogowany is Ochroniarz)
            {
                Console.WriteLine("1. Czujniki");
                Console.WriteLine("2. Zgłoszenia usterek");
                Console.WriteLine("3. Logi kontroli dostępu");
                Console.WriteLine("4. Wyloguj");
            }

            Console.Write("Opcja: ");
            string opcja = Console.ReadLine();

            if (Zalogowany is Administrator)
            {
                switch (opcja)
                {
                    case "1": MenuUzytkownicy(); break;
                    case "2": MenuCzujniki(); break;
                    case "3": MenuUsterki(); break;
                    case "4": MenuKontrolaDostepu(); break;
                    case "5": Zalogowany = null; break;
                    default: Console.WriteLine("Niepoprawna opcja"); break;
                }
            }
            else if (Zalogowany is Mieszkaniec)
            {
                switch (opcja)
                {
                    case "1": MenuUsterkiMieszkaniec(); break;
                    case "2": RejestracjaDostepuMieszkaniec(); break;
                    case "3": Zalogowany = null; break;
                    default: Console.WriteLine("Niepoprawna opcja"); break;
                }
            }
            else if (Zalogowany is Ochroniarz)
            {
                switch (opcja)
                {
                    case "1": MenuCzujniki(); break;
                    case "2": MenuUsterki(); break;
                    case "3": kontrola.PokazLogi(); break;
                    case "4": Zalogowany = null; break;
                    default: Console.WriteLine("Niepoprawna opcja"); break;
                }
            }

            Console.WriteLine("Naciśnij ENTER aby kontynuować...");
            Console.ReadLine();
        }
    }

    private void Logowanie()
    {
        Console.Clear();
        Console.WriteLine("=== Logowanie ===");
        Console.Write("Podaj ID użytkownika: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Niepoprawne ID!");
            return;
        }
        var user = uzytkownicy.Find(x => x.id == id);
        if (user == null)
        {
            Console.WriteLine("Nie znaleziono użytkownika!");
            return;
        }
        Zalogowany = user;
        Console.WriteLine($"Zalogowano jako: {user.imie} {user.nazwisko} ({user.GetType().Name})");
    }

    #region Menu Uzytkownicy
    private void MenuUzytkownicy()
    {
        Console.Clear();
        Console.WriteLine("1. Dodaj użytkownika");
        Console.WriteLine("2. Wyświetl użytkowników");
        Console.WriteLine("3. Usuń użytkownika");
        Console.Write("Opcja: ");
        string opcja = Console.ReadLine();

        if (opcja == "1")
        {
            Console.Write("ID: "); int id = int.Parse(Console.ReadLine());
            Console.Write("Imię: "); string imie = Console.ReadLine();
            Console.Write("Nazwisko: "); string nazwisko = Console.ReadLine();
            Console.Write("Typ (1-Admin,2-Ochroniarz,3-Mieszkaniec): "); string typ = Console.ReadLine();

            Uzytkownik u = typ switch
            {
                "1" => new Administrator(id, imie, nazwisko),
                "2" => new Ochroniarz(id, imie, nazwisko),
                "3" => new Mieszkaniec(id, imie, nazwisko),
                _ => null
            };
            if (u != null) uzytkownicy.Add(u);
        }
        else if (opcja == "2")
        {
            foreach (var u in uzytkownicy) u.WyswietlDane();
        }
        else if (opcja == "3")
        {
            Console.Write("Podaj ID użytkownika do usunięcia: "); int id = int.Parse(Console.ReadLine());
            uzytkownicy.RemoveAll(x => x.id == id);
            Console.WriteLine("Usunięto użytkownika");
        }
    }
    #endregion

    #region Menu Czujniki
    private void MenuCzujniki()
    {
        Console.Clear();
        Console.WriteLine("1. Dodaj czujnik");
        Console.WriteLine("2. Wyświetl czujniki");
        Console.Write("Opcja: ");
        string opcja = Console.ReadLine();

        if (opcja == "1")
        {
            Console.Write("ID: "); int id = int.Parse(Console.ReadLine());
            Console.Write("Lokalizacja: "); string loc = Console.ReadLine();
            Console.Write("Typ (1-Temperatura, 2-Woda): "); string typ = Console.ReadLine();

            Czujnik c = typ switch
            {
                "1" => new CzujnikTemperatury(id, loc),
                "2" => new CzujnikWody(id, loc),
                _ => null
            };
            if (c != null) czujniki.Add(c);
        }
        else if (opcja == "2")
        {
            foreach (var c in czujniki)
            {
                Console.WriteLine($"{c.nazwa} w {c.lokalizacja} | {c.Odczyt()}");
            }
        }
    }
    #endregion

    #region Menu Usterki
    private void MenuUsterki()
    {
        Console.Clear();
        Console.WriteLine("1. Dodaj zgłoszenie");
        Console.WriteLine("2. Wyświetl zgłoszenia");
        Console.WriteLine("3. Zmień status zgłoszenia");
        Console.Write("Opcja: ");
        string opcja = Console.ReadLine();

        if (opcja == "1")
        {
            Console.Write("ID: "); int id = int.Parse(Console.ReadLine());
            Console.Write("Opis: "); string opis = Console.ReadLine();
            Console.Write("Zgłaszający: "); string zg = Console.ReadLine();
            usterki.Add(new ZgloszenieUsterki(id, opis, zg));
            baza.ZapiszUsterki(usterki);
        }
        else if (opcja == "2")
        {
            foreach (var u in usterki) u.ZwrotDanychUsterki();
        }
        else if (opcja == "3")
        {
            Console.Write("Podaj ID zgłoszenia: "); int id = int.Parse(Console.ReadLine());
            var zgl = usterki.Find(x => x.id == id);
            if (zgl == null) { Console.WriteLine("Nie znaleziono zgłoszenia"); return; }
            Console.Write("Nowy status: "); string status = Console.ReadLine();
            zgl.status = status;
            baza.ZapiszUsterki(usterki);
        }
    }
    private void MenuUsterkiMieszkaniec()
    {
        Console.Clear();
        Console.Write("ID zgłoszenia: "); int id = int.Parse(Console.ReadLine());
        Console.Write("Opis: "); string opis = Console.ReadLine();
        usterki.Add(new ZgloszenieUsterki(id, opis, Zalogowany.imie));
        baza.ZapiszUsterki(usterki);
        Console.WriteLine("Zgłoszenie dodane.");
    }
    #endregion

    #region Kontrola dostepu
    private void MenuKontrolaDostepu()
    {
        Console.Clear();
        Console.WriteLine("1. Wejście użytkownika");
        Console.WriteLine("2. Wyjście użytkownika");
        Console.WriteLine("3. Pokaż logi");
        Console.Write("Opcja: ");
        string opcja = Console.ReadLine();

        if (opcja != "3")
        {
            Console.Write("ID użytkownika: ");
            int id = int.Parse(Console.ReadLine());
            var u = uzytkownicy.Find(x => x.id == id);
            if (u == null) { Console.WriteLine("Nie znaleziono użytkownika"); return; }

            switch (opcja)
            {
                case "1": kontrola.Wejscie(u); break;
                case "2": kontrola.Wyjscie(u); break;
                default: Console.WriteLine("Niepoprawna opcja!"); break;
            }
        }
        else
        {
            kontrola.PokazLogi();
        }
    }

    private void RejestracjaDostepuMieszkaniec()
    {
        Console.Clear();
        Console.WriteLine("1. Wejście do budynku");
        Console.WriteLine("2. Wyjście z budynku");
        Console.Write("Opcja: ");
        string opcja = Console.ReadLine();

        if (opcja == "1") kontrola.Wejscie(Zalogowany);
        else if (opcja == "2") kontrola.Wyjscie(Zalogowany);
        else Console.WriteLine("Niepoprawna opcja!");
    }
    #endregion
}
#endregion

#region Program
class Program
{
    static void Main()
    {
        Aplikacja app = new Aplikacja();
        app.Menu();
    }
}
#endregion
