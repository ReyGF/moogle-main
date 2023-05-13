namespace MoogleEngine;


public class ModeloVectorial
{
    BaseDeDatos documents;
    Query query;
    double[] VectorQuery;
    public Dictionary<string, double> Score;
    public List<string> MoreFrequency;
    public ModeloVectorial(BaseDeDatos documents, Query query)
    {
        this.documents = documents;
        this.query = query;
        this.VectorQuery = VectorsQuery();
        this.Score = Scores(); 
        this.MoreFrequency = MoreFrequencyDocuments(Score);
    }
    double[] VectorsQuery()
    {
        double[] VectorQuery = new double[documents.words.Count];

        for(int j = 0;j < documents.words.Count;j++)
        {
            if (query.TFquery.ContainsKey(documents.words[j]))
            {
                VectorQuery[j] = query.TFquery[documents.words[j]]*documents.idf[documents.words[j]];
            }
            else
            {
                VectorQuery[j] = 0;
            }   
        }
        return VectorQuery;
    }
    Dictionary<string, double> Scores()
    {
        Dictionary<string, double> Score = new Dictionary<string, double>();
        
        foreach(KeyValuePair<string, List<double>> item in documents.VectorsDocument)
        {
            Score[item.Key] = Similitud(item.Value, VectorQuery.ToList());
        }
        return Score;
    }
    public double Normalizar(List<double> vector)
    {
        double result = 0;
        
        for(int i = 0;i < vector.Count;i++)
        {
            result += vector[i]*vector[i];
        }
        return Math.Sqrt(result);
    }  
    public double Multiplicar(List<double> vector1, List<double> vector2)
    {
        double producto = 0;
        for(int i = 0;i < vector1.Count;i++)
        {
            producto += vector1[i]*vector2[i];
        }
        return producto;
    }
    public double Similitud(List<double> vector1, List<double> vector2)
    {
        double score = Multiplicar(vector1, vector2)/Normalizar(vector1)*Normalizar(vector2);
        return score;
    }
    public List<string> MoreFrequencyDocuments(Dictionary<string, double> frecuencias)
    {
        // Ordenar el diccionario por valor de forma descendente
        var frecuenciasOrdenadas = frecuencias.OrderByDescending(x => x.Value);

        // Tomar las tres primeras palabras del diccionario ordenado
        var palabrasMasFrecuentes = frecuenciasOrdenadas.Take(5);

        // Crear una lista con las palabras más frecuentes
        List<string> listaPalabrasMasFrecuentes = new List<string>();
        foreach (var kvp in palabrasMasFrecuentes)
        {
            listaPalabrasMasFrecuentes.Add(kvp.Key);
        }

        return listaPalabrasMasFrecuentes;
    }

}


