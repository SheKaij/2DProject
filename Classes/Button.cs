using System;
using System.Drawing;
using GXPEngine;

public class Button : Sprite
{
    public float radius;
    private float _distanceX, _distanceY, _distanceTotal;

    public Button(string pFileName) : base(pFileName)
    {
        radius = 60 / 2;
        SetOrigin(width / 2, height / 2);
    }

    public bool MouseHover()
    {
        _distanceX = Mathf.Abs(Input.mouseX - x);
        _distanceY = Mathf.Abs(Input.mouseY - y);

        if (_distanceX < width / 2 && _distanceY < height / 2)
        {
            alpha = 0.5f;
            return true;
        }

        else
        {
            alpha = 1f;
            return false;
        }
    }

    private void Update()
    {
        MouseHover();
    }
}

