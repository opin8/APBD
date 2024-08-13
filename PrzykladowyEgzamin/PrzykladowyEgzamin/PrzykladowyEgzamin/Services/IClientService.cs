using PrzykladowyEgzamin.Models;

namespace PrzykladowyEgzamin.Services;

public interface IClientService
{
    public IEnumerable<Reservation> GetReservationByClientId(int clientId);

    public int AddReservation(int clientId, DateOnly dateFrom, DateOnly dateTo, int idBoatStandard, int numOfBoats);
    
    

}