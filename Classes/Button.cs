using System;
using System.Drawing;
using GXPEngine;

public class Button : Canvas
{
    private Color _buttonColor;
    public Button(int pWidth, int pHeight) : base(pWidth, pHeight)
    {
        width = pWidth;
        height = pHeight;
        _buttonColor = Color.DarkBlue;

        Draw();
    }

    public Color color
    {
        get
        {
            return _buttonColor;
        }
        set
        {
            _buttonColor = value;
        }
    }
    private void Draw()
    {
        SetOrigin(width / 2, height / 2);

        // Create brush
        SolidBrush brush = new SolidBrush(_buttonColor);

        // Create rectangle to bound ellipse
        RectangleF rectangle = new RectangleF(0, 0, width, height);

        // Draw circle to screen.
        graphics.FillRectangle(brush, rectangle);
    }

    private void Update()
    {
        Draw();
    }
}

