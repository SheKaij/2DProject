using GXPEngine;

public class FadeIn : Sprite
{
    public FadeIn() : base("assets/black_screen.jpg")
    {
        alpha = 0;
    }

    private void Update()
    {
        alpha += 0.008f;
        if (alpha >= 1)
        {
            Destroy();
        }
    }
}