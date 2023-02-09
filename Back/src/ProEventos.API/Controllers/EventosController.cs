using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;
using ProEventos.Application.Dtos;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {


        private readonly IEventoService _eventoService;
        private readonly IWebHostEnvironment _hostEnviroment;

        public EventosController(IEventoService eventoService,IWebHostEnvironment hostEnviroment)
        {
            _eventoService = eventoService;

        }

        [HttpGet]
        public async Task <IActionResult> Get() //IActionResult pega o status code da requisição 
        {
            try{
                  var eventos = await _eventoService.GetEventosByAsync(true);
                  if(eventos==null) return NoContent(); // se evento for ==null, retorna noContent
                  
                  var eventosRetorno = new List<EventoDto>();
                  foreach(var evento in eventos)
                  {
                        eventosRetorno.Add(new EventoDto(){
                        ID =evento.ID,
                        Local = evento.Local,
                        dataEvento=Convert.ToDateTime(evento.dataEvento),
                        Tema=evento.Tema,
                        qtdPessoas = evento.qtdPessoas,
                        ImagemURL = evento.ImagemURL,
                        Telefone = evento.Telefone,
                        Email = evento.Email
                        });
                  }
                  return Ok(eventosRetorno); // retorna registros caso encontre 
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
                  if(evento==null) return NoContent();  
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
                  if(evento==null) return NoContent();  
                  return Ok(evento); // retorna registros caso encontre 
            }
            catch(Exception ex){
              
                  return this.StatusCode(StatusCodes.Status500InternalServerError,
                  $"Erro ao tentar recuperar evento por tema. Erro: {ex.Message}");
            }

        }

        [HttpPost]
        public async Task <IActionResult> Post(EventoDto model)
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
        
        [HttpPost("upload-image/{eventoId}")] // endpoint upload image
        public async Task <IActionResult> UploadImage(int eventoId)
        {
           try{
                  var evento = await _eventoService.GetAllEventosByIdAsync(eventoId,true);
                  if(evento==null) return NoContent();

                  var file = Request.Form.Files[0];
                  if(file.Length>0){

                        DeleteImage(evento.ImagemURL);
                        evento.ImagemURL=await SaveImage(file);
                  }
                  var eventosRetorno=await _eventoService.UpdateEvento(eventoId,evento);
                  return Ok(eventosRetorno);
            }
            catch(Exception ex){
              
                  return this.StatusCode(StatusCodes.Status500InternalServerError,
                  $"Erro ao tentar adicionar evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task <IActionResult> Put(int id, EventoDto model)
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
                 return await _eventoService.DeleteEvento(id)
                       ? Ok(new {message ="Deletado"})
                       :throw new Exception ("Ocorreu um problema não especificado.");
                   
                        return BadRequest("Evento não deletado");
            }
            catch(Exception ex){
              
                  return this.StatusCode(StatusCodes.Status500InternalServerError,
                  $"Erro ao tentar deletar eventos. Erro: {ex.Message}");
            }
        }
        [NonAction]

        public async Task<string> SaveImage(IFormFile imageFile){

            string imageName= new String(Path.GetFileNameWithoutExtension(imageFile.FileName)
            .Take(10) // pega os 10 caracteres do nome do arquivo (imagem)
            .ToArray()
            ).Replace(' ','-');

           imageName=$"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";
           var imagePath=Path.Combine(_hostEnviroment.ContentRootPath,@"Resources/Images",imageName);

           using(var fileStream = new FileStream(imagePath,FileMode.Create)){
            await imageFile.CopyToAsync(fileStream);
           }
            return imageName;

        }
        [NonAction]

        public void DeleteImage(string imageName){

            var imagePath = Path.Combine(_hostEnviroment.ContentRootPath,@"Resources/Images",imageName);
            if(System.IO.File.Exists(imagePath))
            System.IO.File.Delete(imagePath);
            

        }
    }
}
