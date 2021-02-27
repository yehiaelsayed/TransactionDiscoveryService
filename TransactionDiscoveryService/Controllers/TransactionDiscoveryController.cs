using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TransactionDiscovery.API.Models.Requests;
using TransactionDiscovery.API.Models.Responses;
using TransactionDiscovery.Data.Context;
using TransactionDiscovery.Data.Models;
using TransactionDiscovery.Data.Models.Enums;
using TransactionDiscovery.Data.Repository;
using TransactionDiscovery.Services;
using TransactonDiscovery.Utils;

namespace TransactionDiscovery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionDiscoveryController : ControllerBase
    {

        private ITransactionDiscoveryService _transactionService { get; set; }
        public TransactionDiscoveryController(ITransactionDiscoveryService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet, Route("Health")]
        public ActionResult Health()
        {
            return Ok("Service is up and running");
        }

        [HttpPost, Route("AddAccounts")]
        public async Task<ActionResult> AddAccounts(AddAccounts accounts)
        {
            var response = new GenericResponse<List<DiscoveryPatchResponse>>();
            try
            {

                var addedAccounts = _transactionService.AddAccounts(accounts.AccountsIds);
                if (addedAccounts.IsSuccess)
                {
                    var discoveryResult = await _transactionService.DiscoverPayments(addedAccounts.Data);

                    if (discoveryResult.IsSuccess)
                    {
                        var responseData = new List<DiscoveryPatchResponse>();
                        discoveryResult.Data.ForEach(d =>
                        {
                            responseData.Add(new DiscoveryPatchResponse()
                            {
                                AccountId = d.Account.PublicKey,
                                IsSuccess = d.Status == PatchStatus.Success,
                                PulledRecordsCount = d.PulledRecordsCount
                            });
                        });
                        response.Success(responseData);
                    }
                    else
                    {
                        response.Failed("Failed to discover payments");
                    }
                }
                else
                {
                    response.Failed("Failed to Add Account to Database Please check Logs");
                }

            }
            catch (Exception ex)
            {
                response.Failed("Failed to Add Account to Database or discover paymets  Please check Logs");

            }
            return Ok(response);
        }

        [HttpGet, Route("History")]

        public ActionResult GetAccountTransactionHistory(string accountId)
        {
            var response = new GenericResponse<string>();
            try
            {
                var result = _transactionService.GetAccountTransactionHistory(accountId);

                if (result.IsSuccess)
                {
                    var stream = result.Data.ToCSV(new List<string>() { "Wallet", "Amount" }).ToStream();
                    return File(stream, "application/octet-stream", $"{accountId }- History.csv");
                }
                else
                {
                    response.Failed("Failed To get History");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                response.Failed("Something went wrong");
            }

            return Ok(response);
        }

    }
}
