using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Domain
{
    public class Address
    {
        public Address(string city, string street, string houseNumber) {
            SetCity(city);
            SetStreet(street);
            SetHouseNo(houseNumber);
        }

        public string City { get; private set; }
        public string Street { get; private set; }
        public string HouseNumber { get; private set; }

        public void SetCity(string city) {
            if (string.IsNullOrWhiteSpace(city)) throw new AddressException("SetCity - city is empty");
            this.City = city;
        }
        public void SetStreet(string street) { 
            if (string.IsNullOrWhiteSpace(street)) throw new AddressException("SetStreet - street is empty");
            this.Street = street;
        }
        public void SetHouseNo(string houseNo) {
            if (string.IsNullOrWhiteSpace(houseNo)) throw new AddressException("SetHouseNo - house number is empty");
            this.HouseNumber = houseNo;
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
    }
}