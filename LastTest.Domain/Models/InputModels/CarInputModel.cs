using System;
using System.ComponentModel.DataAnnotations;

namespace LastTest.Domain.Models.InputModels
{
    public class CarInputModel
    {
        [Required]
        public int Placa { get; set; }

        [Required(ErrorMessage = "La marca del carro es requerido.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "El modelo del carro es requerido.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "El Año de fabricación del carro es requerido.")]
        public DateTime AnnoFabricacion { get; set; }
    }
}
