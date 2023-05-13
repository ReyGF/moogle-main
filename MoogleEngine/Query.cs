namespace MoogleEngine;


public class Query
{
    public List<string> words;
    public Dictionary<string, double> TFquery;

    public Query(string query)
    {
        this.words = query.ToLower().Split(new string[]{" ", "\t", "\n", "\r", ".", ",", ";", ":", "?", "!", "(", ")", "[", "]", "{", "}", "'","*"}, StringSplitOptions.RemoveEmptyEntries).ToList();
        this.TFquery = TF();
    }
    Dictionary<string, double> TF()
    {
        Dictionary<string , int> frecuencias = new Dictionary<string, int>();
        
        foreach (string word in words)
        {
            if (frecuencias.ContainsKey(word))
            {
                frecuencias[word]++;
            }
            else
            {
                frecuencias[word] = 1;
            }
        }

        Dictionary<string, double> tf = new Dictionary<string, double>();
        
        int maxFrecuencia = frecuencias.Values.Max();
        
        foreach (KeyValuePair<string, int> kvp in frecuencias)
        {
            tf[kvp.Key] = (double)kvp.Value / maxFrecuencia;
        }
        return tf;
    }
    
}
