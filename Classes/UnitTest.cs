using System;
using GXPEngine;

public class UnitTest
{
    public UnitTest()
    {

        Vec2 clonedVec = new Vec2();
        // TODO: Should replace the color check with a method, so that it only check if the necesarry statement is true, once

        //// Test the Add method
        //Vec2 vec1 = new Vec2(1, 1);
        //Console.WriteLine("(Add)Old vector: " + vec1);
        //vec1.Add(new Vec2(1, 1));       // Plus (1, 1)
        //Console.WriteLine("(Add)New vector: " + vec1);
        //Console.WriteLine("Is Add working: " + (vec1.x == 2 && vec1.y == 2) + "\n");

        //// Test the Substract method
        //Vec2 vec2 = new Vec2(5, 5);
        //Console.WriteLine("(Substract)Old vector: " + vec2);
        //vec2.Substract(new Vec2(1, 1));
        //Console.WriteLine("(Substract)New vector: " + vec2);
        //Console.WriteLine("Is Substract working: " + (vec2.x == 4 && vec2.y == 4) + "\n");
        ////Console.WriteLine("(Substract)Is Substract working? " + ((vec2.x == 4 && vec2.y == 4) ? Console.ForegroundColor = ConsoleColor.Green : Console.ForegroundColor = ConsoleColor.Red) + "\n");
        //Console.ResetColor();

        //// Test the Scale method
        //Vec2 vec3 = new Vec2(3, 3);
        //Console.WriteLine("(Scale)Old vector: " + vec3);
        //vec3.Scale(3);
        //Console.WriteLine("(Scale)New vector: " + vec3 + "; <with a scale of 3>");
        //Console.WriteLine("Is Scale working: " + (vec3.x == 9 && vec3.y == 9) + "\n");

        //// Test the Length method
        //Vec2 vec4 = new Vec2(3, 4);
        //Console.WriteLine("(Length)Current vector: " + vec4);
        //Console.WriteLine("(Length)Length of vector: " + vec4.Length() + "; <should be 5>");
        //Console.WriteLine("Is Length working: " + (vec4.Length() == 5) + "\n");

        //// Test the Normalize method
        //Vec2 vec5 = new Vec2(6, 8);
        //Console.WriteLine("(Normalize)Old vector: " + vec5);
        //vec5.Normalize();
        //Console.WriteLine("(Normalize)New vector: " + vec5);
        //Console.WriteLine("Is Normalize working: " + (vec5.x == 0.6f && vec5.y == 0.8f) + "\n");

        //// Test the ToString method
        //Vec2 vec6 = new Vec2(12, 12);
        //Console.WriteLine("(ToString)String of vector" + vec6.ToString());
        //Console.WriteLine("(ToString)Is ToString working: " + (vec6.ToString() == "(12, 12)") + "\n");

        //// Test the Clone method
        //Vec2 vec7 = new Vec2(5, 7);
        //Console.WriteLine("(Clone)Old vector: " + vec7);
        //clonedVec = vec7.Clone();
        //Console.WriteLine("(Clone)Cloned vector: " + clonedVec);
        //Console.WriteLine("Is Clone working: " + (vec7 != clonedVec) + "\n");   // clone is not the same instance

        //// Test the SetXY method
        //Vec2 vec8 = new Vec2(6, 2);
        //Console.WriteLine("(SetXY)Old vector: " + vec8);
        //vec8.SetXY(13, 13);
        //Console.WriteLine("(SetXY)New vector: " + vec8);
        //Console.WriteLine("Is SetXY working: " + (vec8.x == 13 && vec8.y == 13) + "\n");

        //// Test the Deg2Rad method
        float deg1 = 180f;
        Console.WriteLine("(Deg2Rad)Degrees: " + deg1);
        deg1 = Vec2.Deg2Rad(deg1);
        Console.WriteLine("(Deg2Rad)Converted Radians: " + deg1);
        Console.WriteLine("Is Deg2Rad working: " + (deg1 == Mathf.PI) + "\n");

        //// Test the Rad2Deg method
        //float rad1 = Mathf.PI;
        //Console.WriteLine("(Rad2Deg)Radians: " + rad1);
        //rad1 = Vec2.Rad2Deg(rad1);
        //Console.WriteLine("(Rad2Deg)Converted Degrees: " + rad1);
        //Console.WriteLine("Is Rad2Deg working: " + (rad1 == 180f) + "\n");

        // Test the GetAngleDegrees method
        Vec2 vec9 = new Vec2(8, 4);
        Console.WriteLine("(GetAngleDeg)Vector: " + vec9);
        Console.WriteLine("(GetAngleDeg)Degrees: " + vec9.GetAngleDegrees());
        Console.WriteLine("Is GetAngleDeg working: " + (vec9.GetAngleDegrees() == 26.56505f) + "\n");
        //Console.WriteLine("Is GetAngleDeg working: " + (vec9.GetAngleDegrees() == (Mathf.Atan2(4,8) * 180 / Mathf.PI)));

        // Test the GetAngleRadians method
        Vec2 vec10 = new Vec2(7, 17);
        Console.WriteLine("(GetAngleRad)Vector" + vec10);
        Console.WriteLine("(GetAngleRad)Radians: " + vec10.GetAngleRadians());
        Console.WriteLine("Is GetAngleRad working: " + (vec10.GetAngleRadians() == 1.18018925f) + "\n");
    }
}
