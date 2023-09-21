namespace TallerEntity.Models
{
    public class ReservationCreate
    {
        public Guid ReservationID { get; set; }

        public int Cedula { get; set; }

        public int RoomID { get; set; }

        public DateTime ReservationDate { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int Customersln { get; set; }

    }
}
