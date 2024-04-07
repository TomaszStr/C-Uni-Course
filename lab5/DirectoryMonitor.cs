
using System.Linq.Expressions;

class DirectoryMonitor{
    public bool EndMe=false;
    public String dirPath;
    public HashSet<String> files = new HashSet<String>();
    public DirectoryMonitor(String path){
        dirPath = path;
        files = new HashSet<string>(Directory.GetFileSystemEntries(dirPath,"*",SearchOption.TopDirectoryOnly));
    }
    public void checkFiles(){
        HashSet<String> newFiles = new HashSet<string>(Directory.GetFileSystemEntries(dirPath,"*",SearchOption.TopDirectoryOnly));
        foreach(String file in files.Except(newFiles))
            Console.WriteLine("DELETED: {0}",file);
        foreach(String file in newFiles.Except(files))
            Console.WriteLine("ADDED: {0}",file);
        files = newFiles;
    }
    public void start(){
        Console.WriteLine("MONITOR STARTED");
        while(!EndMe){
            checkFiles();
        }
        Console.WriteLine("MONITOR STOPPED");
    }
    public void listenKey(){
        Console.WriteLine("Press q to stop program");
        while(Console.ReadKey(true).KeyChar != 'q'){}
        EndMe  = true;
    }
}