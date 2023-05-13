namespace MoogleEngine;



public static class Moogle
{
    public static SearchResult Querys(string query) {
        // Modifique este método para responder a la búsqueda
        
        Query QUERY = new Query(query);
        
        ModeloVectorial Vectors = new ModeloVectorial(Object.document, QUERY);
        
        SearchItem[] items = new SearchItem[Vectors.MoreFrequency.Count];

        for(int y = 0; y < Vectors.MoreFrequency.Count;y++)
        {
            items[y] = new SearchItem(Vectors.MoreFrequency[y].Substring(44,Vectors.MoreFrequency[y].Length - 48),Snippet(Vectors.MoreFrequency[y], query), 0.9f);
        }
        
        return new SearchResult(items, query); 
    }
    public static string Snippet(string document, string q)
    {
        List<string> lines = File.ReadAllLines(document).ToList();
        foreach (string line in lines)
        {
            if(line.Contains(q)) return line;
        }
        return "No se ha encontrado la palabra";
    }
}
