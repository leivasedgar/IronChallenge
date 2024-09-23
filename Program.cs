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
            
            string output = "";
            char currentKey = '\0';
            int pressCount = 0;
            bool isRunning = true;
            
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
                    }
                }
            }
        }

    public static string OldPhonePad(string input)
    {
        if (string.IsNullOrEmpty(input))
            return "";

        // Mapping of the numbers and their corresponding characters
        Dictionary<char, string> keyMap = new Dictionary<char, string>()
        {
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

        // Variables used
        string output = "";
        char currentKey = '\0';
        int pressCount = 0;

        foreach (char ch in input)
        {
            if (ch == '#')
            {
                // Confirm any pending character
                if (currentKey != '\0' && pressCount > 0)
                {
                    string letters = keyMap[currentKey];
                    if (letters.Length > 0)
                    {
                        int index = (pressCount - 1) % letters.Length; //logic to which letter was selected
                        output += letters[index];
                    }
                    // Reset current key and press count
                    currentKey = '\0';
                    pressCount = 0;
                }
                break;
            }
            else if (ch == '*')
            {
                output = output.Substring(0, output.Length - 1);
            }
            else if (ch == '0')
            {
                // Confirm any pending character
                if (currentKey != '\0' && pressCount > 0)
                {
                    string letters = keyMap[currentKey];
                    if (letters.Length > 0)
                    {
                        int index = (pressCount - 1) % letters.Length; //logic to which letter was selected
                        output += letters[index];
                    }
                    // Reset current key and press count
                    currentKey = '\0';
                    pressCount = 0;
                }
            }
            else if (char.IsDigit(ch))
            {
                if (currentKey == ch)
                {
                    // Same key pressed again
                    pressCount++;
                }
                else
                {
                    // Confirm the previous character
                    if (currentKey != '\0' && pressCount > 0)
                    {
                        string letters = keyMap[currentKey];
                        if (letters.Length > 0)
                        {
                            int index = (pressCount - 1) % letters.Length; //logic to which letter was selected
                            output += letters[index];
                        }
                    }
                    // Start new key press
                    currentKey = ch;
                    pressCount = 1;
                }
            }
        }
        return output;
    }
}
}