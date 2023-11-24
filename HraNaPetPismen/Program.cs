
using System.IO;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

var cesta = Path.Combine(Directory.GetCurrentDirectory(), "PetPismen.txt");


string[] slova = File.ReadAllLines(cesta);

//ověřím, že se slova načetla správně
//foreach (string slovo in slova)
//{
//    Console.WriteLine(slovo);
//}

//vyberu jedno slovo
Random random = new Random();
int index = random.Next(0, slova.Length);
string myslimSiSlovo = slova[index];
string distinctMyslimSi = string.Join("", myslimSiSlovo.Distinct()); //vyberu jen jedinečná písmena

//pravidla hry
Console.WriteLine(" Tvým úkolem je přijít na slovo, které si počítač myslí. Zadávej jen slova, která mají pět písmen, jsou v první osobě jednotného čísla a neobsahují diakritiku, např. 'potok'.");

//vyzvu uživatele, aby zadal svoje slovo

while (true)
{
     string zadaneSlovo = zadejSlovo();

    //zkontroluju, že je zadané slovo správné

    if (zadaneSlovo.Length == 5)
    {
        if (!DiacriticCheck(zadaneSlovo))
        {
            //Console.WriteLine("správně");
            string vysledek = provedVypocet(zadaneSlovo);
            Console.WriteLine(vysledek);
            continue;
        }
        else
        {
            Console.WriteLine("Zadané slovo obsahuje diakritiku, zkus to znovu");

        }
    }
    else
    {
        Console.WriteLine("Slovo má špatnou délku: " + zadaneSlovo.Length + " znaků, musí mít 5 znaků. Zkus to znovu.");

    }
}
string provedVypocet(string slovo)
{

    //nejprve check, jestli slova nejsou stejná 
    if (myslimSiSlovo == slovo)
    {
        string vyhra = "Gratulky, vyhráli jste! Slovo bylo " + slovo;
        return vyhra;
    }
    else
    {
        //projdu proti sobě dvě slova a zjistím, jestli obsahují stejná písmena.

        int pocetShodnychPismen = 0;
        string distinctZadana = string.Join("", slovo.Distinct());

        foreach (char znak in distinctZadana) 
        {
            if (distinctMyslimSi.Contains(znak))
            {
                pocetShodnychPismen++;
            }
        }

        string shodnaPismena = " Počet shodných písmen: " + pocetShodnychPismen;

        //zjistím počet shodných pozic 

        int pocetShodnychPozic = 0;
        for (int i = 0; i < slovo.Length; i++)
        {
            if (slovo[i] == myslimSiSlovo[i])
            {
                pocetShodnychPozic++;
            }
        }
        string shodnePozice = "Počet shodných pozic: " + pocetShodnychPozic + ".";
        return shodnePozice + shodnaPismena;
    }
}


//diacritics check
static bool DiacriticCheck(string text) => Regex.IsMatch(text, @"[ěščřžýáíéóůúňťď]");

//zadání slova
 string zadejSlovo()
{
    Console.WriteLine("\nZadej slovo.");
    string x = Console.ReadLine();
    return x;

}
