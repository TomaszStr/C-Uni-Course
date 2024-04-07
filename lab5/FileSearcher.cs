
class FileSearcher{
    public String dirPath;
    public String regex;
    public bool EndMe=false;
    public bool done=false;
    Queue<String> found = new Queue<String>();

    public FileSearcher(String path, String reg){
        dirPath = path;
        regex = reg;
    }

    public void search(){//String path, Queue<String> list){
        foreach(String fileName in Directory.GetFiles(dirPath,regex,SearchOption.AllDirectories)){
            found.Enqueue(fileName);
            //Console.WriteLine(fileName);
        }
        done = true;
    }

    public void start(){
        Console.WriteLine("START SEARCH");
        while(!done || found.Count > 0)
            if(found.Count > 0)
                Console.WriteLine("FOUND: "+found.Dequeue());
        Console.WriteLine("SEARCH DONE");
    }

}