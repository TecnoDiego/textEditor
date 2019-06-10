using System.Collections.Generic;

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
}
