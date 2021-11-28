using RobotBrain;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

struct keyCodeChar
{
    public keyCodeChar(string k, char c)
    {
        code = k;
        val = c;
    }
    public string code;
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

    private RobotBrain.Brain brain = new RobotBrain.Brain();

    private int[] charLimit = new int[2] { 24, 14 };

    private keyCodeChar[] keyInputCodes = new keyCodeChar[]
    {
        new keyCodeChar("a", 'A'),
        new keyCodeChar("b", 'B'),
        new keyCodeChar("c", 'C'),
        new keyCodeChar("d", 'D'),
        new keyCodeChar("e", 'E'),
        new keyCodeChar("f", 'F'),
        new keyCodeChar("g", 'G'),
        new keyCodeChar("h", 'H'),
        new keyCodeChar("i", 'I'),
        new keyCodeChar("j", 'J'),
        new keyCodeChar("k", 'K'),
        new keyCodeChar("l", 'L'),
        new keyCodeChar("m", 'M'),
        new keyCodeChar("n", 'N'),
        new keyCodeChar("o", 'O'),
        new keyCodeChar("p", 'P'),
        new keyCodeChar("q", 'Q'),
        new keyCodeChar("r", 'R'),
        new keyCodeChar("s", 'S'),
        new keyCodeChar("t", 'T'),
        new keyCodeChar("u", 'U'),
        new keyCodeChar("v", 'V'),
        new keyCodeChar("w", 'W'),
        new keyCodeChar("x", 'X'),
        new keyCodeChar("y", 'Y'),
        new keyCodeChar("z", 'Z'),

        new keyCodeChar("0", '0'),
        new keyCodeChar("1", '1'),
        new keyCodeChar("2", '2'),
        new keyCodeChar("3", '3'),
        new keyCodeChar("4", '4'),
        new keyCodeChar("5", '5'),
        new keyCodeChar("6", '6'),
        new keyCodeChar("7", '7'),
        new keyCodeChar("8", '8'),
        new keyCodeChar("9", '9'),

        new keyCodeChar("[0]", '0'),
        new keyCodeChar("[1]", '1'),
        new keyCodeChar("[2]", '2'),
        new keyCodeChar("[3]", '3'),
        new keyCodeChar("[4]", '4'),
        new keyCodeChar("[5]", '5'),
        new keyCodeChar("[6]", '6'),
        new keyCodeChar("[7]", '7'),
        new keyCodeChar("[8]", '8'),
        new keyCodeChar("[9]", '9'),

        new keyCodeChar("space", ' '),
        new keyCodeChar("-", '-'),
        new keyCodeChar("=", '='),
    };

    private keyCodeChar[] shiftKeyInputCodes = new keyCodeChar[]
    {
        new keyCodeChar("7", '&'),
        new keyCodeChar("9", '('),
        new keyCodeChar("0", ')'),
    };
    private char[][] screen; 

    string command = "";
    const char emptyChar = char.MaxValue;

    ItemList tradeables;
    Inventory inventory;
    public void Start()
    {
        inventory = new Inventory(tradeables.obj);
        screen = new char[charLimit[0]][];
        for(int x = 0; x < charLimit[0]; x++)
        {
            screen[x] = new char[charLimit[1]];
            for(int y = 0; y < charLimit[1]; y++)
            {
                screen[x][y] = emptyChar;
            }
        }

        addString("Mech initalized\n\n(type help to get commands)\n\n");
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            for (int i1 = 0; i1 < shiftKeyInputCodes.Length; i1++)
            {
                if (Input.GetKeyUp(shiftKeyInputCodes[i1].code))
                {
                    addChar(shiftKeyInputCodes[i1].val);
                }
            }
        }
        else
        {
            for (int i1 = 0; i1 < keyInputCodes.Length; i1++)
            {
                if (Input.GetKeyUp(keyInputCodes[i1].code))
                {
                    addChar(keyInputCodes[i1].val);
                }
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
            command = command.Replace('\n', ' ');
            command += $"\n";
            print(command);
            brain.processLine(command.ToLower());

            

            print(brain.hasCommands());
            //print();
            shiftLineUp();
            command = "";
        }
        textArea.text = screenToString();

        if(brain.hasCommands())
        {
            switch(brain.peekCommand())
            {
                case MechCommand.MechRotate rotCmd:
                    if(mech.getState() == MechState.STOP)
                    {
                        mech.setRot(rotCmd.angle);
                        brain.nextCommand();
                    }
                    break;
                case MechCommand.MechMove movCmd:
                    if (mech.getState() == MechState.STOP)
                    {
                        mech.setDist(movCmd.distance);
                        brain.nextCommand();
                    }
                    break;
            }
        }
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
    void addString(string str)
    {
        shiftLineUp();
        for(int i1 =0; i1 < str.Length; i1++)
        {
            if (str[i1] == '\n')
            {
                shiftLineUp();
            }
            else
            {
                addChar(str[i1]);
            }
        }
    }
}
