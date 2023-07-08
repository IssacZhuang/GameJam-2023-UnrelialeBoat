using System;
using System.Collections.Generic;
using UnityEngine;

using Vocore;

public class CommandAudio
{
    [RegisterCommand(Name = "play-audio", Help = "Play audio", MaxArgCount = 1)]
    static void CommandPlayAudio(CommandArg[] args)
    {
        if (args.Length <= 0)
        {
            Debug.Log("只接收一个参数");
            return;
        }

        Current.AudioManager.PlayAsync(args[0].String);
    }
}
