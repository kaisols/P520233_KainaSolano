﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    public  class Movimiento
    {
        public Movimiento()
        {
            MiTipo = new MovimientoTipo();
            MiUsuario = new Usuario();

            Detalles = new List<MovimientoDetalle>();
                
        }

        public int MovimientoID { get; set; }

        public DateTime Fecha { get; set; }

        public int NumeroMovimiento { get; set; }

        public string Anotaciones { get; set; }

        public bool Agregar()
        {
            bool R = false;

            return R;
        }

        public bool Eliminar()
        {
            bool R = false;

            return R;
        }

        public bool ConsultarPorID()
        {
            bool R = false;

            return R;
        }

        public DataTable Listar()
        { 
            DataTable R = new DataTable();


            return R;
        }


        public MovimientoTipo MiTipo { get; set; }
        public Usuario MiUsuario { get; set; }

        //en el caso del detalle, si analizamos el diagrama de clases
        //vemos que al llegar a la clase de detale termina en "muchos"
        //1..*. Eso significa que el atribute tiene multiplicidad, 
        //o sea que se puede repetir n veces 
        List<MovimientoDetalle> Detalles { get; set; }

    }
}
