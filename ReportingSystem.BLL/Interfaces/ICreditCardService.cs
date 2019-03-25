using ReportingSystem.BLL.DTO;
using System.Collections.Generic;

namespace ReportingSystem.BLL.Interfaces
{
    public interface ICreditCardService
    {
        IEnumerable<CreditCardsDTO> GetAll();
        CreditCardsDTO GetCard(int creditCardId);
        CustomerCreditCardsView GetAllCards();
        void CreateCard(CreditCardsDTO creditCard);
        void Update(CreditCardsDTO creditCard);
        void Delete(CreditCardsDTO creditCard);
        void Dispose();
    }
}
