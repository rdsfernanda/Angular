using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {
        public int ID { get; set; }
        public string Local { get; set; }
        public DateTime dataEvento { get; set; }
        [Required (ErrorMessage ="O campo {0} é obrigatório."),
        StringLength(50, MinimumLength =3, ErrorMessage ="{0} Deve ter entre 3 a 50 caracteres.")
        ] // tema é obrigatório
        public string Tema { get; set; }
        [Required(ErrorMessage ="{0} é obrigatório.")]

        [Display(Name ="Qtd Pessoas"),
        Range(1,120000, ErrorMessage ="{0} não pode ser menor que 1 e maior que 120 mil.")]
        public int qtdPessoas { get; set; }
        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage ="Não é uma imagem válida. (Gif, jpg,jpeg,bmp,png")]
        public string ImagemURL { get; set; }
        [Required(ErrorMessage ="o campo {0} é obrigatório."),
        Phone(ErrorMessage="o campo {0} está inválido.")]
        public string Telefone { get; set; }
        [Display(Name ="E-mail"), // renomea o campo 
        EmailAddress(ErrorMessage ="Digite um {0} válido."),
        Required(ErrorMessage ="{0} é obrigatório")]
        public string Email { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> Palestrantes { get; set; }
    }
}