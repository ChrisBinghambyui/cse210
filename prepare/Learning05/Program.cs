using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Learning05 World!");

        List<Shape> shapes = new List<Shape>();
 
        shapes.Add(new Square("Red", 3));
        shapes.Add(new Rectangle("Blue", 4, 5));
        shapes.Add(new Circle("Green", 6));
 
        foreach (Shape s in shapes)
        {
            Console.WriteLine("The " + s.GetColor() + " shape has an area of " + s.GetArea() + ".");
        }
    }
}