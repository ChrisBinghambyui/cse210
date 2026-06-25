using System;

public class Fraction
{
    private int top;
    private int bottom;

    public Fraction()
    {
        top = 1;
        bottom = 1;
    }

    public Fraction(int t)
    {
        top = t;
        bottom = 1;
    }

    public Fraction(int t, int b)
    {
        top = t;
        SetBottom(b);
    }

    public int GetTop()
    {
        return top;
    }

    public int GetBottom()
    {
        return bottom;
    }

    public void SetTop(int t)
    {
        top = t;
    }

    public void SetBottom(int b)
    {
        if (b == 0)
        {
            bottom = 1;
        }
        else
        {
            bottom = b;
        }
    }

    public string GetFractionString()
    {
        return $"{top}/{bottom}";
    }

    public double GetDecimalValue()
    {
        return (double)top / bottom;
    }
}
