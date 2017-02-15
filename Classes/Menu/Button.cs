using System;
using System.Drawing;
using GXPEngine;

public class Button : Sprite
{
    public float radius;
    private float _distanceX, _distanceY;

    public Button(string pFileName) : base(pFileName)
    {
        SetOrigin(width / 2, height / 2);
        radius = 60 / 2;
        alpha = 0;
        SetScaleXY(0.7f);
    }

    private void ButtonAppear()
    {
        if (alpha <= 1)
        {
            alpha += 0.05f;
        }
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
            return false;
        }
    }

    private void Update()
    {
        MouseHover();
        ButtonAppear();
    }
}

