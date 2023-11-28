﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P520233_AllanDelgado.Formularios
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void gestiónDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //En este caso quiero que la ventana se muestre solo una vez
            //en la aplicación (que no se abran varias veces). Para esto
            //hay que revisar si la ventana está o no visible 

            if (!Globales.ObjetosGlobales.MiFormularioDeGestionDeUsuarios.Visible)
            {
                //hago una reinstancia del objeto para asegurar que iniciamos en limpio
                Globales.ObjetosGlobales.MiFormularioDeGestionDeUsuarios = new FrmUsuariosGestion();

                Globales.ObjetosGlobales.MiFormularioDeGestionDeUsuarios.Show();
            }


        }

        private void FrmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            LblUsuario.Text = Globales.ObjetosGlobales.MiUsuarioGlobal.Nombre + "(" +
                              Globales.ObjetosGlobales.MiUsuarioGlobal.MiUsuarioRol.Rol + ")";

            //ahora se debe ajustar los permisos de menús para que se muestren o no, dependiendo 
            //del tipo de rol

            switch (Globales.ObjetosGlobales.MiUsuarioGlobal.MiUsuarioRol.UsuarioRolID)
            {
                //admin
                case 1:
                    //como admin tiene acceso a todo, no es necesario ocultar opciones de menu
                    break;

                //empleado
                case 2:
                    //ocultan los menús correspondientes 
                    MnuGestionUsuarios.Enabled = false; 
                    MnuGestionProductos.Enabled = false;
                    MnuGestionCategorias.Enabled = false;
                    break;

                default:
                    break;
            }

        }

        private void entradasYSalidasDeInventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Globales.ObjetosGlobales.MiFormularioMovimientos.Visible)
            {
                Globales.ObjetosGlobales.MiFormularioMovimientos = new FrmMovimientosInventario();
                Globales.ObjetosGlobales.MiFormularioMovimientos.Show();
            }
        }
    }
}
