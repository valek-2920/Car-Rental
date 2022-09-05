using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTest.Domain.Models.DataModels
{
    public class Car
    {
        [Key]
        public int Placa { get; set; }

        [Required(ErrorMessage = "La marca del carro es requerido.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "El modelo del carro es requerido.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "El Año de fabricación del carro es requerido.")]
        [DataType(DataType.Date)]
        public DateTime AnnoFabricacion { get; set; }

        public ReservationStatus Status { get; set; }

    }
}
