using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Remote;
using LottoSheli.SendPrinter.Repository;
using LottoSheli.SendPrinter.Repository.Converters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Draws
{

    /// <summary>
    /// Receives <see cref="Draw"/> by specified <see cref="ReceiveDrawsCommandData"/>
    /// </summary>
    [Command(Basic = typeof(IReceiveDrawsCommand))]
    public class ReceiveDrawsCommand : IReceiveDrawsCommand
    {
        private readonly ILogger<ReceiveDrawsCommand> _logger;
        private readonly IRemoteClient _remoteClient;
        private readonly IDrawConverter _drawConverter;
        private readonly IDrawRepository _drawRepository;

        public ReceiveDrawsCommand(ILogger<ReceiveDrawsCommand> logger, IRemoteClient remoteClient, 
            IDrawConverter drawConverter, IDrawRepository drawRepository)
        {
            _logger = logger;
            _remoteClient = remoteClient;
            _drawConverter = drawConverter;
            _drawRepository = drawRepository;
        }

        public bool CanExecute()
        {
            return true;
        }

        public async Task<ReceiveDrawsResult> Execute()
        {
            try
            {
                string json = await _remoteClient.GetDrawsAsync();
                if (string.IsNullOrEmpty(json))
                    throw new InvalidOperationException($"Failed to fetch draws from server");
                IList<Draw> draws = _drawConverter.Convert(json);
                foreach (var draw in draws)
                    _drawRepository.UpsertDraw(draw);
                return new ReceiveDrawsResult { Success = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"DRAWS {ex.Message}");
                return new ReceiveDrawsResult { Success = false };
            }

        }
    }
}
