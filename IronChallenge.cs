using System;
using System.Collections.Generic;
using System.Threading;

namespace OldPhonePadApp
{
    class Program
    { 
        private static readonly Dictionary<char, string> keyMap = new Dictionary<char, string>()
           {
            {'1', "&'("}, {'2', "ABC"}, {'3', "DEF"}, {'4', "GHI"}, {'5', "JKL"},
            {'6', "MNO"}, {'7', "PQRS"}, {'8', "TUV"}, {'9', "WXYZ"}, {'0', " "}
        };
        static void Main(string[] args)
        {  
            Console.Clear();
            Console.WriteLine("Old Phone Pad Simulator");
            Console.WriteLine("Press number keys to  type. Press '#' to  send, '*' to backspace");
            Console.WriteLine("Press 0 four times to exit.");

            string output = "";
            char currentKey = '\0';
            int pressCount = 0;
            bool isRunning = true;
            DateTime lastKeyPressTime = DateTime.MinValue;
            string exitSequence = "";

            while(isRunning)
            {
                if(Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    char ch = keyInfo.KeyChar;

                    if(ch == '0')
                    {
                        exitSequence += '0';
                    
                        if(exitSequence.Length >= 4)
                        {
                            isRunning = false;
                            break;
                        }

                    } 
                    
                    else
                    {
                        exitSequence = "";
                    }

                    if (ch == '#')
                    {
                        if(currentKey != '\0' && pressCount > 0 )
                        {
                            string letters = keyMap[currentKey];
                    
                            if (letters.Length > 0)
                            {
                                int index = (pressCount - 1) % letters.Length;
                                output += letters[index];
                            }
                            
                            currentKey = '\0';
                            pressCount = 0;
                        }                        
                        
                        Console.WriteLine("Final text: " + output);  
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);

                        output = "";
                        currentKey = '\0';
                        pressCount = 0;
                        exitSequence = "";
                        lastKeyPressTime = DateTime.MinValue;
                        UpdateDisplay(output, currentKey, pressCount);

                    }
                    
                    else if(ch == '*')
                    {
                        if (pressCount > 0)
                        {
                            currentKey = '\0';
                            pressCount =0;                            
                        }
                        
                        else if (output.Length > 0)
                        {
                            output = output.Substring(0, output.Length - 1);
                        }                        
                    }
                    
                    else if(char.IsDigit(ch))
                    {
                        if(currentKey == ch)
                        {
                            pressCount++;
                        }
                        
                        else
                        {
                            if(currentKey != '\0' && pressCount > 0)
                            {
                                string letters = keyMap[currentKey];

                                if(letters.Length > 0 )
                                {
                                    int index = (pressCount - 1) % letters.Length;
                                    output += letters[index];
                                }
                            }

                            currentKey = ch;
                            pressCount = 1;
                        }

                        lastKeyPressTime = DateTime.Now;
                    }

                    UpdateDisplay(output, currentKey, pressCount);

                }
                
                else
                {
                    if(currentKey != '\0' && pressCount > 0)
                    {
                        TimeSpan timeSinceLastKeyPress = DateTime.Now - lastKeyPressTime;

                        if(timeSinceLastKeyPress.TotalSeconds >= 1)
                        {
                            string letters = keyMap[currentKey];

                            if (letters.Length > 0)
                            {
                                int index = (pressCount - 1) % letters.Length;
                                output += letters[index];
                            }
                            
                            currentKey = '\0';
                            pressCount = 0;
                            UpdateDisplay(output, currentKey, pressCount);
                        }
                    }
                }

                Thread.Sleep(50);
            }

            Console.WriteLine();
            Console.WriteLine("Shutting down...");
        }

        private static void UpdateDisplay(string output, char currentKey, int pressCount)
        {
            Console.Clear();
            Console.WriteLine("Old Phone Pad Simulator");
            Console.WriteLine("Press number keys to  type. Press '#' to  send, '*' to backspace");
            Console.WriteLine("Press 0 four times to exit.");
            
            string display = output;

            if(currentKey != '\0' && pressCount > 0)
            {
                string letters = keyMap[currentKey];

                if(letters.Length > 0)
                {
                    int index = (pressCount - 1) % letters.Length;
                    display += letters[index];
                }
            }

            Console.WriteLine("Current text: " + display);            
        }
    }
}
