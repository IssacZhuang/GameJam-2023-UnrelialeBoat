using System;
using System.Collections.Generic;
using System.Linq;


public class MapComp_InteractiveManger : BaseMapComponent
{
    private const int _tickInterval = 1;
    private int _tick = 0;

    public override void OnTick()
    {
        _tick++;
        if (_tick % _tickInterval == 0)
        {
            CheckCondition();
        }
    }

    public void CheckCondition()
    {
        var entities = Map.Entities;
        bool isAllCondition = true;
        for (int i = 0; i < entities.Count(); i++)
        {
            if (entities.ElementAt(i) is InteractiveObject interactive)
            {
                //todo
            }
        }
    }
}
