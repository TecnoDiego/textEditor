using System.Text;

class Linea
{
    public string LineaActual { get; set; }

    public Linea()
    {
        LineaActual = "";
    }

    public void Insertar(int posicion, char caracterInsertar, bool modoInsercion)
    {
        StringBuilder insertar = new StringBuilder(LineaActual);
        insertar.Insert(posicion, caracterInsertar);
        LineaActual = insertar.ToString();
        if (!modoInsercion)
            Suprimir(posicion + 1);
    }

    public void Borrar(int posicion)
    {
        if (posicion > 0)
            LineaActual = LineaActual.Remove(posicion - 1, 1);
        else if (posicion == 0 && LineaActual.Length > 1)
            LineaActual = LineaActual.Substring(1);
        else
            LineaActual = "";
    }

    public void Suprimir(int posicion)
    {
        if (posicion < LineaActual.Length)
            LineaActual = LineaActual.Remove(posicion, 1);
        
    }

    public string LineaVisible()
    {
        int primeraColumna, tamanyo;
        if(LineaActual.Length <= 80)
        {
            tamanyo = LineaActual.Length;
            primeraColumna = 0;
        }
        else
        {
            tamanyo = 80;
            primeraColumna = LineaActual.Length - 80;
        }

        return LineaActual.Substring(primeraColumna, tamanyo);
    }
}
