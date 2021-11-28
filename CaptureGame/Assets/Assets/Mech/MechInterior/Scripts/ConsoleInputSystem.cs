using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

struct keyCodeChar
{
    public keyCodeChar(KeyCode k, char c)
    {
        code = k;
        val = c;
    }
    public KeyCode code;
    public char val;
}

public class ConsoleInputSystem : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textArea;

    [SerializeField]
    MechMovementControler mech;

    [SerializeField]
    MechMovementControler camera;

    private int[] charLimit = new int[2] { 24, 14 };

    private keyCodeChar[] keyInputCodes = new keyCodeChar[]
    {
        new keyCodeChar(KeyCode.A, 'A'),
        new keyCodeChar(KeyCode.B, 'B'),
        new keyCodeChar(KeyCode.C, 'C'),
        new keyCodeChar(KeyCode.D, 'D'),
        new keyCodeChar(KeyCode.E, 'E'),
        new keyCodeChar(KeyCode.F, 'F'),
        new keyCodeChar(KeyCode.G, 'G'),
        new keyCodeChar(KeyCode.H, 'H'),
        new keyCodeChar(KeyCode.I, 'I'),
        new keyCodeChar(KeyCode.J, 'J'),
        new keyCodeChar(KeyCode.K, 'K'),
        new keyCodeChar(KeyCode.L, 'L'),
        new keyCodeChar(KeyCode.M, 'M'),
        new keyCodeChar(KeyCode.N, 'N'),
        new keyCodeChar(KeyCode.O, 'O'),
        new keyCodeChar(KeyCode.P, 'P'),
        new keyCodeChar(KeyCode.Q, 'Q'),
        new keyCodeChar(KeyCode.R, 'R'),
        new keyCodeChar(KeyCode.S, 'S'),
        new keyCodeChar(KeyCode.T, 'T'),
        new keyCodeChar(KeyCode.U, 'U'),
        new keyCodeChar(KeyCode.V, 'V'),
        new keyCodeChar(KeyCode.W, 'W'),
        new keyCodeChar(KeyCode.X, 'X'),
        new keyCodeChar(KeyCode.Y, 'Y'),
        new keyCodeChar(KeyCode.Z, 'Z'),

        new keyCodeChar(KeyCode.Keypad0, '0'),
        new keyCodeChar(KeyCode.Keypad1, '1'),
        new keyCodeChar(KeyCode.Keypad2, '2'),
        new keyCodeChar(KeyCode.Keypad3, '3'),
        new keyCodeChar(KeyCode.Keypad4, '4'),
        new keyCodeChar(KeyCode.Keypad5, '5'),
        new keyCodeChar(KeyCode.Keypad6, '6'),
        new keyCodeChar(KeyCode.Keypad7, '7'),
        new keyCodeChar(KeyCode.Keypad8, '8'),
        new keyCodeChar(KeyCode.Keypad9, '9'),

        new keyCodeChar(KeyCode.Space, ' '),
    };

    private char[][] screen; 

    string command = "";
    const char emptyChar = char.MaxValue;

    private float coolDownTime = -1;
    public void Start()
    {
        screen = new char[charLimit[0]][];
        for(int x = 0; x < charLimit[0]; x++)
        {
            screen[x] = new char[charLimit[1]];
            for(int y = 0; y < charLimit[1]; y++)
            {
                screen[x][y] = emptyChar;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        for(int i1 = 0; i1 < keyInputCodes.Length; i1++)
        {
            if(Input.GetKeyUp(keyInputCodes[i1].code))
            {
                addChar(keyInputCodes[i1].val);
            }
        }
        if(Input.GetKeyUp(KeyCode.Backspace))
        {
            backSpace();
        }
        else if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyUp(KeyCode.Return))
        {
            shiftLineUp();
        }
        else if(Input.GetKeyUp(KeyCode.Return))
        {
            //do something with the comand
            shiftLineUp();
            command = "";
        }
        textArea.text = screenToString();
    }

    private string screenToString()
    {
        string tmp = "";
        for (int y = charLimit[1] - 1; y >= 0; y--)
        {
            for (int x = 0; x < charLimit[0]; x++)
            {

                if (screen[x][y] == emptyChar)
                {
                    tmp += " ";
                }
                else
                {
                    tmp += $"{screen[x][y]}";
                }
            }
            tmp += "\n";
        }
        return tmp;
    }
    private void shiftLineUp()
    {
        for (int y = charLimit[1] - 1; y > 0 ; y--)   
        {
            for (int x = 0; x < charLimit[0]; x++)
            {
                screen[x][y] = screen[x][y - 1];
            }
        }
        for (int x = 0; x < charLimit[0]; x++)
        {
            screen[x][0] = emptyChar;
        }
    }
    private void shiftLineDown()
    {
        for (int y = 0; y < charLimit[1] - 1; y++)
        {
            for (int x = 0; x < charLimit[0]; x++)
            {
                screen[x][y] = screen[x][y + 1];
            }
        }
        for (int x = 0; x < charLimit[0]; x++)
        {
            screen[x][charLimit[1] - 1] = emptyChar;
        }
    }
    private void backSpace()
    {
        if(command.Length == 0)
        {
            return;
        }
        command = command.Substring(0, command.Length - 1);
        for (int i1 = charLimit[0] - 1; i1 >= 0; i1--)
        {
            if (screen[i1][0] != emptyChar)
            {
                screen[i1][0] = emptyChar;
                return;
            }
        }
        shiftLineDown();
        for (int i1 = charLimit[0] - 1; i1 >= 0; i1--)
        {
            if (screen[i1][0] != emptyChar)
            {
                screen[i1][0] = emptyChar;
                return;
            }
        }
    }
    void addChar(char c)
    {
        command += $"{c}";
        for(int i1 = 0; i1 < charLimit[0]; i1++)
        {
            if(screen[i1][0] == emptyChar)
            {
                screen[i1][0] = c;
                return;
            }
        }
        shiftLineUp();
        screen[0][0] = c;
    }

}
