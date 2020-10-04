using ProjectF.Data.Entities.Common;
using ProjectF.Data.Repositories;

namespace ProjectF.Data.Entities.Currencies
{
    public class Currency
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        protected Currency() { }

        public Currency(int id, string name)
        {
            Id          = id;
            Name        = name;
        }
    }
}