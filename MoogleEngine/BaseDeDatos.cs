namespace MoogleEngine;


public class BaseDeDatos
{
    public string[] separadores = { " ", "\t", "\n", "\r", ".", ",", ";", ":", "?", "!", "(", ")", "[", "]", "{", "}", "'", "*" };
    public Dictionary<string, Dictionary<string, double>> tf;
    public Dictionary<string, double> idf;
    public List<string> words;
    public Dictionary<string, List<double>> VectorsDocument;
    public (Dictionary<string, Dictionary<string, double>>, Dictionary<string, double>) tfidf;

    public BaseDeDatos()
    {
        this.tfidf = TFIDF();
        this.tf = tfidf.Item1;
        this.idf = tfidf.Item2;
        this.words = idf.Keys.ToList();
        this.VectorsDocument = VectorDocument();
    }
    Dictionary<string, List<double>> VectorDocument()
    {
        Dictionary<string, List<double>> Vectorsdocuments = new Dictionary<string, List<double>>();

        double[] VectorDocument = new double[words.Count];
        foreach (string item in tf.Keys)
        {
            for (int i = 0; i < words.Count; i++)
            {
                if (tf[item].ContainsKey(words[i])) VectorDocument[i] = tf[item][words[i]] * idf[words[i]];
                else VectorDocument[i] = 0;
            }
            Vectorsdocuments[item] = VectorDocument.ToList();
        }
        return Vectorsdocuments;
    }
    public (Dictionary<string, Dictionary<string, double>>, Dictionary<string, double>) TFIDF()
    {
        Dictionary<string, Dictionary<string, double>> Tf = new Dictionary<string, Dictionary<string, double>>();
        Dictionary<string, double> idf = new Dictionary<string, double>();

        string[] NameDocuments = Directory.GetFiles("/Users/Reynol/Desktop/moogle-main-master/Content");

        double numdocum = NameDocuments.Length;

        foreach (string document in NameDocuments)
        {
            string documents = File.ReadAllText(document);

            if (!Tf.ContainsKey(document)) Tf[document] = new Dictionary<string, double>();

            foreach (string word in documents.ToLower().Split(separadores, StringSplitOptions.RemoveEmptyEntries))
            {
                if (Tf[document].ContainsKey(word)) Tf[document][word]++; else Tf[document][word] = 1;
            }
            foreach (string word in Tf[document].Keys)
            {
                if (!idf.ContainsKey(word)) idf[word] = 1 / numdocum; else idf[word] = idf[word] + 1 / numdocum;
            }

            double max = Tf[document].Values.Max();

            foreach (string key in Tf[document].Keys)
            {
                Tf[document][key] = Tf[document][key] / max;
            }
        }
        return (Tf, idf);
    }

}
