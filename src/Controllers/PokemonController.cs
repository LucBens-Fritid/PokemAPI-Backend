using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pokemAPI.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemAPI_Backend.Controllers;

[ApiController]
[Route("api[controller]")]
public class PokemonController : ControllerBase
{
     private readonly HttpClient _httpClient;

    public PokemonController(ILogger<PokemonController> logger)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
    }



    [HttpGet("{name}")]
    public async Task<IActionResult> GetPokemonByName(string name)
    {
        try {
            HttpResponseMessage response = await _httpClient.GetAsync($"pokemon/{name.ToLower()}");

            if(response.IsSuccessStatusCode) {

                string result = await response.Content.ReadAsStringAsync();
                var pokemon = JsonSerializer.Deserialize<Pokemon>(result);

                var pokemonInfo = new {
                    Id = pokemon.Id,
                    Name = pokemon.Name,
                    ImageUrl = pokemon.Sprites.FrontDefault

                };
                
                return Ok(pokemonInfo);
                
            } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {

                return StatusCode((int)response.StatusCode);
            }
        } catch {
                return StatusCode(500);
        }
        return StatusCode(500);
    }
}
