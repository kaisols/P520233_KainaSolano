﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P520233_AllanDelgado.Globales
{
    public static class ObjetosGlobales
    {
        //definir un objeto global para el form principal 
        public static Form MiFormularioPrincipal = new Formularios.FrmPrincipal();

        public static Formularios.FrmUsuariosGestion 
            MiFormularioDeGestionDeUsuarios = new Formularios.FrmUsuariosGestion();

        //este sera el usuario validado en el login tendra un scope global  en toda la aplicacion 

        public static Logica.Models.Usuario MiUsuarioGlobal = new Logica.Models.Usuario();

    }
}
