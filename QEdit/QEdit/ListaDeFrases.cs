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

    public void Añadir(string linea)
    {
        frases.Add(linea);
    }
}
