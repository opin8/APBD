using System.Net;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PrzykladowyEgzamin.Context;
using PrzykladowyEgzamin.Models;

namespace PrzykladowyEgzamin.Services;

public class ClientService : IClientService
{
    private readonly BoatsContext _boatsContext;

    public ClientService(BoatsContext boatsContext)
    {
        _boatsContext = boatsContext;
    }

    public IEnumerable<Reservation> GetReservationByClientId(int clientId)
    {
        return _boatsContext.Reservations.Where(r => r.IdClient == clientId).ToList();
    }

    public int AddReservation(int clientId, DateOnly dateFrom, DateOnly dateTo, int idBoatStandard, int numOfBoats)
    {
        using var transaction = _boatsContext.Database.BeginTransaction();
        
        var client = _boatsContext.Clients.Include(c => c.Reservations)
            .FirstOrDefault(c => c.IdClient == clientId);

        if (client == null)
        {
            throw new Exception($"Couldn't find client with Id = {clientId}");
        }

        var activeReservation = client.Reservations.FirstOrDefault(r => r.Fulfilled == null);

        if (activeReservation != null)
        {
            throw new Exception("Client has a pending reservation");
        }

        var boatStandard = _boatsContext.BoatStandards
            .FirstOrDefault(bs => bs.IdBoatStandard == idBoatStandard);

        if (boatStandard == null)
        {
            throw new Exception($"Nie znaleziono standardu łodzi o IdBoatStandard = {idBoatStandard}");
        }
        
        var overlappingReservations = _boatsContext.Reservations
            .Where(r => r.IdBoatStandard == idBoatStandard &&
                        !(r.DateTo < dateFrom || r.DateFrom > dateTo) &&
                        (r.Fulfilled == null || r.Fulfilled == true));
        
        var reservedBoatsCount = overlappingReservations.Sum(r => r.NumOfBoats);

        var totalAvailableBoats = _boatsContext.BoatStandards.Count(bs => bs.IdBoatStandard == idBoatStandard);

        if (totalAvailableBoats - reservedBoatsCount < numOfBoats)
        {
            throw new Exception($"Nie ma wystarczającej liczby żaglówek standardu {idBoatStandard} w tym przedziale czasowym.");
        }

        var newReservation = new Reservation
        {
            IdClient = clientId,
            DateFrom = dateFrom,
            DateTo = dateTo,
            IdBoatStandard = idBoatStandard,
            NumOfBoats = numOfBoats,
            Fulfilled = false
        };

        _boatsContext.Reservations.Add(newReservation);

        var price = CalculatePrice(newReservation, client);

        newReservation.Price = price;

        _boatsContext.SaveChanges();
        
        transaction.Commit();

        return newReservation.IdReservation;
    }
    
    private decimal CalculatePrice(Reservation reservation, Client client)
    {
        decimal basePrice = 100;
        decimal discount = client.ClientCategory.DiscountPerc;
        return basePrice * reservation.NumOfBoats * (1 - discount);
    }
}