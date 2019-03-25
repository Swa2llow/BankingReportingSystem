using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ReportingSystem.BLL.DTO;
using ReportingSystem.BLL.Interfaces;
using AutoMapper;
using ReportingSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ReportingSystem.Controllers
{
    public class CreditCardApiController : ApiController
    {
        ICreditCardService cardService;

        public CreditCardApiController(ICreditCardService service)
        {
            cardService = service;
        }

        // GET: api/CreditCardApi
        public IHttpActionResult GetAllCreditCards()
        {
            var customerCreditCards = cardService.GetAllCards();
            if (customerCreditCards == null)
                return BadRequest("Customer not found.");

            return Ok(customerCreditCards);
        }

        // GET: api/CreditCardApi/5
        public IHttpActionResult GetCustomerById(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid customer id");

            var creditCardDTO = cardService.GetCard(id);
            if (creditCardDTO == null)
                return BadRequest($"Curd with Id {id} not found.");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CreditCardsDTO, CreditCardViewModel>().ForMember(c => c.CardId, m => m.MapFrom(s => s.Id))).CreateMapper();
            var customer = mapper.Map<CreditCardsDTO, CreditCardViewModel>(creditCardDTO);

            return Ok(customer);
        }

        // POST: api/CustomerApi
        public IHttpActionResult Post([FromBody]CreditCardViewModel value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (value is null)
                return BadRequest("Parameter type is null.");

            if (value.CustomerId <= 0)
                return BadRequest("Not a valid customer id");

            if (value.AvailableCash < 0)
                return BadRequest("AvailableCash must be greater or equal 0");

            if (value.ExpirationDate < DateTime.Now)
                return BadRequest("ExpirationDate must be in future");

            if (value.CardHolderName.Contains("string"))
                return BadRequest("Please change default values");

            if (!Regex.IsMatch(value.CardNumber.Trim(), "^[0-9]+$", RegexOptions.Compiled))
                return BadRequest("CardNumber invalide format. Must be number");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CreditCardViewModel, CreditCardsDTO>().ForMember(c => c.CustomerId, m => m.MapFrom(s => s.CustomerId))).CreateMapper();
            var model = mapper.Map<CreditCardViewModel, CreditCardsDTO>(value);

            cardService.CreateCard(model);
            return Ok();
        }

        // PUT: api/CustomerApi/5
        public IHttpActionResult Put([FromBody]EditCreditCardViewModel creditcard)
        {
            if (creditcard.CardId <= 0)
                return BadRequest("Not a valid card id");
            
            if (creditcard.AvailableCash < 0)
                return BadRequest("AvailableCash must be greater or equal 0");

            if (creditcard.ExpirationDate < DateTime.Now)
                return BadRequest("ExpirationDate must be in future");

            if (creditcard is null)
                return BadRequest("Parameter type is null.");

            if (creditcard.CardHolderName.Contains("string"))
                return BadRequest("Please change default values");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EditCreditCardViewModel, CreditCardsDTO>().ForMember(c => c.Id, m => m.MapFrom(s => s.CardId))).CreateMapper();
            var creditCardDTO = mapper.Map<EditCreditCardViewModel, CreditCardsDTO>(creditcard);

            try
            {
                cardService.Update(creditCardDTO);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // DELETE: api/CustomerApi/5
        public IHttpActionResult Delete(int creditCardId)
        {
            if (creditCardId <= 0)
                return BadRequest("Not a valid card id");

            var creditCardDTO = cardService.GetCard(creditCardId);
            if (creditCardDTO == null)
                return BadRequest("Customer not found.");

            cardService.Delete(creditCardDTO);
            return Ok(creditCardDTO);
        }
    }
}