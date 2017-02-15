using GXPEngine;

public class FadeOut : Sprite
{
    public FadeOut() : base("assets/black_screen.jpg")
    {
        // base alpha is already 1
    }

    private void Update()
    {
        alpha -= 0.008f;
        if (alpha <= 0)
        {
            Destroy();
        }
    }
}

