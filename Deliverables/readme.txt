Old Phone Pad Simulator
Description
This C# console application simulates the text input method of old mobile phones with numeric keypads. Users can input text by pressing number keys multiple times to cycle through letters, mimicking the T9 input system.
Features

Simulates old phone keypad text input
Supports all letters (A-Z), space, and special characters (&'()
Backspace functionality
Auto-completion after 1 second of inactivity on a key
Send message functionality
Exit application option

How to Use

Run the application.
Press number keys to type letters:

Press once for the first letter, twice for the second, etc.
Example: Press '2' once for 'A', twice for 'B', three times for 'C'


Press '0' for space.
Press '*' to backspace.
Press '#' to send the current message.
Press '0' four times in a row to exit the application.

Key Mappings

1: &'(
2: ABC
3: DEF
4: GHI
5: JKL
6: MNO
7: PQRS
8: TUV
9: WXYZ
0: Space

Technical Details

Language: C#
Application Type: Console Application
.NET Version: 8.0.401

Installation

Ensure you have the .NET SDK installed on your machine.
Clone this repository or download the source code.
Navigate to the project directory in your terminal.
Run dotnet build to compile the application.
Run dotnet run to start the application.

Future Improvements

Add support for numbers and more special characters
Implement a dictionary for word suggestions
Create a graphical user interface (GUI) version

License
MIT License - 2024
Author
Edgar Leiva