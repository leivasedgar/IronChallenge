using System;
using System.Collections.Generic;

namespace OldPhonePadApp{
    class Program{ 
        Dictionary<char, string> keyMap = new Dictionary<char, string>(){
            {'1', "&'("},
            {'2', "ABC"},
            {'3', "DEF"},
            {'4', "GHI"},
            {'5', "JKL"},
            {'6', "MNO"},
            {'7', "PQRS"},
            {'8', "TUV"},
            {'9', "WXYZ"},
            {'0', " "}
        };
        static void Main(string[] args){ 
            Console.WriteLine("Old Phone Pad Simulator");
            Console.WriteLine("Press number keys to  type. Press '#' to  send, '*' to backspace");
            Console.WriteLine();   
            
            string output = "";
            char currentKey = '\0';
            int pressCount = 0;
            bool isRunning = true;
            DateTime lastKeyPressTime = DateTime.MinValue;

            while(isRunning){
                if(Console.KeyAvailable){
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    char ch = keyInfo.KeyChar;

                    // send with #
                    if (ch == '#'){
                        if(currentKey != '\0' && pressCount > 0 ){
                            string letters = keyMap[currentKey];
                            if (letters.Length > 0){
                                int index = (pressCount - 1) % letters.Length;
                                output += letters[index];
                            }
                            currentKey = '\0';
                            pressCount = 0;
                        }                        
                        isRunning = false;
                    } else if(ch == '*'){
                        if (pressCount > 0){
                            // Discard current typing
                            currentKey = '\0';
                            pressCount =0;
                        } else if (output.Length > 0){
                            // Removing last character from the string
                            output = output.Substring(0, output.Length - 1);
                        }                        
                    }else if(char.IsDigit(ch)){
                        if(currentKey == ch){
                            pressCount++;
                        } else{                            
                            if(currentKey != '\0' && pressCount > 0){
                                string letter = keyMap[currentKey];
                                if(letters.Length > 0 ){
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

                } else{
                    // Adding 1 second pause logic
                    if(currentKey != '\0' && pressCount > 0){
                        TimeSpan timeSinceLastKeyPress = DateTime.Now - lastKeyPressTime;
                        if(timeSinceLastKeyPress.TotalSeconds >= 1){
                            string letters = keyMap[currentKey];
                            if (letters.Length > 0){
                                int index = (pressCount - 1) % letters.Length;
                                output += letters[index];
                            }
                            currentKey = '\0';
                            pressCount = 0;
                            UpdateDisplay(output, currentKey, pressCount);
                        }
                    }
                }
            }

            // - 
        }

        static void UpdateDisplay(string output, char currentKey, int pressCount){
            string display = output;

            if(currentKey != '\0' && pressCount > 0){
                string letters = keyMap[currentKey];
                if(letters.Length > 0){
                    int index = (pressCount - 1) % letters.Length;
                    display += letters[index];
                }
            }
            Console.WriteLine("Result: " + display);
        }
    }
}
