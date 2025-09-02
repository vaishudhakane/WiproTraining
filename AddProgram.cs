using System;

public class AddProgram
{
	public delegate int AddDelegate(int a, int b);
	public delegate void PrintDelegate(int result);

	public static void Main(string[] args)
	{
		AddDelegate add = Add;


		PrintDelegate print = Print;
		int result = add(5, 10);
		print(result);
	

    }
	public static int Add(int x, int y)
	{
		return x + y;
    }
	public static void Print(int result)
	{
		Console.WriteLine("The result is: " + result);
    }



}
