namespace MoogleEngine;


public class BaseDeDatos
{
    public string[] separadores = {" ", "\t", "\n", "\r", ".", ",", ";", ":", "?", "!", "(", ")", "[", "]", "{", "}", "'","*"};
    public Dictionary<Dictionary<string, Dictionary<string,double>>, Dictionary<string, double>> TFIDF;
    public Dictionary<string, Dictionary<string,double>> tf;
    public Dictionary<string, double> idf;
    public List<string> words;
    public Dictionary<string, List<double>> VectorsDocument;
   
    public BaseDeDatos()
    {
        this.TFIDF = Load();
        this.tf = TF();
        this.idf = IDF();
        this.words = idf.Keys.ToList();
        this.VectorsDocument = VectorDocument();
    }
    Dictionary<string, List<double>> VectorDocument()
    {
        Dictionary<string, List<double>> Vectorsdocuments = new Dictionary<string, List<double>>();

        double[] VectorDocument = new double[words.Count];
        foreach (string item in tf.Keys)
        {
            for(int i = 0; i < words.Count;i++)
            {
                if(tf[item].ContainsKey(words[i])) VectorDocument[i] = tf[item][words[i]] * idf[words[i]];
                else VectorDocument[i] = 0;
            }
            Vectorsdocuments[item] = VectorDocument.ToList();
        }
        return Vectorsdocuments;
    }
    public Dictionary<Dictionary<string, Dictionary<string,double>>, Dictionary<string, double>> Load()
    {
        string[] Namedocuments = Directory.GetFiles("/home/reynol/Escritorio/moogle-main/Content");
       
        Dictionary<string, List<string>> wordfordocument = new Dictionary<string, List<string>>();
            
        Dictionary<string, Dictionary<string, double>> Tf = new Dictionary<string, Dictionary<string, double>>();
        
        Dictionary<string, double> idf = new Dictionary<string, double>();
            
        foreach (string document in Namedocuments)
        {
            string documents = File.ReadAllText(document);
            
            if(!Tf.ContainsKey(document)) Tf[document] = new Dictionary<string, double>();
            
            foreach(string word in documents.ToLower().Split(separadores, StringSplitOptions.RemoveEmptyEntries).ToList())
            {
                if(!wordfordocument.ContainsKey(word)) wordfordocument[word] = new List<string>();
                
                if(!wordfordocument[word].Contains(document)) wordfordocument[word].Add(document);
                

                if(Tf[document].ContainsKey(word)) Tf[document][word]++; else Tf[document][word] = 1;
                
            }
            foreach(string key in Tf[document].Keys)
            {
                Tf[document][key] = Tf[document][key] / Tf[document].Values.Max();
            }
        }
        foreach (string key in wordfordocument.Keys)
        {
            idf[key] = Math.Log((double)(Namedocuments.Length)/(wordfordocument[key].Count), 10);
        } 

        Dictionary<Dictionary<string, Dictionary<string,double>>, Dictionary<string, double>> TFIDF = new Dictionary<Dictionary<string, Dictionary<string, double>>, Dictionary<string, double>>();
        TFIDF.Add(Tf, idf);
        
        return TFIDF;
    }  
    public Dictionary<string, Dictionary<string, double>> TF()
    {
        Dictionary<string, Dictionary<string, double>> tf = new Dictionary<string, Dictionary<string, double>>();
        foreach (Dictionary<string, Dictionary<string,double>> item in TFIDF.Keys)
        {
           tf = item;
        }
        return tf;
    }
    public Dictionary<string, double> IDF()
    {
        Dictionary<string, double> idf = new Dictionary<string, double>();
        foreach (Dictionary<string, Dictionary<string,double>> item in TFIDF.Keys)
        {
            idf = TFIDF[item];
        }
        return idf;
    }
}