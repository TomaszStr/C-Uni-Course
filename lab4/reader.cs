
class Reader<T>{
    static public List<T> ReadList(String path,String sep, bool header, Func<String[],T> construct){
        List<T> result = new List<T>();
        String ?line;
        String[] vals;
        System.IO.StreamReader reader = new StreamReader(path);
        // skip the header if it's present
        if(header)
            reader.ReadLine();
        while((line = reader.ReadLine()) != null){
            vals = line.Split(sep);
            result.Add(construct(vals));
        }
        return result;
    }
}