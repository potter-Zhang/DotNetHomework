using System.Security.Cryptography;

public abstract class Shape
{
    // return area of shapes
    abstract public double CalculateArea();
    // determine whether a shape is legal or not
    abstract public bool IsLegal();
}

class Square : Shape
{
    private double Width { get; set; }
    public Square(double[] parameters)
    {
        Width = parameters[0];
    }
    public override double CalculateArea()
    {
        return Width * Width;
    }
    public override bool IsLegal()
    {
        return Width > 0;
    }
}


class Rectangle : Shape
{
    private double Height { get; set; }
    private double Width { get; set; }
    public Rectangle(double[] parameters)
    {
        Height = parameters[0];
        Width = parameters[1];
    }
    public override double CalculateArea()
    {
        return Height * Width;
    }
    public override bool IsLegal()
    {
        return Height > 0 && Width > 0;
    }
}
class Circle : Shape
{
    private double Radius { get; set; }
    public Circle(double[] parameters)
    {
        Radius = parameters[0];
    } 
    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    } 
    public override bool IsLegal()
    {
        return Radius > 0;
    }
}

class Triangle : Shape
{
    private double A { get; set; }
    private double B { get; set; }
    private double C { get; set; }
    public Triangle(double[] parameters)
    {
        A = parameters[0];  
        B = parameters[1];
        C = parameters[2];
    }
    public override double CalculateArea()
    {
        double p = (A + B + C) / 2;
        return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
    }
    public override bool IsLegal()
    {
        double twoSides = Math.Min(B + C, Math.Min(A + B, A + C));
        double oneSide = Math.Max(A, Math.Max(B, C));
        return twoSides > oneSide;

    }
}

class Factory
{
    public static Shape Create(string shape, params double[] parameters)
    {
        Shape s;
        if (shape == "rectangle" && parameters.Length >= 2)
            s = new Rectangle(parameters);
        else if (shape == "circle" && parameters.Length >= 1)
            s = new Circle(parameters);
        else if (shape == "triangle" && parameters.Length >= 3)
            s = new Triangle(parameters);
        else if (shape == "square" && parameters.Length >= 1)
            s = new Square(parameters);
        else
            throw new Exception("Error: Invalid shape or missing parameters!");
        if (!s.IsLegal())
            throw new Exception("Error: Illegal parameters!");
        return s;
    }
}

class Test
{
    static void Main(string[] args)
    {
        const int N = 10;
        Random rd = new Random();
        double[] parameters = { 1, 3, 5 };
        string[] shapes = { "rectangle", "circle", "triangle", "square" };
        double sumOfArea = 0;
        List<Shape> listOfShape = new List<Shape>();
        try
        {
            for (int i = 0; i < N; i++)
            {
                int randomIdx = rd.Next(shapes.Length);
                RandomParamsGenerator(parameters);
                listOfShape.Add(Factory.Create(shapes[randomIdx], parameters));
                sumOfArea += listOfShape[i].CalculateArea();
                Console.WriteLine(shapes[randomIdx] + " with area = " + listOfShape[i].CalculateArea());
            }
        } 
        catch(Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        //Factory.Create(shapes[0], 2.0, 3.0, 4.0, 5.0);

    }

    static void RandomParamsGenerator(double[] parameters)
    {
        if (parameters == null)
            return;
        Random rd = new Random();
        for (int i = 0; i < parameters.Length; i++)
        {
            parameters[i] = 10 * rd.NextDouble();
        }
    }
}

