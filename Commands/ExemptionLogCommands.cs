﻿using SBCore.Configurator.EditWindows;
using System.Windows.Input;

namespace SBCore.Configurator.Commands
{
    public static class ExemptionLogCommands
    {
        public static RoutedCommand AddRowCommand { get; } = new RoutedCommand("AddRow", typeof(ExemptionLogEditWindow));
        public static RoutedCommand RemoveRowCommand { get; } = new RoutedCommand("RemoveRow", typeof(ExemptionLogEditWindow));
    }
}
