using System.Collections.Generic;

class Consumer{
    public int id;
    public bool endMe=false;
    int delay;
    Mutex ?mutex=null;
    public Dictionary<int,int> consumed;
    public Queue<Product> source;

    public Consumer(int Id,int Delay,Queue<Product> Source,Mutex mut){
        id = Id;
        delay = Delay;
        source = Source;
        consumed = new Dictionary<int,int>();
        mutex=mut;
    }
    public void consume(Product product){
        mutex.WaitOne();
        if(!consumed.ContainsKey(product.parentId))
            consumed[product.parentId] = 0;
        consumed[product.parentId]++;
        mutex.ReleaseMutex();
    }

    public void start(){
        while(!endMe){
            if(source.Count > 0){
                consume(source.Dequeue());
            }
            else Console.WriteLine("Consumer:{0} empty stack!",id);

            Thread.Sleep(delay);
        }
        Console.Write("Consumer:{0} consumed: ",id,consumed.Count);
        int[] sortedKeys = consumed.Keys.ToArray();
        Array.Sort(sortedKeys);
        foreach(int k in sortedKeys)
            Console.Write("Producer:{0}, Count{1} ",k,consumed[k]);
        Console.Write("\n");
    }

    public void stop(){
        endMe = true;
    }
}