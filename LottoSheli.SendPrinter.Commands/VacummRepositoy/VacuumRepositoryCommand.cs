using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Core;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace LottoSheli.SendPrinter.Commands
{

    /// <summary>
    /// Vacuums current repository
    /// </summary>
    [Command(Basic = typeof(IVacuumRepositoryCommand))]
    public class VacuumRepositoryCommand : IVacuumRepositoryCommand
    {
        private readonly ILogger<VacuumRepositoryCommand> _logger;
        private readonly ICommonRepository _commonRepository;

        public VacuumRepositoryCommand(ILogger<VacuumRepositoryCommand> logger, ICommonRepository commonRepository)
        {
            _logger = logger;
            _commonRepository = commonRepository;
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            _commonRepository.VacuumStorage();          
        }
    }
}
