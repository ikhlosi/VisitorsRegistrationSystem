using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Domain
{
    public class Address
    {
        public Address(string city, string street, string houseNumber, string? busNumber) {
            SetCity(city);
            SetStreet(street);
            SetHouseNo(houseNumber);
            SetBusNo(busNumber);
        }
         public Address(int id,string city, string street, string houseNumber, string? busNumber) {
            setId(id);
            SetCity(city);
            SetStreet(street);
            SetHouseNo(houseNumber);
            SetBusNo(busNumber);
        }

        public int Id { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string HouseNumber { get; private set; }
        public string BusNumber { get; private set; }


        public void SetCity(string city) {
            if (string.IsNullOrWhiteSpace(city)) throw new AddressException("SetCity - city is empty");
            this.City = city;
        }
        public void SetStreet(string street) { 
            if (string.IsNullOrWhiteSpace(street)) throw new AddressException("SetStreet - street is empty");
            this.Street = street;
        }

        public void setId(int id)
        {
            if(id < 1) throw new AddressException("SetId - id smaller than 1");
            this.Id = id;
        }
        public void SetHouseNo(string houseNo) {
            if (string.IsNullOrWhiteSpace(houseNo)) throw new AddressException("SetHouseNo - house number is empty");
            this.HouseNumber = houseNo;
        }
        public void SetBusNo(string? busNo)
        {
            this.BusNumber = busNo;
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
            return "city: " + City + " street: " + Street + " housenumber: " + HouseNumber + " busnumber: " + BusNumber;
        }
    }
}