using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;

namespace Restbucks.Menu
{

    public class MenuItemMapping : ICommandMapping
    {

        public void MapCommands(CommandService commandService)
        {

            Map.Command<AddMenuItem>()
                .ToAggregateRoot<MenuItem>()
                .CreateNew(cmd => new MenuItem(cmd.MenuItemId, cmd.Name, cmd.Price))
                .RegisterWith(commandService);

            Map.Command<AddCustomization>()
                .ToAggregateRoot<MenuItem>()
                .WithId(cmd => cmd.MenuItemId)
                .ToCallOn((cmd, menuItem) => menuItem.AddCustomization(cmd.Customization, cmd.Options))
                .RegisterWith(commandService);


        }
    }

}
