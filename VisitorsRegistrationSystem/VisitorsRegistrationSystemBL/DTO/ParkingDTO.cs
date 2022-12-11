using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsRegistrationSystemBL.DTO
{
    public class ParkingDTO
    {
        public ParkingDTO(int iD, int totalSpaces, int occupiedSpaces, bool isFull)
        {
            ID = iD;
            this.totalSpaces = totalSpaces;
            this.occupiedSpaces = occupiedSpaces;
            this.isFull = isFull;
        }

        public int ID { get; set; }
        public int totalSpaces { get; set; }
        public int occupiedSpaces { get; set; }
        public bool isFull { get; set; }

        public override string ToString()
        {
            return $"{ID} {totalSpaces} {occupiedSpaces} {isFull}";
        }
    }
}
