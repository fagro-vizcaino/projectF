using System.Collections.Generic;
using System.Linq;
using ProjectF.Data.Entities.Categories;
using ProjectF.Data.Entities.PaymentList;
using ProjectF.Data.Entities.Warehouses;
using ProjectF.Data.Entities.Banks;
using System;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Context;

namespace ProjectF.Data.FakeData
{
    //public class PrivateFaker<T> : Faker<T> where T : class
    //{
    //    public PrivateFaker<T> UsePrivateConstructor()
    //    {
    //        return base.CustomInstantiator(f => Activator.CreateInstance(typeof(T), nonPublic: true) as T)
    //           as PrivateFaker<T>;
    //    }

    //    public PrivateFaker<T> RuleForPrivate<TProperty>(string propertyName, Func<Faker, TProperty> setter)
    //    {
    //        base.RuleFor(propertyName, setter);
    //        return this;
    //    }
    //}

    //public  class FakeDataGenerator
    //{
    //    private readonly _AppDbContext _context;
    //    public FakeDataGenerator(_AppDbContext context)
    //    {
    //        _context = context;
    //    }
    //    public static int AmountPerEntity { get; set; } = 100;
    //    public static IEnumerable<Category> Categories()
    //    {
    //        var CategoryFaker = new Faker<CategoryFake>()
    //            .RuleFor(c => c.Name, x => x.Commerce.Categories(1)[0])
    //            .RuleFor(c => c.Code, x => x.Random.String(4, 4, 'C', 'Z'))
    //            .RuleFor(c => c.ShowOn, x => x.Random.Bool());

    //        return CategoryFaker.Generate(100);
    //    }

    //    public static List<PaymentTerm> PaymentTerms()
    //    {
    //        var paymentTermFaker = new Faker<PaymentTerm>()
    //            .RuleFor(c => c.Id, x => x.Random.Int())
    //            .RuleFor(c => c.Description.Value, x => x.Random.String(4,20))
    //            .RuleFor(c => c.DayValue, x => x.Random.Int(1,200));

    //        return paymentTermFaker.Generate(AmountPerEntity);
    //    }

    //    public static List<Warehouse> Warehouses()
    //    {
    //        var paymentTermFaker = new Faker<Warehouse>()
    //            .RuleFor(c => c.Id, x => x.Random.Int())
    //            .RuleFor(c => c.Name.Value, x => x.Random.String(4, 20))
    //            .RuleFor(c => c.Location, x => x.Random.String(4, 20));
            
    //        return paymentTermFaker.Generate(AmountPerEntity);
    //    }

    //    public static List<BankAccountType> BankAccountTypes()
    //    {
    //        var bankAccountTypeFaker = new Faker<BankAccountType>()
    //            .RuleFor(c => c.Id, x => x.Random.Int())
    //            .RuleFor(c => c.Name.Value, c => c.Finance.AccountName())
    //            .RuleFor(c => c.Description.Value, c => c.Random.String(4, 20));

    //        return bankAccountTypeFaker.Generate(AmountPerEntity);
    //    }

    //    public static List<BankAccount> BankAccounts()
    //    {
    //        var bankAccount = new Faker<BankAccount>()
    //            .RuleFor(c => c.Id, x => x.Random.Int())
    //            .RuleFor(c => c.AccountName.Value, x => x.Finance.AccountName())
    //            .RuleFor(c => c.AccountNumber, x => x.Finance.Account())
    //            .RuleFor(c => c.Description.Value, x => x.Random.String(4, 20))
    //            .RuleFor(c => c.Created, _ => DateTime.Now);

    //        return bankAccount.Generate(AmountPerEntity);
    //    }


    //}
}
