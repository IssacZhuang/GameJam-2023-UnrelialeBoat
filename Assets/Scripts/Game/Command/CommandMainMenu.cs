using System;
using UnityEngine;
using Vocore;

public class CommandMainMenu
{
    [RegisterCommand(Name = "main-menu", Help = "Pop main menu")]
    static void CommandPopDialog(CommandArg[] args)
    {
        MainMenu.PopMainMenu();
    }
}
