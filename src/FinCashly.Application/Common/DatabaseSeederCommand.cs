using System.Text.Json;
using AutoMapper;
using FinCashly.API.seed;
using FinCashly.Application.Utils;
using FinCashly.Domain.Common;
using FinCashly.Domain.Common.interfaces;
using FinCashly.Domain.Exceptions;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Transactions.Queries.GetTransactionList;

public class DatabaseSeederCommand : IRequest<bool>
{
    
}
public class DatabaseSeederCommandHandler : IRequestHandler<DatabaseSeederCommand, bool>
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<DatabaseSeederCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public DatabaseSeederCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DatabaseSeederCommandHandler> logger, ICurrentUserService currentUserService)
    {
        _uow = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task<bool> Handle(DatabaseSeederCommand request, CancellationToken cancellationToken)
    {
        var jsonFile = GenericMethodsUtils.ReadFileTypeJson("seed/FinCashlySeed.json");

        if (string.IsNullOrWhiteSpace(jsonFile))
        {
            throw new BusinessException("Arquivo informado não é válido"); 
        }

        var data = JsonSerializer.Deserialize<List<SeedFinCashly>>(jsonFile) ?? new List<SeedFinCashly>();

        return Task.FromResult<bool>(true);
    }
}