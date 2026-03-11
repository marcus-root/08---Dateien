namespace Streams___01___Dateisuche
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String dateiname = "ausgabeDateiFürAufgabeDateien.txt"; // Legt den Dateinamen fest
            String dateipfad = Suche(dateiname); // Sucht die Zieldatei und speichert den Dateipfad in der Variable
            String endString = "."; // legt die Zeichenfolge fest mit der die Eingabe beendet wird
            List<String> eingabeZeilen;

            if (dateipfad != "-1") // Wenn die Datei gefunden wurde
            {
                Console.WriteLine($"Datei {dateiname} \nwurde in {dateipfad.Replace(dateiname, "")} gefunden!\nGeben Sie Text ein und Beenden Sie Ihre Eingabe mit einem Punkt \".\"\n");
                eingabeZeilen = Einlesen(endString); // Liest alle Zeilen ein bis eine Zeile nur aus endString besteht
                Console.WriteLine($"\nDatei {dateiname} wurde mit Ihrer Eingabe und Zeitstempeln ergänzt!");
                // LeereDatei(dateipfad); // leert die Datei
                SchreibeInDatei(dateipfad, eingabeZeilen); // Anhängen des eingegebenen Textes an die Datei
            }
            else { Console.WriteLine($"Die Datei {dateiname} wurde nicht gefunden!"); }
        }

        static List<String> Einlesen(String endString)
        {
            String eingabeZeile = "";
            String zeilenDatum = "";
            List<String> eingabeZeilen = new List<string>();
            do
            {
                eingabeZeile = Console.ReadLine(); // eine Zeile einlesen
                if (eingabeZeile != endString) // wenn die Zeile nicht dem endString entspricht
                {
                    zeilenDatum = DateTime.Now.ToShortTimeString(); // Aktuelles Datum speichern
                    eingabeZeilen.Add($"{zeilenDatum} | {eingabeZeile}"); // Die Zeile der Liste hinzufügen
                }
            } while (eingabeZeile != endString); // Abbruchbedingung: Wenn die Zeile dem endString entspricht
            return eingabeZeilen; // Die Liste zurückgeben
        }

        static String Suche(String dateiname)
        {
            return Suche(@"D:\", dateiname); // Aufruf der rekursiven Funktion
        }

        static String Suche(String pfad, String dateiname)
        {
            String[] ordnerInPfad = default;
            if (File.Exists($"{pfad}\\{dateiname}")) return $"{pfad}\\{dateiname}"; // wenn die Datei im aktuellen Pfad gefunden wurde, gib den Pfad zurück
            else
            {
                ordnerInPfad = Directory.GetDirectories(pfad); // speichert die Unterordner des aktuellen Pfades
                if (ordnerInPfad?.Length != 0) // wenn es Unterordner gibt
                {
                    foreach (String ordner in ordnerInPfad) // für jeden Ordner
                    {
                        if (!ordner.Contains("$RECYCLE.BIN")) // Wenn wir nicht im Papierkorb sind (es treten sonst Fehler auf)
                        {
                            return Suche(ordner, dateiname); // neuer Aufruf dieser Funktion
                        }
                    }
                }
                return "-1"; // Wurde die Datei nicht gefunden und es existieren keine Ornder mehr, wird "-1" zurückgegeben
            }
        }

        static void SchreibeInDatei(String dateipfad, List<String> zeilen)
        {
            File.AppendAllLines(dateipfad, zeilen);
        }

        static void LeereDatei(String dateipfad)
        {
            File.WriteAllText(dateipfad, "");
        }
    }
}
