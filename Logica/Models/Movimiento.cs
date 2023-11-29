﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

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

            //primero hacemos un insert en el encabezado y recolectamos el ID que se genera
            //esto es indispensable ya que se necesita como fk en la tabla de detalle
            Conexion MyCnn = new Conexion();

            MyCnn.ListaDeParametros.Add(new SqlParameter("@Fecha", this.Fecha));
            MyCnn.ListaDeParametros.Add(new SqlParameter("@Anotaciones", this.Anotaciones));
            MyCnn.ListaDeParametros.Add(new SqlParameter("@TipoMovimiento", this.MiTipo.MovimientoTipoID));
            MyCnn.ListaDeParametros.Add(new SqlParameter("@UsuarioID", this.MiUsuario.UsuarioID));

            //generico
            Object RetornoSPAgregar = MyCnn.EjecutarSELECTEscalar("SPMovimientosAgregarEncabezado");

            int IDMovimientoRecienCreado;

            if (RetornoSPAgregar != null)
            {
                //especialziado
                IDMovimientoRecienCreado = Convert.ToInt32(RetornoSPAgregar.ToString());

                foreach (MovimientoDetalle item in this.Detalles)
                {
                    //por cada iteracion de lista de detalles hacemos un insert en la 
                    //tabla de detalle

                    Conexion MyCnnDetalle = new Conexion();

                    MyCnnDetalle.ListaDeParametros.Add(new SqlParameter("@IDMovimiento", IDMovimientoRecienCreado));
                    MyCnnDetalle.ListaDeParametros.Add(new SqlParameter("@IDProducto", item.MiProducto.ProductoID));
                    MyCnnDetalle.ListaDeParametros.Add(new SqlParameter("@Cantidad", item.CantidadMovimiento));
                    MyCnnDetalle.ListaDeParametros.Add(new SqlParameter("@Costo", item.Costo));
                    MyCnnDetalle.ListaDeParametros.Add(new SqlParameter("@SubTotal", item.SubTotal));
                    MyCnnDetalle.ListaDeParametros.Add(new SqlParameter("@TotalIVA", item.TotalIVA));
                    MyCnnDetalle.ListaDeParametros.Add(new SqlParameter("@PrecioUnitario", item.PrecioUnitario));

                    MyCnnDetalle.EjecutarDML("SPMovimientosAgregarDetalle");

                }

                R = true;
            }

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
        //vemos que al llegar a la clase de detalle termina en "muchos"
        //1..*. Eso significa que el atribute tiene multiplicidad, 
        //o sea que se puede repetir n veces 
        public List<MovimientoDetalle> Detalles { get; set; }


        public DataTable AsignarEsquemaDelDetalle()
        {
            DataTable R = new DataTable();

            Conexion MyCnn = new Conexion();

            //queremos cargar el esquema del datatable, NO los datos
            R = MyCnn.EjecutarSelect("SPMovimientoCargarDetalle", true);

            //para evitar el identity (1,1) que está originalmente en la tabla
            //me genere numeros unicos que impidan repetir registros
            R.PrimaryKey = null;

            return R;
        }


    }
}
