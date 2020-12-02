using BancoXYZ.Business.Models;
using System;

namespace BancoXYZ.Business.Commands
{
    public static class CommandExecutor
    {
        public static Response ExecuteCommand(ICommand command)
        {
            switch (command.Request.Method)
            {
                case Types.MethodType.Get:
                    return command.Get();
                case Types.MethodType.Post:
                    return command.Post();
                case Types.MethodType.Patch:
                    return command.Patch();
                case Types.MethodType.Put:
                    return command.Put();
                case Types.MethodType.Delete:
                    return command.Delete();
                case Types.MethodType.Head:
                    return command.Head();
                default:
                    throw new NotImplementedException($"Method {command.Request.Method} is not supported!!");
            }
        }
    }
}
