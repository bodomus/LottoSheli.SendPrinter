using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Core;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace LottoSheli.SendPrinter.Commands
{

    /// <summary>
    /// Updates current repository
    /// </summary>
    [Command(Basic = typeof(IUpdateRepositoryCommand))]
    public class UpdateRepositoryCommand : IUpdateRepositoryCommand
    {
        private readonly ILogger<UpdateRepositoryCommand> _logger;
        private readonly ITicketTaskRepository _ticketTaskRepository;
        private readonly IConstantRepository _constantRepository;
        private readonly IHashGenerator _hashGenerator;

        public const string DBVersion = "DBVersion";

        public UpdateRepositoryCommand(ITicketTaskRepository ticketTaskRepository, IConstantRepository constantRepository, ILogger<UpdateRepositoryCommand> logger, IHashGenerator hashGenerator)
        {
            _logger = logger;
            _ticketTaskRepository = ticketTaskRepository;
            _constantRepository = constantRepository;
            _hashGenerator = hashGenerator;
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute(UpdateRepositoryCommandData data)
        {
            var constant = _constantRepository.GetByName(DBVersion);

            if (constant == null)
            {
                constant = _constantRepository.CreateNew();

                constant.Id = 1;
                constant.ConstantName = DBVersion;
                constant.ConstantValue = "0";
                constant.CreatedDate = DateTime.Now;

                constant = _constantRepository.Insert(constant);
            }

            var updateVersion = int.Parse(constant.ConstantValue);

            for (int i = updateVersion + 1; i <= data.Version; i++)
            {
                switch (i)
                {
                    case 1: UpdateToV1(); break;
                    default: throw new NotImplementedException();
                }

                constant.ConstantValue = i.ToString();

                _constantRepository.Update(constant);

                _logger.LogInformation($"Database succesfully updated to version: {i}");

            }            
        }

        private void UpdateToV1()
        {
            var ticketTasks = _ticketTaskRepository.GetAll();

            foreach (var ticketTask in ticketTasks)
            {
                var tablesHashWithUser = $"{ticketTask.UserId}:{_hashGenerator.ComputeHash(string.Join(string.Empty, ticketTask.Tables.Select(obj => obj.ToString())))}";
                ticketTask.TablesHash = tablesHashWithUser;

                _ticketTaskRepository.Update(ticketTask);
            }
        }
    }
}
