using System;
using System.Collections.Generic;
using System.Threading;

namespace OldPhonePadApp{
    class Program{ 
        // Mapping the numebers and their characters
       static Dictionary<char, string> keyMap = new Dictionary<char, string>(){
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
            
            // Instructions
            Console.Clear();
            Console.WriteLine("Old Phone Pad Simulator");
            Console.WriteLine("Press number keys to  type. Press '#' to  send, '*' to backspace");
            Console.WriteLine("Press 0 four times to exit.");

            // Variables used
            string output = "";
            char currentKey = '\0';
            int pressCount = 0;
            bool isRunning = true;
            DateTime lastKeyPressTime = DateTime.MinValue;
            string exitSequence = ""; // Added to exit the program while typing 

            while(isRunning){
                if(Console.KeyAvailable){
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    char ch = keyInfo.KeyChar;

                    // Logic for the exitSequence "0000"
                    if(ch == '0'){
                        exitSequence += '0';
                        if(exitSequence.Length >= 4){
                            isRunning = false;
                            break;
                        }
                    } else{
                        exitSequence = ""; // Resetting the exit sequence in case of mistake
                    }

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
                        
                        // Showing final text
                        Console.WriteLine("Final text: " + output);  
                        
                        // Waiting for user to continue (restart typing)
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                        
                        // Reseting for new text
                        output = "";
                        currentKey = '\0';
                        pressCount = 0;
                        exitSequence = "";
                        lastKeyPressTime = DateTime.MinValue;
    
                        // Update the display for the new input
                        UpdateDisplay(output, currentKey, pressCount);

                    }else if(ch == '*'){
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
                                string letters = keyMap[currentKey];

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
                Thread.Sleep(50);
            }
            Console.WriteLine();
            Console.WriteLine("Shutting down...");
        }

        static void UpdateDisplay(string output, char currentKey, int pressCount){
            
            
            // Some instructions to guide the user
            Console.Clear();
            Console.WriteLine("Old Phone Pad Simulator");
            Console.WriteLine("Press number keys to  type. Press '#' to  send, '*' to backspace");
            Console.WriteLine("Press 0 four times to exit.");
            
            string display = output;

            if(currentKey != '\0' && pressCount > 0){
                string letters = keyMap[currentKey];
                if(letters.Length > 0){
                    int index = (pressCount - 1) % letters.Length;
                    display += letters[index];
                }
            }
            Console.WriteLine("Current text: " + display);
            
            
        }
    }
}
