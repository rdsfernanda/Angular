using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Persistence;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {


        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;

        }

        [HttpGet]
        public async Task <IActionResult> Get() //IActionResult pega o status code da requisição 
        {
            try{
                  var eventos = await _eventoService.GetEventosByAsync(true);
                  if(eventos==null) return NotFound("Nenhum evento encontrado"); // erro 404 de não encontrado 
                  return Ok(eventos); // retorna registros caso encontre 
            }
            catch(Exception ex){
              
                  return this.StatusCode(StatusCodes.Status500InternalServerError,
                  $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }

        }
        [HttpGet("{id}")]
        public async Task <IActionResult> GetByID(int id)
        {
              try{
                  var evento = await _eventoService.GetAllEventosByIdAsync(id,true);
                  if(evento==null) return NotFound("Evento por ID encontrado"); // erro 404 de não encontrado 
                  return Ok(evento); // retorna registros caso encontre 
            }
            catch(Exception ex){
              
                  return this.StatusCode(StatusCodes.Status500InternalServerError,
                  $"Erro ao tentar recuperar evento por ID. Erro: {ex.Message}");
            }

        }

        [HttpGet("{tema}/tema")] // colocar a barra para que o protocolo saiba onde localizar o item de busca
        public async Task <IActionResult> GetByTema(string tema)
        {
              try{
                  var evento = await _eventoService.GetAllEventosByTemaAsync(tema,true);
                  if(evento==null) return NotFound("Eventos por tema não encontrado"); // erro 404 de não encontrado 
                  return Ok(evento); // retorna registros caso encontre 
            }
            catch(Exception ex){
              
                  return this.StatusCode(StatusCodes.Status500InternalServerError,
                  $"Erro ao tentar recuperar evento por tema. Erro: {ex.Message}");
            }

        }

        [HttpPost]
        public async Task <IActionResult> Post(Evento model)
        {
           try{
                  var evento = await _eventoService.AddEventos(model);
                  if(evento==null) return BadRequest("Erro ao tentar adicionar evento.");
                  return Ok(evento); // retorna registros caso encontre 
            }
            catch(Exception ex){
              
                  return this.StatusCode(StatusCodes.Status500InternalServerError,
                  $"Erro ao tentar adicionar evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task <IActionResult> Put(int id, Evento model)
        {
            try{
                  var evento = await _eventoService.UpdateEvento(id,model);
                  if(evento==null) return BadRequest("Erro ao tentar atualizar evento.");
                  return Ok(evento); // retorna registros caso encontre 
            }
            catch(Exception ex){
              
                  return this.StatusCode(StatusCodes.Status500InternalServerError,
                  $"Erro ao tentar atualizar evento por tema. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> Delete(int id)
        {
            try{
                  if(await _eventoService.DeleteEvento(id))
                        return Ok("Deletado");
                  else 
                        return BadRequest("Evento não deletado");
            }
            catch(Exception ex){
              
                  return this.StatusCode(StatusCodes.Status500InternalServerError,
                  $"Erro ao tentar deletar eventos. Erro: {ex.Message}");
            }
        }
    }
}
