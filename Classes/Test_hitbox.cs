using System;
using System.Drawing;
using GXPEngine;


public class Test_hitbox : Canvas
{
    private float _radius;
    private float _diameter;

    private float _area;
    private float _circumference;
    private Canvas hitbox;
    private Color _hitboxColor;

    public Test_hitbox(int pWidth, int Pheight) : base(pWidth, Pheight)
    {

        width = pWidth;
        height = Pheight;

        _diameter = width;
        _radius = _diameter / 2;
        _area = Mathf.PI * (Mathf.Pow(_radius, 2));
        _circumference = 2 * Mathf.PI * _radius;

        _hitboxColor = Color.Red;
        //DrawCircle();

        Console.WriteLine("Test radius: " + _radius);
        Console.WriteLine("Test area: " + _area);
        Console.WriteLine("Test circumference: " + _circumference);
    }

    public float radius
    {
       get
        {
            return _radius;
        }
    }

    private void DrawCircle()
    {
        SetOrigin(radius, radius);
        // Create brush
        SolidBrush brush = new SolidBrush(Color.DarkRed);

        // Create rectangle to bound ellipse
        RectangleF rectangle = new RectangleF(0, 0, radius * 2, radius * 2);

        // Draw circle to screen.
        graphics.FillEllipse(brush, rectangle);
    }

    private void DrawRectangle()
    {
        SetOrigin(radius, radius);
        // Create brush
        SolidBrush brush = new SolidBrush(Color.DarkRed);

        // Create rectangle to bound ellipse
        RectangleF rectangle = new RectangleF(0, 0, radius * 2, radius * 2);

        // Draw circle to screen.
        graphics.FillEllipse(brush, rectangle);
    }
}