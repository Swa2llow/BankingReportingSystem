using ReportingSystem.BLL.DTO;
using ReportingSystem.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReportingSystem.Controllers
{
    public class GenerateTransactionsController : ApiController
    {
        ICreditCardService cardService;
        ITransactionService transactionService;

        enum Status
        {
            success = 0,
            error = 1
        }

        public GenerateTransactionsController(ICreditCardService cddService, ITransactionService trService)
        {
            cardService = cddService;
            transactionService = trService;
        }
        // POST api/<controller>
        public void Post()
        {
            var cards = cardService.GetAll();

            foreach (var t in cards)
            {
                var tranAmount = new Random().Next(10000) / 10m;

                var st = ((t.AvailableCash - tranAmount) > 0) ? Status.success : Status.error;

                TransactionDTO model = new TransactionDTO
                {
                    CardId = t.Id,
                    CustomerId = t.CustomerId,
                    Amount = tranAmount,
                    Status = (int)st,
                    Message = st.ToString(),
                    CreationDate = DateTime.Now
                };

                transactionService.Create(model);
            }
        }
    }
}