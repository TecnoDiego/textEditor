// Diego Aníbal Lezcano Reissner
using System;
using System.IO;

class QEdit
{
    public static void DibujarPrimeraLinea(string primeraLinea)
    {
        Console.SetCursorPosition(0, 0);
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(primeraLinea);
        for (int i = 0; i < (Console.WindowWidth - primeraLinea.Length); i++)
        {
            Console.Write(" ");
        }
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.Cyan;
    }

    public static void DibujarTexto(ListaDeFrases frases)
    {
        Console.SetCursorPosition(0, 1);
        for(int i = 0;i < frases.Cantidad;i++)
        {
            Console.WriteLine(frases.Get(i));
        }
    }

    public static void Guardar(string nombreFichero, ListaDeFrases frases)
    {
        try
        {
            StreamWriter fichero = new StreamWriter(nombreFichero);
            for(int i = 0;i < frases.Cantidad; i++)
            {
                fichero.WriteLine(frases.Get(i));
            }
            fichero.Close();
        }
        catch (PathTooLongException)
        {
            Console.WriteLine("Ruta demasiado larga");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Fichero no encontrado");
        }
        catch(IOException io)
        {
            Console.WriteLine("Error de lectura/escritura: " + io.Message);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    static void Main(string[] args)
    {
        ListaDeFrases frases = new ListaDeFrases();
        int x = 0, y = 1;
        string nombreFichero = args.Length == 1? args[0] : "";
        string primeraLinea;
        string lineaActual = "";
        bool salir = false;
        ConsoleKeyInfo tecla;

        do
        {
            primeraLinea = "Línea:" + y + " Columna:" + x +
                " Documento:" + nombreFichero;

            DibujarPrimeraLinea(primeraLinea);
            DibujarTexto(frases);
            Console.SetCursorPosition(0, y);
            Console.Write(lineaActual);
            tecla = Console.ReadKey(true);
            if (x + 1 <= 80)
            {
                lineaActual += tecla.KeyChar;
                x++;
            }
            if (tecla.Key == ConsoleKey.Enter)
            {
                frases.Añadir(lineaActual);
                lineaActual = "";
                x = 0;
                y++;
            }
            /*
            else if(tecla.Key == ConsoleKey.Delete)
            {
                lineaActual = lineaActual.Remove(lineaActual.Length - 2);
                x--;
            }
            */
            else if (tecla.Key == ConsoleKey.F10)
            {
                salir = true;
            }

        } while (!salir);
        Console.WriteLine();

        while(nombreFichero == "")
        {
            Console.Clear();
            Console.Write("Introduzca nombre del fichero: " + nombreFichero);
            nombreFichero += Console.ReadKey().KeyChar;
        }
        Guardar(nombreFichero, frases);
    }
}
