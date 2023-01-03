﻿using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Domain
{
    public class Address
    {
        //todo POSTCODE
        public Address(string city,string postalCode, string street, string houseNumber, string? busNumber) {
            SetCity(city);
            SetPostalCode(postalCode);
            SetStreet(street);
            SetHouseNo(houseNumber);
            SetBusNo(busNumber);
        }
         public Address(int id,string city,string postalCode, string street, string houseNumber, string? busNumber) {
            setId(id);
            SetCity(city);
            SetPostalCode(postalCode);
            SetStreet(street);
            SetHouseNo(houseNumber);
            SetBusNo(busNumber);
        }

        public int Id { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }
        public string Street { get; private set; }
        public string HouseNumber { get; private set; }
        public string BusNumber { get; private set; }
        public void SetCity(string city) {
            if (string.IsNullOrWhiteSpace(city)) throw new AddressException("Address - SetCity - city is empty");
            this.City = city;
        }
        public void SetStreet(string street) { 
            if (string.IsNullOrWhiteSpace(street)) throw new AddressException("Address - SetStreet - street is empty");
            this.Street = street;
        }

        public void setId(int id)
        {
            if(id < 1) throw new AddressException("Address - SetId - id smaller than 1");
            this.Id = id;
        }
        public void SetHouseNo(string houseNo) {
            if (string.IsNullOrWhiteSpace(houseNo)) throw new AddressException("Address - SetHouseNo - house number is empty");
            this.HouseNumber = houseNo;
        }
        public void SetBusNo(string? busNo)
        {
            this.BusNumber = busNo;
        }

        public void SetPostalCode(string postalCode)
        {
            if (string.IsNullOrWhiteSpace(postalCode)) throw new AddressException("Address - SetPostalCode - postal code is empty");
            this.PostalCode = postalCode;
        }
        public override bool Equals(object? obj) {
            return obj is Address address &&
                   City == address.City &&
                   Street == address.Street &&
                   HouseNumber == address.HouseNumber;
        }

        public override int GetHashCode() {
            return HashCode.Combine(City, Street, HouseNumber);
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(BusNumber))
            {
                return $"{Street} {HouseNumber} {PostalCode} {City}";
            } else return $"{Street} {HouseNumber}/{BusNumber} {PostalCode} {City}";
        }
    }
}