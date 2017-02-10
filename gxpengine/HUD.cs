using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections.Specialized;
using System.Drawing.Text;
using GXPEngine;

public class HUD : Sprite
{
    public HUD() : base("HUD.png")
    {
        const int HEALTH_BAR_WIDTH = 365;
        const int HEALTH_BAR_OUTLINE_WIDTH = HEALTH_BAR_WIDTH + 1;
    }
}
