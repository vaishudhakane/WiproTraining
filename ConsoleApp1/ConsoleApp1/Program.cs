class ABC
{
    //Example of single cast delegate
    //public delegate void Admin();
    //static void Main(string[] args)
    //{
    //    Admin a = new Admin(AdminMethod);

    //}
    //public static void AdminMethod()
    //{
    //    Console.WriteLine("Admin has generated invoices");
    //}

    //Code for addition using delegates and printing the result
    public delegate int Operation(int a, int b);
  
    public delegate void PrintDelegate(int result);

    public static void Main1(string[] args)
    {
        Operation add = Add;
        Operation subtract = Subtract;

        PrintDelegate print = Print;
        int r1 = add(5, 10);
        int r2 = subtract(50, 10);
        print(r1);
        print(r2);


    }
    public static int Add(int x, int y)
    {
        return x + y;
    }
    public static int Subtract(int c,int d)
    {
        return c - d;
    }
    public static void Print(int result)
    {
        Console.WriteLine("The result is: " + result);
    }

}
