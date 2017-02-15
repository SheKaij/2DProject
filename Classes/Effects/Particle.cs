using System;
using GXPEngine;


public class Particle : AnimationSprite
{
    private int _timer = 100;

    public Particle(string pFilename, int pCols, int pRows) : base(pFilename, pCols, pRows)
    {
        SetOrigin(width / 2, height / 2);
    }

    private void Update()
    {
        alpha = _timer / 500.0f;
        _timer--;
        if (_timer <= 0)
        {
            Destroy();
        }
    }
}