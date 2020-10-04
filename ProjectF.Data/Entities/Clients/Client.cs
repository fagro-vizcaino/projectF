using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Countries;
using System;

namespace ProjectF.Data.Entities.Clients
{
    public class Client : Entity
    {
        public Code Code { get; private set; }
        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Phone Phone { get; private set; }
        public DateTime? Birthday { get; private set;}
        public string Rnc { get; private set; }
        public string HomeOrApartment { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public virtual Country Country { get; private set; }

        protected Client() { }

        public Client(Code code
            , Name name
            , Email email
            , Phone phone
            , DateTime? birthday
            , string rnc
            , string homeOrApartment
            , string city
            , string street
            , Country country) =>
            (Code, Name, Email, Phone, Rnc, Birthday, HomeOrApartment, City, Street, Country)
            = (code, name, email, phone, rnc, birthday, homeOrApartment, city, street, country);


        public void Deconstruct(out Code dcode
            , out Name dname
            , out Email demail
            , out Phone dphone
            , out string drnc
            , out DateTime? dbirthday
            , out string dhomeOrApartment
            , out string dcity
            , out string dstreet
            , out Country dcountry)
        {
            dcode            = Code;
            dname            = Name;
            demail           = Email;
            dphone           = Phone;
            drnc             = Rnc;
            dbirthday        = Birthday;
            dhomeOrApartment = HomeOrApartment;
            dcity            = City;
            dstreet          = Street;
            dcountry         = Country;

        }

        public void EditUserClient(
             Code code
            , Name name
            , Email email
            , Phone phone
            , string rnc
            , DateTime? birthday
            , string homeOrApartment
            , string city
            , string street
            , Country country)
        {
            Code            = code;
            Name            = name;
            Email           = email;
            Phone           = phone;
            Rnc             = rnc;
            Birthday        = birthday;
            HomeOrApartment = homeOrApartment;
            City            = city;
            Street          = street;
            Country         = country;
        }

        public static implicit operator ClientDto(Client client)
            => new ClientDto(client.Id,
                client.Code.Value,
                client.Name.Value,
                client.Email,
                client.Phone.Value,
                client.Rnc,
                client.Birthday,
                client.HomeOrApartment,
                client.City,
                client.City,
                client.Country?.Id ?? 0,
                client.Country);
    }
}
