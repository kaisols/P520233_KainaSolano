﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Globalization;
using Logica.Tools;

namespace Logica.Models
{
    public class Usuario
    {
        public Usuario()
        {
            MiUsuarioRol = new UsuarioRol();    
        }

        public int UsuarioID { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contrasennia { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }

        public UsuarioRol MiUsuarioRol { get; set; }

        //comportamientos, operaciones, funciones, métodos

        public bool Agregar()
        {
            bool R = false; 

            Conexion MiCnn = new Conexion();

            //ahora agregamos todos los parámetros que solicita el SP de agregar
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Cedula", this.Cedula));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Nombre", this.Nombre));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Correo", this.Correo));
            
            Tools.Crypto MiEncriptador = new Tools.Crypto();
            string ContrasenniaEncriptada = MiEncriptador.EncriptarEnUnSentido(this.Contrasennia);
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Contrasennia", ContrasenniaEncriptada));

            MiCnn.ListaDeParametros.Add(new SqlParameter("@Telefono", this.Telefono));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Direccion", this.Direccion));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@UsuarioRolID", this.MiUsuarioRol.UsuarioRolID));

            int resultado = MiCnn.EjecutarDML("SPUsuariosAgregar");

            if (resultado > 0) R = true;

            return R;
        }

        public bool Actualizar()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            //ahora agregamos todos los parámetros que solicita el SP de agregar
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Cedula", this.Cedula));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Nombre", this.Nombre));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Correo", this.Correo));

            Tools.Crypto MiEncriptador = new Tools.Crypto();
            string ContrasenniaEncriptada = MiEncriptador.EncriptarEnUnSentido(this.Contrasennia);
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Contrasennia", ContrasenniaEncriptada));

            MiCnn.ListaDeParametros.Add(new SqlParameter("@Telefono", this.Telefono));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Direccion", this.Direccion));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@UsuarioRolID", this.MiUsuarioRol.UsuarioRolID));

            MiCnn.ListaDeParametros.Add(new SqlParameter("@ID", this.UsuarioID));

            int resultado = MiCnn.EjecutarDML("SPUsuariosActualizar");

            if (resultado > 0) R = true;

            return R;
        }

        public bool Eliminar()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@ID", this.UsuarioID));

            int resultado = MiCnn.EjecutarDML("SPUsuariosEliminar");
            
            if (resultado > 0) R = true;

            return R;
        }

        public bool Activar()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@ID", this.UsuarioID));

            int resultado = MiCnn.EjecutarDML("SPUsuariosActivar");

            if (resultado > 0) R = true;

            return R;
        }

        public bool ConsultarPorID()
        {
            bool R = false;

            Conexion MyCnn = new Conexion();

            MyCnn.ListaDeParametros.Add(new SqlParameter("@ID", this.UsuarioID));

            DataTable DatosUsuario = new DataTable();

            DatosUsuario = MyCnn.EjecutarSelect("SPUsuariosConsultarPorID");

            if (DatosUsuario != null && DatosUsuario.Rows.Count > 0)
            {
                //el usuario existe
                R = true;
            }

            return R;
        }

        public Usuario ConsultarPorID(int IdUsuario)
        {
            Usuario R = new Usuario();

            //esta funcion retorna un objeto de tipo usuario con datos en los atributos. 
            //es una variedad de ConsultarPorID que me permite manipular el objeto y no
            //solo saber si el usuario existe o no a traves de un bool

            Conexion MyCnn = new Conexion();

            MyCnn.ListaDeParametros.Add(new SqlParameter("@ID", IdUsuario));

            DataTable DatosUsuario = new DataTable();

            DatosUsuario = MyCnn.EjecutarSelect("SPUsuariosConsultarPorID");

            if (DatosUsuario != null && DatosUsuario.Rows.Count > 0)
            {
                //como tenemos que llenar un objeto compuesto (por el rol de usuario) 
                //debemos extraer los datos de la consulta y llenar los atributos 
                //correspondientes del objeto de tipo Usuario R. 

                //acá capturamos los datos de la fila 0 del resultado 
                DataRow MiFila = DatosUsuario.Rows[0];

                R.UsuarioID = Convert.ToInt32(MiFila["UsuarioID"]);
                R.Nombre = Convert.ToString(MiFila["Nombre"]);
                R.Cedula = Convert.ToString(MiFila["Cedula"]);
                R.Correo = Convert.ToString(MiFila["Correo"]);
                R.Telefono = Convert.ToString(MiFila["Telefono"]);
                R.Contrasennia = Convert.ToString(MiFila["Contrasennia"]);
                R.Direccion = Convert.ToString(MiFila["Direccion"]);
                R.MiUsuarioRol.UsuarioRolID = Convert.ToInt32(MiFila["UsuarioRolID"]);
                R.MiUsuarioRol.Rol = Convert.ToString(MiFila["Rol"]);
                R.Activo = Convert.ToBoolean(MiFila["Activo"]);
            }

            return R;
        }

        public bool ConsultarPorCedula(string pCedula)
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@Cedula", pCedula));

            DataTable dt = new DataTable();

            dt = MiCnn.EjecutarSelect("SPUsuariosConsultarPorCedula");

            if (dt != null && dt.Rows.Count > 0) R = true;

            return R;
        }

        public bool ConsultarPorCorreo(string pCorreo)
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@Correo", pCorreo));

            DataTable dt = new DataTable();

            dt = MiCnn.EjecutarSelect("SPUsuariosConsultarPorCorreo");

            if (dt != null && dt.Rows.Count > 0) R = true;

            return R;
        }

        public DataTable ListarActivos(string pFiltro = "") 
        {
            DataTable R = new DataTable();

            //hay que hacer instancia de la clase conexion 

            Conexion MiCnn = new Conexion();
            //como el SP para listar requiere un parámetro, hay que agregarlo a la lista
            MiCnn.ListaDeParametros.Add(new SqlParameter("@VerActivos", true));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Filtro", pFiltro));

            R = MiCnn.EjecutarSelect("SPUsuariosListar");

            return R;
        }

        public DataTable ListarInactivos(string pFiltro = "")
        {
            DataTable R = new DataTable();

            //hay que hacer instancia de la clase conexion 

            Conexion MiCnn = new Conexion();
            //como el SP para listar requiere un parámetro, hay que agregarlo a la lista
            MiCnn.ListaDeParametros.Add(new SqlParameter("@VerActivos", false));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Filtro", pFiltro));

            R = MiCnn.EjecutarSelect("SPUsuariosListar");


            return R;
        }


        public int ValidarIngreso(string pUsuario, string pContrasennia)
        {
            int R = 0; 
            
            Conexion myCnn = new Conexion();

            Crypto myEncriptador = new Crypto();

            string PasswordEncriptado = myEncriptador.EncriptarEnUnSentido(pContrasennia);

            myCnn.ListaDeParametros.Add(new SqlParameter("@Usuario", pUsuario));
            myCnn.ListaDeParametros.Add(new SqlParameter("@Contrasennia", PasswordEncriptado));

            DataTable resultado = myCnn.EjecutarSelect("SPUsuariosValidarIngreso");

            if (resultado != null && resultado.Rows.Count > 0)
            {
                DataRow MiFila = resultado.Rows[0];

                R = Convert.ToInt32(MiFila["UsuarioID"]);
            }

            return R;
        }



    }
}
