
using UnityEngine;

public class Hacker : MonoBehaviour
{
    string menuHint = "You may type menu at any time";
    string[] level1Passwords = {"books", "aisle", "shelf", "password", "font", "borrow" };
    string[] level2Passwords = {"prisoner", "handcuffs", "holster", "uniform", "arrest"};
    string[] level3Passwords = {"starfield", "telescope", "environment", "exploration", "astronauts"};
    

    //Game State
    int level;

    enum Screen { MainMenu, Password, Win};
    Screen currentScreen;

    string password;

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu ();
    }

    void ShowMainMenu () 
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("Press 1 for the local library");
        Terminal.WriteLine("Press 2 for the police station");
        Terminal.WriteLine("Press 3 for NASA!");
        Terminal.WriteLine("Enter your selection:");
    }
    void OnUserInput(string input)
	{
		if (input == "menu") //We can go to menu
        {
            ShowMainMenu();
        }
        else if (input == "quit" || input == "close" || input == "exit") 
        {
            Terminal.WriteLine("If on the web close on the tab");
            Application.Quit();
        }
        else if (currentScreen == Screen.MainMenu) 
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password) 
        {
            CheckPassword(input);
        }
    }

    void RunMainMenu(string input) 
    {
        bool IsValidLevelNumber = input =="1" || input == "2" || input == "3";
        if (IsValidLevelNumber) 
        {
            level = int.Parse(input); 
            StartGame();
        }
        else if (input == "007") // easter egg
        {
            Terminal.WriteLine("secret...");
        }else                     
        {
            Terminal.WriteLine("Please choose a valid level");
            Terminal.WriteLine(menuHint);
        }  
    }



    void StartGame() 
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();        
        switch(level) 
        {
            case 1:
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                password = level3Passwords[Random.Range(0, level3Passwords.Length)];
                break;
            default: 
                Debug.LogError("Invalid level number");
                break;
        }
        Terminal.WriteLine("Enter your password, hint: " + password.Anagram());
    }
    void CheckPassword(string input) 
    {
        if (input == password) 
        {
            DisplayWinScreen();
        }else 
        {
            StartGame();
        }
    }

    void  DisplayWinScreen() 
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }

    void ShowLevelReward() 
    {
        switch(level) 
        {
            case 1:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
    _______
   /      // 
  /      //
 /_____ //
(______(/               
"               
                );
                break;
            case 2:
                Terminal.WriteLine("You got the prision key!");
                Terminal.WriteLine("Play again for greater challenge");
                Terminal.WriteLine(@"
 __
/0 \_______
\__/-=' = '         
"
                );
                break;
            case 3:
                Terminal.WriteLine(@"
 _ __   __ _ ___  __ _
| '_ \ / _` / __|/ _` |
| | | | (_| \__ \ (_| |
|_| |_|\__,_|___)\__,_|
"
                );
                Terminal.WriteLine("Welcome to NASA's internal system!");
                break;
            Debug.LogError("Invalid level reached");
                break;
        }
    }
}
