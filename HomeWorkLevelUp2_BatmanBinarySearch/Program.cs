using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    static void Main(/*string[] args*/)
    {
        /*string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int W = int.Parse(inputs[0]); // width of the building.
        int H = int.Parse(inputs[1]); // height of the building.
        int N = int.Parse(Console.ReadLine()); // maximum number of turns before game over.
        inputs = Console.ReadLine().Split(' ');
        int X0 = int.Parse(inputs[0]);
        int Y0 = int.Parse(inputs[1]);*/
        var W = 12;
        var H = 10;
        var X0 = 2;
        var Y0 = 8;
        
        // game loop
        var leftX = 0;
        var rightX = W - 1;
        var leftY = 0;
        var rightY = H - 1;
        var x = X0;
        var y = Y0;
        while (true)
        {
            var bombDir = Console.ReadLine(); // the direction of the bombs from batman's current location (U, UR, R, DR, D, DL, L or UL)

            if (bombDir.Length == 2) // два направления
            {
                switch (bombDir[0])
                {
                    case 'U':
                    {
                        rightY = y;
                        break;
                    }
                    case 'D':
                    {
                        if (leftY == y && leftY == rightY - 1)
                            leftY = rightY;
                        else
                            leftY = y;
                        break;
                    }
                }
                
                switch (bombDir[1])
                {
                    case 'L':
                    {
                        rightX = x;
                        break;
                    }
                    case 'R':
                    {
                        if (leftX == x && leftX == rightX - 1)
                            leftX = rightX;
                        else
                            leftX = x;
                        break;
                    }
                }
            }
            else // одно направление
            {
                switch (bombDir[0])
                {
                    case 'U':
                    {
                        rightY = y;
                        break;
                    }
                    case 'D':
                    {
                        if (leftY == y && leftY == rightY - 1)
                            leftY = rightY;
                        else
                            leftY = y;
                        break;
                    }
                    case 'L':
                    {
                        rightX = x;
                        break;
                    }
                    case 'R':
                    {
                        if (leftX == x && leftX == rightX - 1)
                            leftX = rightX;
                        else
                            leftX = x;
                        break;
                    }
                }
            }


            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");


            // the location of the next window Batman should jump to.
            x = (leftX + rightX) / 2;
            y = (leftY + rightY) / 2;
            Console.WriteLine(x + " " + y);
        }
    }
}