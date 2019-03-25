using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReportingSystem.BLL.Interfaces;
using AutoMapper;
using ReportingSystem.Models;
using ReportingSystem.BLL.DTO;

namespace ReportingSystem.Controllers
{
    public class TransactionApiController : ApiController
    {
        ITransactionService transactionService;
        public TransactionApiController(ITransactionService service)
        {
            transactionService = service;
        }

        // GET: api/TransactionApi
        public IHttpActionResult GetByTransaction(int Id)
        {
            if (Id <= 0)
                return BadRequest("Not a valid transaction id");

            var transactionDto = transactionService.Get(Id);
            if (transactionDto == null)
                return BadRequest("Transaction not found.");

            //CustomerTransactionViewModel

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TransactionDTO, TransactionViewModel>().ForMember(c => c.TransactionId, m => m.MapFrom(s => s.Id))).CreateMapper();
            var transaction = mapper.Map<TransactionDTO, TransactionViewModel>(transactionDto);

            return Ok(transaction);
        }

        //GET: api/TransactionApi/5
        public IHttpActionResult GetByCard(int creditCardId)
        {
            var transactionDto = transactionService.GetByCard(creditCardId);
            if (transactionDto == null)
                return BadRequest("Data not found.");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TransactionDTO, TransactionViewModel>().ForMember(c => c.TransactionId, m => m.MapFrom(s => s.Id))).CreateMapper();
            var transaction = mapper.Map<IEnumerable<TransactionDTO>, List<TransactionViewModel>>(transactionDto);

            return Ok(transaction);
        }

        // POST: api/CustomerApi
        public IHttpActionResult Post(int? creditCardId = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            if (creditCardId.HasValue)
            {
                if (creditCardId.Value <= 0)
                    return BadRequest("Not a valid creditCardId");
            }

            var transactionDto = transactionService.GetAll(creditCardId, fromDate, toDate);
            if (transactionDto == null)
                return BadRequest("Transaction not found.");


            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TransactionDTO, TransactionViewModel>().ForMember(c => c.TransactionId, m => m.MapFrom(s => s.Id))).CreateMapper();
            var transaction = mapper.Map<IEnumerable<TransactionDTO>, List<TransactionViewModel>>(transactionDto);

            return Ok(transaction);
        }
    }
}
