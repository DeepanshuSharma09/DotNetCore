using MongoDB.Driver;
using CreditCardApi.Interfaces;
using CreditCardApi.Models;

namespace CreditCardApi.Services
{
    public class CardService
    {
        IMongoCollection<Cards> _cards;
        public CardService(IMongoDBSettings settings)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);
            var databases = mongoClient.GetDatabase(settings.DatabaseName);
            _cards = databases.GetCollection<Cards>(settings.CollectionName);
        }

        public Cards GetCardDetails(string cardNumber)
        {
            return this._cards.Find<Cards>(x=>x.CardNumber == cardNumber).FirstOrDefault();
        }

        public void Delete(string cardNumber)
        {
            this._cards.DeleteOne(card => card.CardNumber == cardNumber);
        }

        public Cards Create(Cards card)
        {
            if(this.ValidateCardDetails(card))
            {
                //card.Id = card.CardNumber;
                this._cards.InsertOne(card);
                return card;
            }
            return null;
        }

        public void Update(Cards card)
        {
            this._cards.ReplaceOne(X => X.CardNumber == card.CardNumber, card);
        }

        private bool ValidateCardDetails(Cards card)
        {
            return (checkLuhn(card.CardNumber) && (card.CardNumber.Length == 16));
        }

        private bool checkLuhn(string cardNo) 
        { 
            int nDigits = cardNo.Length; 
        
            int nSum = 0; 
            bool isSecond = false; 
            for (int i = nDigits - 1; i >= 0; i--)  
            { 
        
                int d = cardNo[i] - '0'; 
        
                if (isSecond == true) 
                    d = d * 2; 
        
                nSum += d / 10; 
                nSum += d % 10; 
        
                isSecond = !isSecond; 
            } 
            return (nSum % 10 == 0); 
        }
    }
}