using System;

namespace OrdersApplication.Domain
{
    public class Order : Entity
    {
        public Order(int id, string text)
            : base(id)
        {
            Text = text;
            CreateRandomNumber();
        }

        public int Random { get; private set; }
        public string Text { get; set; }

        protected void CreateRandomNumber()
        {
            Random random = new Random();
            Random = random.Next(1, 10);
        }
    }

}
