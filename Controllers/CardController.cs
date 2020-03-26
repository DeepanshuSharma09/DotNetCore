using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using CreditCardApi.Models;
using CreditCardApi.Services;

namespace CreditCardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly CardService _cardService;

        public CardController(CardService cardService)
        {
            this._cardService = cardService;
        }

        [HttpGet("{cardNumber}")]
        public ActionResult<Cards> Get(string cardNumber)
        {
            var cardDetails = this._cardService.GetCardDetails(cardNumber);

            if(cardDetails == null)
                return NotFound();

            return cardDetails;
        }

        [HttpDelete("{cardNumber}")]
        public ActionResult Delete(string cardNumber)
        {
            var card = this._cardService.GetCardDetails(cardNumber);

            if(card == null)
            {
                return NotFound();
            }

            this._cardService.Delete(cardNumber);

            return NoContent();
        }

        [HttpPost]
        public ActionResult Create(Cards card)
        {
            if(this._cardService.Create(card) !=null)
            {
                return Ok("Cards Details successfully saved.");
            }

            return BadRequest("Data Validation on card failed.");
        }

        [HttpPost]
        [Route("transaction")]
        public ActionResult Transaction(Transaction transactionDetails)
        {
            var cardDetails = this._cardService.GetCardDetails(transactionDetails.CardNumber);

            if(cardDetails == null) 
            {
                return NotFound();
            }

            if(transactionDetails.FlowDirection == "IN")
            {
                cardDetails.Balance += transactionDetails.Amount;
            }
            else
            {
                cardDetails.Balance -= transactionDetails.Amount;
            }

            this._cardService.Update(cardDetails);

            return Ok();
        }  
    }
}