using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Print.Base;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Returns <see cref="PrinterSettingsResult"/> by specified <see cref="GetPrinterSettingsForTicketTaskCommandData"/>
    /// </summary>
    [Command(Basic = typeof(IGetPrinterSettingsForTicketTaskCommand))]
    public class GetPrinterSettingsForTicketTaskCommand : IGetPrinterSettingsForTicketTaskCommand
    {
        private readonly ILogger<GetPrinterSettingsForTicketTaskCommand> _logger;
        private readonly ICreateLottoTicketImageCommand<CreatedLottoRegularAndDoubleImageResult> _createLottoRegularAndDoubleImageCommand;
        private readonly ICreateLottoTicketImageCommand<CreatedLottoDoubleMethodicalTicketImageResult> _createLottoDoubleMethodicalTicketImageCommand;
        private readonly ICreateLottoTicketImageCommand<CreatedLottoMethodicalTicketImageResult> _createLottoMethodicalTicketImageCommand;
        private readonly ICreateLottoTicketImageCommand<CreatedLottoDoubleMethodicalStrongTicketImageResult> _createLottoDoubleMethodicalSrongTicketImageCommand;
        private readonly ICreateLottoTicketImageCommand<CreatedLottoMethodicalStrongTicketImageResult> _createLottoMethodicalStrongTicketImageCommand;
        private readonly ICreateLottoTicketImageCommand<CreatedTripleSevenTicketImageResult> _createTripleSevenTicketImageCommand;
        private readonly ICreateLottoTicketImageCommand<CreatedTripleSevenMethodicalTicketImageResult> _createTripleSevenMethodcalTicketImageCommand;
        private readonly ICreateLottoTicketImageCommand<CreatedChanceTicketImageResult> _createChanceTicketImageCommand;
        private readonly ICreateLottoTicketImageCommand<CreatedMultipleChanceTicketImageResult> _createMultipleChanceTicketImageCommand;
        private readonly ICreateLottoTicketImageCommand<CreatedMethodicalChanceTicketImageResult> _createMethodicalChanceTicketImageCommand;
        private readonly ICreateLottoTicketImageCommand<Created123TicketImageResult> _create123TicketImageCommand;

        public GetPrinterSettingsForTicketTaskCommand(ILogger<GetPrinterSettingsForTicketTaskCommand> logger, 
            ICreateLottoTicketImageCommand<CreatedLottoRegularAndDoubleImageResult> createLottoRegularAndDoubleImageCommand,
            ICreateLottoTicketImageCommand<CreatedLottoDoubleMethodicalTicketImageResult> createLottoDoubleMethodicalTicketImageCommand,
            ICreateLottoTicketImageCommand<CreatedLottoMethodicalTicketImageResult> createLottoMethodicalTicketImageCommand,
            ICreateLottoTicketImageCommand<CreatedLottoDoubleMethodicalStrongTicketImageResult> createLottoDoubleMethodicalSrongTicketImageCommand,
            ICreateLottoTicketImageCommand<CreatedLottoMethodicalStrongTicketImageResult> createLottoMethodicalStrongTicketImageCommand,
            ICreateLottoTicketImageCommand<CreatedTripleSevenTicketImageResult> createTripleSevenTicketImageCommand,
            ICreateLottoTicketImageCommand<CreatedTripleSevenMethodicalTicketImageResult> createTripleSevenMethodcalTicketImageCommand,
            ICreateLottoTicketImageCommand<CreatedChanceTicketImageResult> createChanceTicketImageCommand,
            ICreateLottoTicketImageCommand<CreatedMultipleChanceTicketImageResult> createMultipleChanceTicketImageCommand,
            ICreateLottoTicketImageCommand<CreatedMethodicalChanceTicketImageResult> createMethodicalChanceTicketImageCommand,
            ICreateLottoTicketImageCommand<Created123TicketImageResult> create123TicketImageCommand)
        {
            _logger = logger;
            _createLottoRegularAndDoubleImageCommand = createLottoRegularAndDoubleImageCommand;
            _createLottoDoubleMethodicalTicketImageCommand = createLottoDoubleMethodicalTicketImageCommand;
            _createLottoMethodicalTicketImageCommand = createLottoMethodicalTicketImageCommand;
            _createLottoDoubleMethodicalSrongTicketImageCommand = createLottoDoubleMethodicalSrongTicketImageCommand;
            _createLottoMethodicalStrongTicketImageCommand = createLottoMethodicalStrongTicketImageCommand;
            _createTripleSevenTicketImageCommand = createTripleSevenTicketImageCommand;
            _createTripleSevenMethodcalTicketImageCommand = createTripleSevenMethodcalTicketImageCommand;
            _createChanceTicketImageCommand = createChanceTicketImageCommand;
            _createMultipleChanceTicketImageCommand = createMultipleChanceTicketImageCommand;
            _createMethodicalChanceTicketImageCommand = createMethodicalChanceTicketImageCommand;
            _create123TicketImageCommand = create123TicketImageCommand;
    }

        public bool CanExecute()
        {
            return true;
        }

        /// <summary>
        /// factory for creating task instances
        /// taking from server response game type and subtype
        /// creates appropriate task instance
        /// </summary>
        /// <param name="taskData">json server reposnse with task data</param>
        /// <returns>task instance</returns>
        [Obsolete("This method should be refactored later.")]
        private PrinterSettingsResult ParseTask(TicketTask taskData)
        {

            var gameType = taskData.Type;
            var subType = taskData.SubType;


            var commandData = new CreateLottoTicketImageCommandData { ExisintgTicketTask = taskData };

            if (gameType ==  TaskType.LottoRegular || gameType == TaskType.LottoDouble || gameType == TaskType.LottoSocial)
            {
                
                if (subType == TaskSubType.LottoMethodical)
                {
                    return new PrinterSettingsResult
                    {
                        PrinterSettingsProfile = "LottoMethodical",
                        DescriptionForPrint = $"TID: {taskData.TicketId}, UID: {taskData.UserId}",
                        PrintedImage = _createLottoMethodicalTicketImageCommand.Execute(commandData).Image
                    };
                }
                else if (subType == TaskSubType.LottoDoubleMethodical)
                {
                    return new PrinterSettingsResult
                    {
                        PrinterSettingsProfile = "LottoDoubleMethodical",
                        DescriptionForPrint = $"TID: {taskData.TicketId}, UID: {taskData.UserId}",
                        PrintedImage = _createLottoDoubleMethodicalTicketImageCommand.Execute(commandData).Image
                    };
                }
                else if (subType == TaskSubType.LottoStrongMethodical)
                {
                    return new PrinterSettingsResult
                    {
                        PrinterSettingsProfile = "LottoStrongMethodical",
                        DescriptionForPrint = $"TID: {taskData.TicketId}, UID: {taskData.UserId}",
                        PrintedImage = _createLottoMethodicalStrongTicketImageCommand.Execute(commandData).Image
                    };
                }
                else if (subType == TaskSubType.LottoDoubleStrongMethodical)
                {
                    return new PrinterSettingsResult
                    {
                        PrinterSettingsProfile = "LottoDoubleStrongMethodical",
                        DescriptionForPrint = $"TID: {taskData.TicketId}, UID: {taskData.UserId}",
                        PrintedImage = _createLottoDoubleMethodicalSrongTicketImageCommand.Execute(commandData).Image
                    };
                }
                else
                {
                    return new PrinterSettingsResult { 
                        PrinterSettingsProfile = subType == TaskSubType.LottoRegular || subType == TaskSubType.LottoSocial ? "NormalLotto" : "DoubleLotto", 
                        DescriptionForPrint = $"TID: {taskData.TicketId}, UID: {taskData.UserId} {(taskData.Extra == true ? "X" : string.Empty)}", 
                        PrintedImage = _createLottoRegularAndDoubleImageCommand.Execute(commandData).Image
                    };
                }
            }
            else if (gameType == TaskType.TripleSeven)
            {
                if (subType == TaskSubType.TripleSeven)
                {

                    return new PrinterSettingsResult
                    {
                        PrinterSettingsProfile = "777",
                        DescriptionForPrint = $"TID: {taskData.TicketId}, UID: {taskData.UserId}",
                        PrintedImage = _createTripleSevenTicketImageCommand.Execute(commandData).Image
                    };
                }
                else if (subType == TaskSubType.TripleSevenMethodical)
                {
                    return new PrinterSettingsResult
                    {
                        PrinterSettingsProfile = "777Methodical",
                        DescriptionForPrint = $"TID: {taskData.TicketId}, UID: {taskData.UserId}",
                        PrintedImage = _createTripleSevenMethodcalTicketImageCommand.Execute(commandData).Image
                    };
                }
            }
            else if (gameType == TaskType.RegularChance)
            {
                return new PrinterSettingsResult
                {
                    PrinterSettingsProfile = "Chance",
                    DescriptionForPrint = $"TID: {taskData.TicketId}, UID: {taskData.UserId} {(taskData.UserIdMandatoryFlag == 1 ? "X" : string.Empty)}",
                    PrintedImage = _createChanceTicketImageCommand.Execute(commandData).Image
                };
            }
            else if (gameType == TaskType.MultipleChance)
            {
                return new PrinterSettingsResult
                {
                    PrinterSettingsProfile = "MultipleChance",
                    DescriptionForPrint = $"TID: {taskData.TicketId}, UID: {taskData.UserId} {(taskData.UserIdMandatoryFlag == 1 ? "X" : string.Empty)}",
                    PrintedImage = _createMultipleChanceTicketImageCommand.Execute(commandData).Image
                };
            }
            else if (gameType == TaskType.MethodicalChance)
            {
                return new PrinterSettingsResult
                {
                    PrinterSettingsProfile = "MethodicalChance",
                    DescriptionForPrint = $"TID: {taskData.TicketId}, UID: {taskData.UserId} {(taskData.UserIdMandatoryFlag == 1 ? "X" : string.Empty)}",
                    PrintedImage = _createMethodicalChanceTicketImageCommand.Execute(commandData).Image
                };
            }
            else if (gameType == TaskType.Regular123)
            {
                return new PrinterSettingsResult
                {
                    PrinterSettingsProfile = "123",
                    DescriptionForPrint = $"TID: {taskData.TicketId}, UID: {taskData.UserId}",
                    PrintedImage = _create123TicketImageCommand.Execute(commandData).Image
                };
            }
            
            throw new NotSupportedException($"Unknown game type: {gameType}");
        }

        public PrinterSettingsResult Execute(GetPrinterSettingsForTicketTaskCommandData data)
        {
            return new PrinterSettingsResult
            {
                PrinterSettingsProfile = GetPrinterSettingsProfile(data.ExisintgTicketTask),
                DescriptionForPrint = GetDescriptionForPrint(data.ExisintgTicketTask),
                PrintedImage = GetImageForPrint(data.ExisintgTicketTask)
            };
        }

        private string GetPrinterSettingsProfile(TicketTask ticket) 
        { 
            if (ticket is { Type: var type, SubType: var subType }) 
            {
                return type switch
                {
                    TaskType.LottoRegular or TaskType.LottoDouble or TaskType.LottoSocial =>
                        subType switch
                        {
                            TaskSubType.LottoRegular or TaskSubType.LottoSocial => "NormalLotto",
                            TaskSubType.LottoDouble => "DoubleLotto",
                            TaskSubType.LottoMethodical => "LottoMethodical",
                            TaskSubType.LottoDoubleMethodical => "LottoDoubleMethodical",
                            TaskSubType.LottoStrongMethodical => "LottoStrongMethodical",
                            TaskSubType.LottoDoubleStrongMethodical => "LottoDoubleStrongMethodical",
                            _ => "LottoMethodical"
                        },
                    TaskType.TripleSeven =>
                        subType switch
                        {
                            TaskSubType.TripleSevenMethodical => "777Methodical",
                            _ => "777"
                        },
                    TaskType.RegularChance => "Chance",
                    TaskType.MultipleChance => "MultipleChance",
                    TaskType.MethodicalChance => "MethodicalChance",
                    TaskType.Regular123 => "123",
                    _ => throw new NotSupportedException($"Unknown game type: {type} {subType}")
                };
            }
            throw new InvalidOperationException("Ticket missing. Failed to identify printing profile");
        }

        private string GetDescriptionForPrint(TicketTask ticket) 
        {
            if (ticket is { TicketId: var ticketId, UserId: var userId, UserIdMandatoryFlag: var nationalIdRequired, Extra: var extra }) 
            {
                bool printX = (IsLottoTicket(ticket) && (extra ?? false)) || (!IsLottoTicket(ticket) && 1 == nationalIdRequired);
                return $"TID: {ticketId}, UID: {userId} {(printX ? "X" : string.Empty)}";
            }
            throw new InvalidOperationException("Ticket missing. Failed to compile print header");
        }
            

        private Bitmap GetImageForPrint(TicketTask ticket) 
        {
            if (ticket is { Type: var type, SubType: var subType }) 
            {
                var commandData = new CreateLottoTicketImageCommandData { ExisintgTicketTask = ticket };
                return type switch
                {
                    TaskType.LottoRegular or TaskType.LottoDouble or TaskType.LottoSocial =>
                        subType switch
                        {
                            TaskSubType.LottoMethodical => _createLottoMethodicalTicketImageCommand.Execute(commandData).Image,
                            TaskSubType.LottoDoubleMethodical => _createLottoDoubleMethodicalTicketImageCommand.Execute(commandData).Image,
                            TaskSubType.LottoStrongMethodical => _createLottoMethodicalStrongTicketImageCommand.Execute(commandData).Image,
                            TaskSubType.LottoDoubleStrongMethodical => _createLottoDoubleMethodicalSrongTicketImageCommand.Execute(commandData).Image,
                            _ => _createLottoRegularAndDoubleImageCommand.Execute(commandData).Image
                        },
                    TaskType.TripleSeven =>
                        subType switch
                        {
                            TaskSubType.TripleSevenMethodical => _createTripleSevenMethodcalTicketImageCommand.Execute(commandData).Image,
                            _ => _createTripleSevenTicketImageCommand.Execute(commandData).Image
                        },
                    TaskType.RegularChance => _createChanceTicketImageCommand.Execute(commandData).Image,
                    TaskType.MultipleChance => _createMultipleChanceTicketImageCommand.Execute(commandData).Image,
                    TaskType.MethodicalChance => _createMethodicalChanceTicketImageCommand.Execute(commandData).Image,
                    TaskType.Regular123 => _create123TicketImageCommand.Execute(commandData).Image,
                    _ => throw new NotSupportedException($"Unknown game type: {type} {subType}")
                };
            }
            throw new InvalidOperationException("Ticket missing. Failed to create image for printing");
        }

        private bool IsLottoTicket(TicketTask ticket) => null != ticket 
            && (TaskType.LottoRegular == ticket.Type 
                || TaskType.LottoDouble == ticket.Type 
                || TaskType.LottoSocial == ticket.Type);
    }
}
