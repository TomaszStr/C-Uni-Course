using System.Text.RegularExpressions;

public class Tweets{
    List<Tweet>? _Data;

    public List<Tweet>? Data{
        get; set;
    }

    public void Sort(Comparer<Tweet> comparer){
        if(Data != null)
            Data.Sort(comparer);
        else
            throw new Exception("EMPTY LIST");
    }

    public Dictionary<string,List<Tweet>> ToDict(){
        if(Data == null)
            throw new Exception("EMPTY LIST");
        Dictionary<string,List<Tweet>> result = new Dictionary<string,List<Tweet>>();
        //iterate through tweets
        foreach(Tweet t in Data){
            if(!result.ContainsKey(t.UserName))//add key
                result[t.UserName] = new List<Tweet>();
            result[t.UserName].Add(t);//add tweet to list
        }

        return result;
    }

    public Dictionary<string,int> WordFrequency(){
        if(Data == null)
            throw new Exception("EMPTY LIST");
        Dictionary<string,int> result = new Dictionary<string,int>();
        foreach(Tweet t in Data){
            String text = t.Text;
            //String[] strings = t.Text.Split(" ");//simple version
            
            //MORE ACCURATE
            String pattern = @"\b\w+\b";
            MatchCollection matches = Regex.Matches(text,pattern);
            String[] strings = matches.Select(x => x.Value).ToArray();
            
            for (int i = 0; i < strings.Length; i++)
            {
                if(!result.ContainsKey(strings[i]))//add key
                    result[strings[i]] = 0;
                result[strings[i]]++;
            }
        }
        return result;
    }

    public Dictionary<string,double> TermIDF(){
        Dictionary<string,double> result = new Dictionary<string,double>();
        int len = Data.Count;

        foreach(Tweet t in Data){
            string pattern = @"\b\w+\b";
            MatchCollection matches = Regex.Matches(t.Text,pattern);
            List<String> unique = matches.Select(x => x.Value).ToList();
            unique = unique.Distinct().ToList();

            foreach(string u in unique){
                if(!result.ContainsKey(u))
                    result[u]=0;
                result[u]++;
            }
        }

        foreach(string key in result.Keys){
            result[key] = Math.Log(len/result[key]);
        }
        
        return result;

    }
}