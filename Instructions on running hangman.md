# Manual Test Instructions – Hangman CLI Game

## Overview
This is a CLI-based Hangman game implemented in C#. It supports single-player and multiplayer modes, a hint system, word suggestion, and a persistent scoring system.

Authors: Daniel Reynaldo, Juwon Lee, Mustafa Alnidawi, Noah Belara Reyes

---

## 0. Running the Test
- Install .Net from Microsoft
- Run the program with `dotnet run -- test`.
  - If you get an error about no project file detected, create a project folder using `dotnet new console -o Hangman`
  - `cd Hangman`
  - `rm Program.cs` so that this file is not defaultly run.
  - Copy and paste all files into the newly created /Hangman directory
  - `dotnet run -- test`
- This command only runs tests and terminates the program, not running the game.

---

## 1. Starting the Game
- Install .Net from Microsoft
- Run the program with `dotnet run`.
  - If you get an error about no project file detected, create a project folder using `dotnet new console -o Hangman`
  - `cd Hangman`
  - `rm Program.cs` so that this file is not defaultly run.
  - Copy and paste all files into the newly created /Hangman directory
  - `dotnet run`
- If `playerScore.txt` exists, it will load your last saved score.
  - If it is your first time playing, the default score is 0.
- You will see a menu with options:
  - `START`: begin a new game
  - `SUGGEST`: suggest new words for future games
  - `EXIT`: quit the program

---

## 2. Game Modes
After choosing `START`, you will be prompted to select a game mode:
- `Easy`, `Intermediate`, `Hard`: random word selected from respective `.txt` file.
- `Multiplayer`: another player inputs a word, and the second player attempts to guess it.

You will also be asked if you want to enable a **time limit** (30 seconds).  
Respond with `y` or `n`.

---

## 3. Playing the Game
- The game shows a blank word (e.g., `____`) and remaining lives.
- Enter one letter at a time to guess the word.
- You may type `hint` once to reveal a random unguessed letter.
- If the word is guessed, score is calculated as:

```
(lives left) * 100 * word length - (200 * number of hints used)
(minimum score: 50)
```

- Score is updated in `playerScore.txt`.

---

## 4. Hint System
- Type `hint` during gameplay to reveal one unguessed letter.
- Using a hint reduces your final score.

---

## 5. Multiplayer Mode
- One player enters a word (input hidden is not implemented).
- Second player guesses letters like in single-player mode.
- All other mechanics (lives, hint, scoring) are the same.

---

## 6. Word Suggestion Feature
- From the main menu, select `SUGGEST`.
- Type `CONTINUE` to suggest words, or `EXIT` to return.
- Words must only contain alphabetic characters (no spaces or symbols).
- Words are saved to:
  - `easy.txt` (≤ 4 letters)
  - `intermediate.txt` (5–8 letters)
  - `hard.txt` (≥ 9 letters)

---

## 7. Input Validation
- All inputs are case-insensitive.
- If input is invalid (e.g., wrong mode, multi-letter guess), a warning will print and re-prompt.
- Previously guessed letters are tracked — repeated guesses are ignored with a message.

---

## 8. Ending the Game
- After finishing a round, you’ll be asked if you want to play again (`y/n`).
- Typing `n` will exit the program.
- Or if you are in main menu, typing `EXIT` will exit the program.
