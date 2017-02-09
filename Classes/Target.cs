using System;
using System.Drawing;
using GXPEngine;

public class Target : Canvas
{
    public readonly int radius;
    private Vec2 _position;

    private Color _ballColor;

    public Target(int pRadius, Vec2 pPosition = null, Color? pColor = null) : base(pRadius * 2, pRadius * 2)
    {
        radius = pRadius;
        position = pPosition;

        _ballColor = pColor ?? Color.Blue;

        draw();
        x = (float)position.x;
        y = (float)position.y;
    }

    private void draw()
    {
        SetOrigin(radius, radius);

        graphics.FillPolygon(
                    new SolidBrush(_ballColor), 
                    new PointF[] {
                    new PointF (0, 0),                  //top left
                    new PointF (2*radius, 0),           //top right
                    new PointF (2*radius, 2*radius),    //bottom right
                    new PointF (0, 2*radius)            //bottom left
            }
        );
    }

    public Vec2 position
    {
        set
        {
            _position = value ?? Vec2.zero;
        }
        get
        {
            return _position;
        }
    }
}