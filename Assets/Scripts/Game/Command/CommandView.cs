using System;
using UnityEngine;
using Vocore;

public class CommandView
{
    [RegisterCommand(Name = "dialog", Help = "Pop a dialog", MaxArgCount = 1)]
    static void CommandPopDialog(CommandArg[] args)
    {
        if (args.Length <= 0)
        {
            Debug.Log("只接收一个参数");
            return;
        }

        WindowDialog.PopDialog(args[0].String);
    }

    [RegisterCommand(Name = "tip", Help = "Pop a float tip", MaxArgCount = 1)]
    static void CommandFloatTip(CommandArg[] args)
    {
        if (args.Length <= 0)
        {
            Debug.Log("只接收一个参数");
            return;
        }

        FloatTip.Pop(args[0].String);
        Terminal.Close();
    }

    [RegisterCommand(Name = "endgame", Help = "Pop end game menu", MaxArgCount = 0)]
    static void CommandEnd(CommandArg[] args)
    {
        WindowEndGame.Pop();
    }
}
