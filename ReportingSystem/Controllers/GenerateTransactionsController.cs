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
                var st = ((t.AvailableCash - 1000) > 0) ? Status.success : Status.error;

                TransactionDTO model = new TransactionDTO
                {
                    CardId = t.Id,
                    CustomerId = t.CustomerId,
                    Amount = 1000,
                    Status = (int)st,
                    Message = st.ToString(),
                    CreationDate = DateTime.Now
                };

                transactionService.Create(model);
            }
        }
    }
}