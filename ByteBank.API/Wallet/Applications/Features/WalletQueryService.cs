﻿using AutoMapper;
using ByteBank.API.Shared.Application.Exceptions;
using ByteBank.API.Wallet.Domain.Models.Aggregates;
using ByteBank.API.Wallet.Domain.Models.Queries;
using ByteBank.API.Wallet.Domain.Models.Responses;
using ByteBank.API.Wallet.Domain.Repository;
using ByteBank.API.Wallet.Domain.Service;

namespace ByteBank.API.Wallet.Applications.Features;

public class WalletQueryService : IWalletQueryService
{
    private readonly IWalletRepository _walletRepository;
    private readonly IMapper _mapper; 

    public WalletQueryService(IWalletRepository walletRepository, IMapper mapper)
    {
        _walletRepository = walletRepository;
        _mapper = mapper;
    }
    
    public async Task<WalletResponse> Handle(GetWalletByIdQuery query)
    {
        var wallet = await _walletRepository.FindByIdAsync(query.Id);
        if (wallet == null)
        {
            throw new NotFoundEntityIdException(nameof(Wallet), query.Id);
        }
        var walletResponse = _mapper.Map<WalletResponse>(wallet);
        return walletResponse;
    }
}