﻿using Ncqrs.Commanding.ServiceModel;

namespace Restbucks
{
    public interface ICommandMapping
    {

        void MapCommands(CommandService commandService);

    }
}