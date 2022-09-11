// !!! No using other than System are authorized !!!

using System;
using System.Collections.Generic;

namespace Case1_HangedMan.Game
{
    public static class Game
    {
        // This is a private global variable accessible from everywhere within this file
        // You can give as an argument of Loader.GetWord() the path to the word_bank.txt
        // By default if no argument is given the path will be at the root of the TP1 folder
        // If you want to have a go at reading from another word bank file, give as an 
        // argument the path of that file. (ex: to use the word bank with the ACDC's logins,
        // call the function Loader.GetWord with the argument "../../../Game/acdc_logins.txt")
        private static readonly string WordToGuess = Loader.GetWord();
        
        /*
         * TODO implement this function
         * This function gets the user input from the console and return a char
         * The input must be a single letter (upper or lowercase)
         * The output must be a single lowercase letter (you can use Char.IsLetter() or your own functions)
         * In the case of an error, you must print an error message on the stderr and return 0 using an explicit cast
         */
        public static char GetInput()
        {
            string input = Console.ReadLine();
            char c = Char.Parse(input);
            
            if (input == null || input.Length > 1 || !char.IsLetter(c)) {
                Console.Error.WriteLine("Invalid Argument !");
                return '0';
            }

            if (char.IsUpper(c))
                c = Char.ToLower(c);
            
            return c;
        }

        /*
         * TODO implement this function
         * This function clears the console and prints the 2 arguments
         * You should loop through the array guessedWord and print each char.
         */
        public static void DisplayWord(char[] guessedWord, string usedLetters)
        {
            Console.Clear();
            Console.Write("Your guess: " );

            for (int i = 0; i < guessedWord.Length; i++)
                Console.Write(guessedWord[i]);
            
            Console.WriteLine(", Used letters: " + usedLetters);
        }

        /*
         * TODO implement this function
         * This function displays the hangman according to the gameState and the current errorCount (usedLetters.Length)
         * The hangman ascii is found in Loader.Ascii, it is an array with the different states to display
         *      case gameState Loader.GameState.RUNNING: print the hangman according to the number of wrong letters
         *      case gameState Loader.GameState.LOST: print the the hangman at index Loader.DEFEAT in Loader.Ascii
         *      case gameState Loader.GameState.WON: print the the hangman at index Loader.VICTORY in Loader.Ascii
         */
        public static void DisplayHangman(string usedLetters, Loader.GameState gameState)
        {
            if ( gameState == Loader.GameState.RUNNING && usedLetters.Length == 0)
                Console.WriteLine(Loader.Ascii[0]);
            else
            {
                switch (gameState)
                {
                    case Loader.GameState.WON:
                        Console.WriteLine(Loader.Ascii[Loader.Victory]);
                        break;
                    case Loader.GameState.LOST:
                        Console.WriteLine(Loader.Ascii[Loader.Defeat]);
                        break;
                    default:
                        Console.WriteLine(Loader.Ascii[usedLetters.Length]);
                        break;
                }
            }
            
        }

        /*
         * TODO implement this function
         * This function returns a bool whether the letter is contained in guessedWord
         */
        public static bool ContainsLetter(char[] guessedWord, char letter)
        {
            int i = 0;
            int length = guessedWord.Length;
            
            while (i < length || guessedWord[i] != letter)
                i += 1;

            return i != length;
        }
        
        /*
         * TODO implement this function
         * This function returns the string of usedLetters updated
         * If the letter was already guess or if the letter was already used, print an error on stderr and return
         * usedLetters unchanged
         * If the letter is in the the WordToGuess, update the guessedWord array and leaves usedLetters unchanged
         * else add it to the usedLetters string
         * To check if the letter is contained in usedLetter you can use String.Contains() function 
         * To check if the letter is contained in guessedWord you should use ContainLetter
         * (you may also use 'new string()' but ContainsLetter will be expected and tested)
         * At the end call DisplayWord and DisplayHangman and return usedLetters
         */
        public static string ValidateLetter(char[] guessedWord, string usedLetters, char letter,
            Loader.GameState gameState)
        {
            if (gameState == Loader.GameState.RUNNING)
            {

                for (int i = 0; i < WordToGuess.Length; i++)
                {
                    if (usedLetters.Contains(letter))
                    {
                        if (letter == WordToGuess[i]) {
                            Console.WriteLine("This letter has already been guessed !");
                        }
                        else {
                            Console.WriteLine("You already used that letter...");
                        }
                    }
                    else
                    {
                        if (letter == WordToGuess[i]) {
                            guessedWord[i] = letter;
                        }
                        else {
                            usedLetters += letter;
                        }
                    }
                }
            }
            
            DisplayWord(guessedWord, usedLetters);
            DisplayHangman(usedLetters, gameState);
            return usedLetters;
        }

        
        public static string _ValidateLetter(char[] guessedWord, string usedLetters, char letter,
            Loader.GameState gameState)
        {
            if (gameState == Loader.GameState.RUNNING)
            {
                List<int> indexes = new List<int>();
                
                for (int i = 0; i < WordToGuess.Length; i++)
                {
                    if (WordToGuess[i] == letter) {
                        indexes.Add(i);
                    }
                }

                if (usedLetters.Contains(letter))
                {
                    if (indexes.Count != 0) {
                        Console.WriteLine("This letter has already been guessed !");
                    }
                    else {
                        Console.WriteLine("You already used that letter...");
                    }
                }
                
                else
                {
                    if (indexes.Count != 0) {
                        foreach (int index in indexes) {
                            guessedWord[index] = letter;
                        }
                    }
                    else {
                        usedLetters += letter;
                    }
                }
            }
            
            DisplayWord(guessedWord, usedLetters);
            DisplayHangman(usedLetters, gameState);
            return usedLetters;
        }

        /*
         * TODO implement this function
         * This function returns the state of the game
         * If the number of letters in usedLetters is greater or equal to the number of attempts then the user lost
         * If the WordToGuess and guessedWord are equal then the user won
         * Otherwise, keep running the game
         */
        public static Loader.GameState GameStatus(char[] guessedWord, string usedLetters, int attempts)
        {
            if (usedLetters.Length >= attempts) 
                return Loader.GameState.LOST;

            int i = 0;
            while (i < guessedWord.Length && guessedWord[i] == WordToGuess[i])
                i += 1;

            if (i == guessedWord.Length)
                return Loader.GameState.WON;
            return Loader.GameState.RUNNING;
        }

        /*
         * TODO implement this function
         * This function calls DisplayWord, DisplayHangman and displays a victory or defeat message
         * In case of a defeat, print the WordToGuess
         */
        public static void EndScreen(char[] guessedWord, string usedLetters, Loader.GameState gameState)
        {
            DisplayWord(guessedWord, usedLetters);
            DisplayHangman(usedLetters, gameState);
            
            if (gameState == Loader.GameState.WON)
                Console.WriteLine("You won !");
                
            if ( gameState == Loader.GameState.LOST)
                Console.WriteLine("You lost :( The answer was " + WordToGuess );

        }
        
        /*
         * TODO implement this function
         * This function must initialise the variables used in the game:
         *      The gameState must be initialized to Loader.GameState.RUNNING
         *      The number of attempts is defined in Loader.Attempts
         *      The errorCount (usedLetters) must be initialized to an empty string
         *      Moreover, guessedWord can be initialised using Loader.GetEmptyDuplicate(WordToGuess);
         * The game must run as follows:
         *      Print the word and the hangman. While the game has to run, ask for an input.
         *      If the output is invalid, loop again else and update the variables.
         *      Upon exiting the loop, print the correct end message according to the gameState
         */
        public static void LaunchGame()
        {
            Loader.GameState gameState = Loader.GameState.RUNNING;
            int attempts = Loader.Attempts;
            string usedLetters = "";
            char[] guessedWord = Loader.GetEmptyDuplicate(WordToGuess);
            
            
            while (gameState == Loader.GameState.RUNNING)
            {
                DisplayWord(guessedWord, usedLetters);
                DisplayHangman(usedLetters, gameState);
                
                string Letter = GetInput().ToString();
                char letter = Char.Parse(Letter);
                while (Letter == null || Letter.Length > 1 || !char.IsLetter(letter))
                    Letter = GetInput().ToString();

                usedLetters = _ValidateLetter(guessedWord, usedLetters, letter, gameState);
                gameState = GameStatus(guessedWord, usedLetters, attempts);
                
            }
            
            EndScreen(guessedWord, usedLetters, gameState);
            
        }
    }
}