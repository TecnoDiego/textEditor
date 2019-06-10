

using System.Text;

class Linea
{
    public string LineaActual { get; set; }

    public Linea()
    {
        LineaActual = "";
    }

    public void Insertar(int posicion, char caracterInsertar)
    {
        StringBuilder insertar = new StringBuilder(LineaActual);
        insertar.Insert(posicion, caracterInsertar);
        LineaActual = insertar.ToString();
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
