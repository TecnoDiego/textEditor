using System;
using System.Collections.Generic;
using System.IO;

/*
 * Clase que almacena la lista de frases actualmente escritas
 */
class ListaDeFrases
{
    protected List<string> frases;
    public int Cantidad
    {
        get
        {
            return frases.Count;
        }
    }

    public ListaDeFrases()
    {
        frases = new List<string>();
    }

    public string Get(int numeroLinea)
    {
        return frases[numeroLinea];
    }

    // Modifica la linea actual en la que estemos o añade la linea a la lista si
    // la coordenada Y del cursor es mayor que el tamaño de la lista
    public void Anyadir(string linea, int posicion)
    {
        if (posicion >= frases.Count)
        {
            frases.Add(linea);
        }
        else
        {
            frases[posicion] = linea;
        }
    }

    public void Anyadir(string linea)
    {
        frases.Add(linea);
    }

    public void Guardar(string nombreFichero)
    {
        try
        {
            StreamWriter fichero = new StreamWriter(nombreFichero);
            for (int i = 0; i < Cantidad; i++)
            {
                fichero.WriteLine(Get(i));
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
        catch (IOException io)
        {
            Console.WriteLine("Error de lectura/escritura: " + io.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void Cargar(string nombreFichero)
    {
        try
        {
            StreamReader fichero = new StreamReader(nombreFichero);
            string linea = null;
            do
            {
                linea = fichero.ReadLine();
                if (linea != null)
                {
                    Anyadir(linea);
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
}
