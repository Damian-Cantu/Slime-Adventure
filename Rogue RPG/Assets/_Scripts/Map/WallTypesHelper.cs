using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallTypesHelper
{
    public static HashSet<int> wallTop = new HashSet<int>
    {
        0b1111,
        0b0110,
        0b0011,
        0b0010,
        0b1010,
        0b1100,
        0b1110,
        0b1011,
        0b0111
    };

    public static HashSet<int> wallSideLeft = new HashSet<int>
    {
        0b0100
    };

    public static HashSet<int> wallSideRight = new HashSet<int>
    {
        0b0001
    };

    public static HashSet<int> wallBottom = new HashSet<int>
    {
        0b1000
    };

    public static HashSet<int> wallInnerCornerUpRight = new HashSet<int>
    {
        0b11100000,
        0b11110001,
        0b11110000,
        0b11100001,
        0b11100100,
        0b11110100,
        0b11100101,
        0b11110101,
        0b10110000,
        0b10100000,
        0b10100001,
        0b10110001,
        0b10100101,
    };

    public static HashSet<int> wallInnerCornerUpLeft = new HashSet<int>
    {
        0b10000011,
        0b11000111,
        0b11000011,
        0b10000111,
        0b10010011,
        0b11010111,
        0b11010011,
        0b10010111,
        0b10000010,
        0b10000110,
        0b11000010,
        0b11000110,
        0b11010110,
    };

    public static HashSet<int> wallInnerCornerDownLeft = new HashSet<int>
    {
        0b00011110,
        0b00001110,
        0b00001111,
        0b00011111,
        0b01011111,
        0b01001110,
        0b01011110,
        0b01001111,
        0b00011011,
        0b00011010,
        0b01011011,
        0b00001011, 
        0b01011010,
    };

    public static HashSet<int> wallInnerCornerDownRight = new HashSet<int>
    {        
        0b00111100,
        0b00111000,
        0b01111100,
        0b01111000,
        0b00111001,
        0b01111101,
        0b00111101,
        0b01111001,
        0b01101000,
        0b01101001,
        0b00101100,
        0b01101100,
        0b01101101,
        0b00101000,
    };

    public static HashSet<int> wallDiagonalCornerDownLeft = new HashSet<int>
    {
        0b00000100
    };

    public static HashSet<int> wallDiagonalCornerDownRight = new HashSet<int>
    {
        0b00010000
    };

    public static HashSet<int> wallDiagonalCornerUpLeft = new HashSet<int>
    {
        0b00000001,
    };

    public static HashSet<int> wallDiagonalCornerUpRight = new HashSet<int>
    {
        0b01000000,
    };
    public static HashSet<int> wallTopLeftBottomRight = new HashSet<int>
    {
        0b00010001,
    };
    public static HashSet<int> wallTopRightBottomLeft = new HashSet<int>
    {
        0b01000100,
    };

    public static HashSet<int> wallFullEightDirections = new HashSet<int>
    {
        0b11111111,
        0b10111111,
        0b11101111,
        0b11111011,
        0b11111110,
        0b10111110,
        0b11111010,
        0b10111011,
        0b11101110,
    };

    public static HashSet<int> wallUtileUp = new HashSet<int>
    {
        0b00111110,
        0b01111111,
        0b00111111,
        0b01111110,
        0b00111010,
        0b01101110,
        0b00111010,
        0b01101110,
        0b01101111,
        0b01111011,
        0b00101110,
        0b01111010,
        0b00111011,
        0b00101111,
    };

    public static HashSet<int> wallUtileRight = new HashSet<int>
    {
        0b10001111,
        0b11011111,
        0b11001111,
        0b10011111,
        0b11001011,
        0b11001110,
        0b10001110,
        0b11011011,
        0b10001011,
        0b11011110,
        0b11011010,
        0b10011011,
    };

    public static HashSet<int> wallUtileDown = new HashSet<int>
    {
        0b11100011,
        0b11110111,
        0b11110011,
        0b11100111,
        0b10100111,
        0b11100110,
        0b11110110,
        0b11100010,
        0b10110011,
        0b10110111,
        0b10100011,
        0b11110010,
        0b10110010,
    };

    public static HashSet<int> wallUtileLeft = new HashSet<int>
    {
        0b11111000,
        0b11111101,
        0b11111100,
        0b11111001,
        0b11101100,
        0b10101100,
        0b10101001,
        0b10101101,
        0b11101101,
        0b10111000,
        0b10111101,
        0b11101000,
        0b10111100,
        0b11101001,
    };

    public static HashSet<int> wallTopandBottom = new HashSet<int>
    {
        0b10001000,
        0b11001000,
        0b11001001,
        0b10011100,
        0b10011000,
        0b10001100,
        0b11011000,
        0b10011101,
        0b11001101,
        0b11011001,
        0b11011100,
        0b11011101,
        0b11011101,
        0b10001101,
        0b10001001,
        0b11001100,
        0b11011110,
        0b11011110,
        0b10011001,
    };

    public static HashSet<int> wallRightandLeft = new HashSet<int>
    {
        0b00100010,
        0b01100010,
        0b00110010,
        0b00100110,
        0b00100011,
        0b01110010,
        0b01100110,
        0b01100011,
        0b01110110,
        0b01110011,
        0b01110111,
        0b00110110,
        0b01100111,
        0b00110111,
        0b00110011,
        0b00100111,

    };



}