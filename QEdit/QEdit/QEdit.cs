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

    public static bool EsDibujable(ConsoleKey tecla)
    {
        // Teclas que se usan actualmente (de momento) pero que no queremos que se añadan a la cadena
        if(tecla == ConsoleKey.Enter || tecla == ConsoleKey.End ||
            tecla == ConsoleKey.Home || tecla == ConsoleKey.LeftArrow ||
            tecla == ConsoleKey.RightArrow || tecla == ConsoleKey.UpArrow ||
            tecla == ConsoleKey.DownArrow || tecla == ConsoleKey.F10 ||
            tecla == ConsoleKey.Insert || tecla == ConsoleKey.Delete || 
            tecla == ConsoleKey.Backspace || tecla == ConsoleKey.PageDown ||
            tecla == ConsoleKey.PageUp)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public static void ComprobarLongitudCadena(ref int columna, int fila, 
        ListaDeFrases frases)
    {
        if (columna > frases.Get(fila - 1).Length)
            columna = frases.Get(fila - 1).Length;
    }

    public static bool GuardarCambios()
    {
        ConsoleKeyInfo tecla;
        bool guardar = false; ;
        do
        {
            Console.Clear();
            Console.WriteLine("¿Desea guardar los cambios?(s/n)");
            tecla = Console.ReadKey(true);
            
            if(tecla.Key == ConsoleKey.S)
            {
                guardar = true;
                Console.WriteLine("Guardando...");
            }
        } while (tecla.Key != ConsoleKey.S && tecla.Key != ConsoleKey.N);
        return guardar;
    }

    static void Main(string[] args)
    {
        const int SALTO = 20;
        const int ANCHO = 80;
        const int ALTO = 25;
        const int BUFFER = 1024;
        ListaDeFrases frases = new ListaDeFrases();
        Linea linea = new Linea();
        Console.SetBufferSize(BUFFER, BUFFER);
        Console.SetWindowSize(ANCHO, ALTO);
        string nombreFichero = args.Length == 1 ? args[0] : "";

        Cursor cursor;
        cursor.x = 0;
        cursor.y = 1;
        bool insercion = true;
        string primeraLinea;
        bool salir = false;
        ConsoleKeyInfo tecla;

        if (File.Exists(nombreFichero))
            frases.Cargar(nombreFichero);
        linea.LineaActual = frases.Get(cursor.y - 1);
        do
        {
            primeraLinea = "Línea:" + cursor.y + " Columna:" + (cursor.x + 1) +
               (insercion? " INS" : " SOB") + " Documento:" + nombreFichero;

            DibujarPrimeraLinea(primeraLinea);
            DibujarTexto(frases);
            Console.SetCursorPosition(0, cursor.y);
            Console.Write(linea.LineaActual);
            Console.SetCursorPosition(cursor.x, cursor.y);
            tecla = Console.ReadKey(true);

            if (tecla.Key == ConsoleKey.Insert)
                insercion = !insercion;

            if (EsDibujable(tecla.Key))
            {
                linea.Insertar(cursor.x, tecla.KeyChar, insercion);
                
                cursor.x++;
            }
            frases.Anyadir(linea.LineaActual, cursor.y - 1);

            // TO DO Encapsular en funciones
            if (tecla.Key == ConsoleKey.Enter)
            {
                cursor.x = 0;
                linea.LineaActual = frases.Get(cursor.y);
                cursor.y++;
            }
            else if (tecla.Key == ConsoleKey.Backspace)
            {
                linea.Borrar(cursor.x);
                if (cursor.x > 0)
                    cursor.x--;
            }
            else if (tecla.Key == ConsoleKey.Delete)
            {
                linea.Suprimir(cursor.x);
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
                    ComprobarLongitudCadena(ref cursor.x, cursor.y, frases);
                }
            }
            else if (tecla.Key == ConsoleKey.DownArrow)
            {
                if (cursor.y < frases.Cantidad)
                {
                    cursor.y++;
                    linea.LineaActual = frases.Get(cursor.y - 1);
                    ComprobarLongitudCadena(ref cursor.x, cursor.y, frases);
                }
            }
            else if(tecla.Key == ConsoleKey.PageDown)
            {
                if (cursor.y + SALTO > frases.Cantidad)
                {
                    cursor.y = frases.Cantidad;
                }
                else
                {
                    cursor.y += 20;
                }
                linea.LineaActual = frases.Get(cursor.y - 1);
                ComprobarLongitudCadena(ref cursor.x, cursor.y, frases);
            }
            else if (tecla.Key == ConsoleKey.PageUp)
            {
                
                if (cursor.y - SALTO < 1)
                {
                    cursor.y = 1;
                }
                else
                {
                    cursor.y -= SALTO;
                }
                linea.LineaActual = frases.Get(cursor.y - 1);
                ComprobarLongitudCadena(ref cursor.x, cursor.y, frases);
            }
            else if (tecla.Key == ConsoleKey.F10)
            {
                salir = true;
            }
            

        } while (!salir);
        Console.WriteLine();

        if(GuardarCambios())
        {
            if(nombreFichero == "")
                nombreFichero = PedirNombreFichero();
            frases.Guardar(nombreFichero);
        }
        Console.WriteLine();
    }
}
