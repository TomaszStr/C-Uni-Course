
class Producer{
    public int id;
    public int count;
    public bool endMe=false;
    int delay;
    Mutex mutex;
    public Queue<Product> destination;

    public Producer(int Id, int Delay, Queue<Product> Destination,Mutex mut){
        count = 0;
        id = Id;
        delay = Delay;
        mutex = mut;
        destination = Destination;
    }

    public Product produce(){
        count++;
        return new Product(id);
    }

    public void start(){
        while(!endMe){
            mutex.WaitOne();
            destination.Enqueue(produce());
            mutex.ReleaseMutex();
            Thread.Sleep(delay);
        }
        Console.WriteLine("Producer:{0}, produced:{1}",id,count);
    }

    public void stop(){
        endMe = true;
    }
}