﻿using System;
using System.Drawing;
using GXPEngine;

public class Button : AnimationSprite
{
    public float radius;
    private float _distanceX, _distanceY;

    private Sound _hoverButton;
    private Sound _clickButton;

    private bool hasPlayed;

    public Button(string pFileName, int pCols = 1, int pRows = 1) : base(pFileName, pCols, pRows)
    {
        SetOrigin(width / 2, height / 2);
        radius = 60 / 2;
        alpha = 0;

        _hoverButton = new Sound("assets\\sfx\\buttonsound.wav");
        _clickButton = new Sound("assets\\sfx\\buttonsv2.wav");
    }

    private void ButtonAppear()
    {
        if (alpha <= 0.5f)
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
            alpha = 1f;
            SetScaleXY(1.1f);
            if (hasPlayed == false)
            {
                //_hoverButton.Play();
                hasPlayed = true;
            }
            return true;
        }

        else
        {
            SetScaleXY(1f);
            if (alpha >= 0.5f)
            {
                alpha -= 0.05f;
            }
            hasPlayed = false;
            return false;
        }
    }

    private void MouseClick()
    {
        if (Input.GetMouseButtonUp(0) && MouseHover() == true)
        {
            _clickButton.Play();
        }
    }

    private void Update()
    {
        MouseClick();
        MouseHover();
        ButtonAppear();
    }
}

