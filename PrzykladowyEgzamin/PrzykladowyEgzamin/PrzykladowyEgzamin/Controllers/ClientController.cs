using Microsoft.AspNetCore.Mvc;
using PrzykladowyEgzamin.Context;
using PrzykladowyEgzamin.Models;
using PrzykladowyEgzamin.Services;

namespace PrzykladowyEgzamin.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{

    private readonly ClientService _clientService;

    public ClientController(ClientService clientService)
    {
        _clientService = clientService;
    }
    
    [HttpGet("{idClient}/reservations")]
    public async Task<IActionResult> GetReservationByClientId(Client idClient)
    {
        var result = _clientService.GetReservationByClientId(idClient.IdClient);

        result = result.OrderByDescending(r => r.DateTo).ToList();
        
        return Ok(result);
    }
    
    [HttpPost("{idClient}/reservations/create")]
    public async Task<IActionResult> AddReservation([FromRoute]int idClient, [FromBody]ReservationRequest request)
    {
        try
        {
            var reservationId = _clientService.AddReservation(idClient, request.DateFrom, request.DateTo, request.IdBoatStandard, request.NumOfBoats);
            return Ok(new { ReservationId = reservationId });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
    
    public class ReservationRequest
    {
        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }
        public int IdBoatStandard { get; set; }
        public int NumOfBoats { get; set; }
    }
}