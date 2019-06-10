// Diego Aníbal Lezcano Reissner
using System;
using System.IO;

class QEdit
{
    struct Cursor
    {
        public int x;
        public int y;
    }

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

    public static string PedirNombreFichero()
    {
        ConsoleKeyInfo tecla;
        string fichero = "";

        do
        {
            Console.Clear();
            Console.Write("Introduzca nombre del fichero: " + fichero);
            tecla = Console.ReadKey(true);
            if (tecla.Key != ConsoleKey.Enter)
                fichero += tecla.KeyChar;
        } while (tecla.Key != ConsoleKey.Enter);

        return fichero;
    }

    public static void Guardar(string nombreFichero, ListaDeFrases frases)
    {
        try
        {
            StreamWriter fichero = new StreamWriter(nombreFichero);
            for(int i = 0;i < frases.Cantidad; i++)
            {
                Console.WriteLine(frases.Get(i));
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

    public static void Cargar(string nombreFichero, ListaDeFrases frases)
    {
        try
        {
            StreamReader fichero = new StreamReader(nombreFichero);
            string linea = null;
            do
            {
                linea = fichero.ReadLine();
                if(linea != null)
                {
                    frases.Anyadir(linea, 0);
                }
            } while (linea != null);
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
        catch (IOException io)
        {
            Console.WriteLine("Error de lectura/escritura: " + io.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static bool EsDibujable(ConsoleKey tecla)
    {
        // Teclas que se usan actualmente (de momento) pero que no queremos que se añadan a la cadena
        if(tecla == ConsoleKey.Enter || tecla == ConsoleKey.End ||
            tecla == ConsoleKey.Home || tecla == ConsoleKey.LeftArrow ||
            tecla == ConsoleKey.RightArrow || tecla == ConsoleKey.UpArrow ||
            tecla == ConsoleKey.DownArrow || tecla == ConsoleKey.F10)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    static void Main(string[] args)
    {
        ListaDeFrases frases = new ListaDeFrases();
        Console.SetBufferSize(1024, 1024);
        Console.SetWindowSize(80, Console.WindowHeight);
        Cursor cursor;
        cursor.x = 0;
        cursor.y = 1;
        string nombreFichero = args.Length == 1? args[0] : "";
        string primeraLinea;
        Linea linea = new Linea();
        
        bool salir = false;
        ConsoleKeyInfo tecla;

        if (File.Exists(nombreFichero))
            Cargar(nombreFichero, frases);

        do
        {
            primeraLinea = "Línea:" + cursor.y + " Columna:" + (cursor.x + 1) +
                " Documento:" + nombreFichero;

            DibujarPrimeraLinea(primeraLinea);
            DibujarTexto(frases);
            Console.SetCursorPosition(0, cursor.y);
            Console.Write(linea.LineaActual);
            Console.SetCursorPosition(cursor.x, cursor.y);
            tecla = Console.ReadKey(true);

            if (EsDibujable(tecla.Key))
            {
                linea.Insertar(cursor.x, tecla.KeyChar);
                cursor.x++;
            } 
            frases.Anyadir(linea.LineaActual, cursor.y - 1);
            
            // TO DO Encapsular en funciones
            if (tecla.Key == ConsoleKey.Enter)
            {
                cursor.x = 0;
                linea.LineaActual = "";
                cursor.y++;
            }
            else if (tecla.Key == ConsoleKey.Home)
            {
                cursor.x = 0;
            }
            else if (tecla.Key == ConsoleKey.End)
            {
                cursor.x = frases.Get(cursor.y - 1).Length;
            }
            else if(tecla.Key == ConsoleKey.LeftArrow)
            {
                if(cursor.x > 0)
                    cursor.x--;
            }
            else if (tecla.Key == ConsoleKey.RightArrow)
            {
                if (cursor.x < linea.LineaActual.Length)
                    cursor.x++;
            }
            else if (tecla.Key == ConsoleKey.UpArrow)
            {
                if (cursor.y - 1 > 0)
                {
                    cursor.y--;
                    linea.LineaActual = frases.Get(cursor.y - 1);
                    if (cursor.x > frases.Get(cursor.y - 1).Length)
                        cursor.x = frases.Get(cursor.y - 1).Length;
                }
            }
            else if (tecla.Key == ConsoleKey.DownArrow)
            {
                if (cursor.y < frases.Cantidad)
                {
                    cursor.y++;
                    linea.LineaActual = frases.Get(cursor.y - 1);
                    if (cursor.x > frases.Get(cursor.y - 1).Length)
                        cursor.x = frases.Get(cursor.y - 1).Length;
                }
            }
            else if (tecla.Key == ConsoleKey.F10)
            {
                salir = true;
            }
            

        } while (!salir);
        Console.WriteLine();

        if(nombreFichero == "")
            nombreFichero = PedirNombreFichero();

        Console.WriteLine();
        Guardar(nombreFichero, frases);
    }
}
