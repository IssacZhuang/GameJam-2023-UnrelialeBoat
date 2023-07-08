using System;
using UnityEngine;
using Vocore;

public class CommandDialog
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
}
