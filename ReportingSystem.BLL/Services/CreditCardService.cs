using ReportingSystem.BLL.DTO;
using ReportingSystem.BLL.Interfaces;
using ReportingSystem.DAL.Entities;
using ReportingSystem.DAL.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using ReportingSystem.BLL.Infrastructure;
using System.Linq;
using System;

namespace ReportingSystem.BLL.Services
{
    public class CreditCardService : ICreditCardService
    {
        IUnitOfWork Database { get; set; }

        public CreditCardService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<CreditCardsDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CreditCards, CreditCardsDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<CreditCards>, List<CreditCardsDTO>>(Database.CreditCards.GetAll());
        }

            public CustomerCreditCardsView GetAllCards()
        {
            var customers = Database.Customers.GetAll();
            
            List<CustomerCreditCards> model = new List<CustomerCreditCards>();
            
            foreach (var cust in customers)
            {
                CustomerCreditCards custom = new CustomerCreditCards
                {
                    CustomerId = cust.Id,
                    FirstName = cust.FirstName,
                    LastName = cust.LastName,
                    Email = cust.Email,
                    Phone = cust.Phone,
                    Address = cust.Address,
                    CreditCards = new List<CreditCardView>()
                };

                var creditCardAll = Database.CreditCards.GetAll().Where(c => c.CustomerId == cust.Id);

                if(creditCardAll.ToList().Count > 0)
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CreditCards, CreditCardView>().ForMember(c => c.CreditCardId, m => m.MapFrom(s => s.Id))).CreateMapper();
                    custom.CreditCards = mapper.Map<IEnumerable<CreditCards>, List<CreditCardView>>(creditCardAll);                    
                }

                if(custom.CreditCards.Count > 0)
                    model.Add(custom);
            }

            CustomerCreditCardsView modelList = new CustomerCreditCardsView
            {
                Customers = model
            };

            return modelList;
        }

        public CreditCardsDTO GetCard(int creditCardId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CreditCards, CreditCardsDTO>()).CreateMapper();
            return mapper.Map<CreditCards, CreditCardsDTO>(Database.CreditCards.Get(creditCardId));
        }

        public void CreateCard(CreditCardsDTO cardDto)
        {
            if (cardDto.ExpirationDate <= DateTime.Now)
                throw new ValidationException($"Couldn't add expired card", "");

            Customers customer = Database.Customers.Get(cardDto.CustomerId);
            
            if (customer == null)
                throw new ValidationException($"Customer {cardDto.CustomerId} not exist", "");

            CreditCards card = Database.CreditCards.GetAll().Where(c => c.CardNumber.Contains(cardDto.CardNumber)).FirstOrDefault();
            if (card != null)
                throw new ValidationException($"Such credit card {cardDto.CardNumber} exists", "");
            
            CreditCards newCard = new CreditCards
            {
                CustomerId = customer.Id,
                CardNumber = cardDto.CardNumber,
                CardHolderName = cardDto.CardHolderName,
                ExpirationDate = cardDto.ExpirationDate
            };

            Database.CreditCards.Create(newCard);
            Database.Save();
        }

        public void Update(CreditCardsDTO cardDto)
        {
            CreditCards card = Database.CreditCards.Get(cardDto.Id);
            if (card == null)
                throw new ValidationException($"Credit card with number {cardDto.CardNumber} not exists", "");

            if (card != null)
            {
                if (!String.IsNullOrEmpty(cardDto.CardHolderName))
                    card.CardHolderName = cardDto.CardHolderName;

                if (cardDto.AvailableCash >= 0 )
                    card.AvailableCash = cardDto.AvailableCash;

                if (cardDto.ExpirationDate >= DateTime.Now)
                    card.ExpirationDate = cardDto.ExpirationDate;
            }

            Database.CreditCards.Update(card);
            Database.Save();
        }

        public void Delete(CreditCardsDTO cardDto)
        {
            CreditCards card = Database.CreditCards.Get(cardDto.Id);
            if (card == null)
                throw new ValidationException($"Credit card with number {cardDto.CardNumber} not exists", "");

            Database.CreditCards.Delete(card);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
