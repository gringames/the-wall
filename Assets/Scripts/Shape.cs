using System;
using System.Collections.Generic;
using System.Linq;

public enum Shape
{
    Triangle,
    Square,
    Circle,
}

public static class ShapeAccess
{
    private static readonly List<Shape> Values = Enum.GetValues(typeof(Shape)).Cast<Shape>().ToList();
    private static readonly int Size = Values.Count;

    public static Shape GetRandomShape()
    {
        var index = UnityEngine.Random.Range(0, Size);
        return Values[index];
    }
}