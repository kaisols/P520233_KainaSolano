using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P520233_AllanDelgado.Formularios
{
    public partial class FrmProductosGestion : Form
    {
        //objeto local de tipo usuario. 
        private Logica.Models.Producto MiProductoLocal { get; set; }
        public FrmProductosGestion()
        {
            InitializeComponent();
            MiProductoLocal = new Logica.Models.Producto();
        }

        private void FrmProductosGestion_Load(object sender, EventArgs e)
        {
            MdiParent = Globales.ObjetosGlobales.MiFormularioPrincipal;

            CargarComboCategorias();

            CargarListaProductos(CbVerActivos.Checked);

            ActivarBotonAgregar();

        }

        private void ActivarBotonAgregar()
        {
            BtnAgregar.Enabled = true;
        }


        private void CargarComboCategorias()
        {
            Logica.Models.ProductoCategoria MiRol = new Logica.Models.ProductoCategoria();

            DataTable dt = new DataTable();

            dt = MiRol.Listar();

            if (dt != null && dt.Rows.Count > 0)
            {
                //una asegurado que el dt tiene valores, los "dibujo" en el combobox
                CboxCategoria.ValueMember = "id";
                CboxCategoria.DisplayMember = "Descripcion";
                CboxCategoria.DataSource = dt;
                CboxCategoria.SelectedIndex = -1;

            }

        }

        private void CargarListaProductos(bool VerActivos, string FiltroBusqueda = "")
        {
            Logica.Models.Producto miProducto = new Logica.Models.Producto();

            DataTable lista = new DataTable();

            if (VerActivos)
            {
                //si se quieren ver los usuarios activos
                lista = miProducto.ListarActivos(FiltroBusqueda);
                DgvListaProductos.DataSource = lista;
            }
            else
            {
                //Usuarios inactivos
                lista = miProducto.ListarInactivos(FiltroBusqueda);
                DgvListaProductos.DataSource = lista;
            }

        }


        private bool ValidarDatosRequeridos()
        {
            bool R = false;

            //validar que se hayan digitado valores en los campos obligatorios
            if (!string.IsNullOrEmpty(TxtProductoCodBarras.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtProductoNombre.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtProductoCosto.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtProductoPrecioUnitario.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtProductoSubTotal.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtProductoTasaImpuesto.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtProductoUtilidad.Text.Trim()) && 
                CboxCategoria.SelectedIndex > -1
                )
            {
                if (!Regex.IsMatch(TxtProductoCosto.Text.Trim(), "^[0-9]*\\.?[0-9]*$"))
                {
                    MessageBox.Show("Debe digitar el Costo, debe ser un Numero.", "Error de validación", MessageBoxButtons.OK);
                    return false;
                }

                if (!Regex.IsMatch(TxtProductoPrecioUnitario.Text.Trim(), "^[0-9]*\\.?[0-9]*$"))
                {
                    MessageBox.Show("Debe digitar el Precio Unitario, debe ser un Numero.", "Error de validación", MessageBoxButtons.OK);
                    return false;
                }

                if (!Regex.IsMatch(TxtProductoSubTotal.Text.Trim(), "^[0-9]*\\.?[0-9]*$"))
                {
                    MessageBox.Show("Debe digitar el SubTotal, debe ser un Numero.", "Error de validación", MessageBoxButtons.OK);
                    return false;
                }

                if (!Regex.IsMatch(TxtProductoUtilidad.Text.Trim(), "^[0-9]*\\.?[0-9]*$"))
                {
                    MessageBox.Show("Debe digitar la Utilidad, debe ser un Numero.", "Error de validación", MessageBoxButtons.OK);
                    return false;
                }

                if (!Regex.IsMatch(txtStock.Text.Trim(), "^[0-9]*\\.?[0-9]*$"))
                {
                    MessageBox.Show("Debe digitar el Stock, debe ser un Numero.", "Error de validación", MessageBoxButtons.OK);
                    return false;
                }

                if (!Regex.IsMatch(TxtProductoTasaImpuesto.Text.Trim(), "^[0-9]*\\.?[0-9]*$"))
                {
                    MessageBox.Show("Debe digitar la Tasa de Impuesto, debe ser un Numero.", "Error de validación", MessageBoxButtons.OK);
                    return false;
                }

                R = true;
            }
            else
            {
                //indicar al usuario qué validación está faltando

                //COD BARRAS
                if (string.IsNullOrEmpty(TxtProductoCodBarras.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar el Codigo de Barras", "Error de validación", MessageBoxButtons.OK);
                    return false;
                }

                //NOMBRE
                if (string.IsNullOrEmpty(TxtProductoNombre.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar el Nombre", "Error de validación", MessageBoxButtons.OK);
                    return false;
                }

                //COSTO
                if (string.IsNullOrEmpty(TxtProductoCosto.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar el Costo", "Error de validación", MessageBoxButtons.OK);
                    return false;
                }
                else
                {
                    if(!Regex.IsMatch(TxtProductoCosto.Text.Trim(), "^[0-9]*\\.?[0-9]*$"))
                    {
                        MessageBox.Show("Debe digitar el Costo, debe ser un Numero.", "Error de validación", MessageBoxButtons.OK);
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(TxtProductoPrecioUnitario.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar el Precio Unitario", "Error de validación", MessageBoxButtons.OK);
                    return false;
                }
                else
                {
                    if (!Regex.IsMatch(TxtProductoPrecioUnitario.Text.Trim(), "^[0-9]*\\.?[0-9]*$"))
                    {
                        MessageBox.Show("Debe digitar el Precio Unitario, debe ser un Numero.", "Error de validación", MessageBoxButtons.OK);
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(TxtProductoSubTotal.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar el SubTotal", "Error de validación", MessageBoxButtons.OK);
                    return false;
                }
                else
                {
                    if (!Regex.IsMatch(TxtProductoSubTotal.Text.Trim(), "^[0-9]*\\.?[0-9]*$"))
                    {
                        MessageBox.Show("Debe digitar el SubTotal, debe ser un Numero.", "Error de validación", MessageBoxButtons.OK);
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(TxtProductoUtilidad.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar la Utilidad", "Error de validación", MessageBoxButtons.OK);
                    return false;
                }
                else
                {
                    if (!Regex.IsMatch(TxtProductoUtilidad.Text.Trim(), "^[0-9]*\\.?[0-9]*$"))
                    {
                        MessageBox.Show("Debe digitar la Utilidad, debe ser un Numero.", "Error de validación", MessageBoxButtons.OK);
                        return false;
                    }
                }
                

                if (string.IsNullOrEmpty(txtStock.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar el Stock", "Error de validación", MessageBoxButtons.OK);
                    return false;
                }
                else
                {
                    if (!Regex.IsMatch(txtStock.Text.Trim(), "^[0-9]*\\.?[0-9]*$"))
                    {
                        MessageBox.Show("Debe digitar el Stock, debe ser un Numero.", "Error de validación", MessageBoxButtons.OK);
                        return false;
                    }
                }
                

                if (string.IsNullOrEmpty(TxtProductoTasaImpuesto.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar la Tasa de Impuesto", "Error de validación", MessageBoxButtons.OK);
                    return false;
                }
                else
                {
                    if (!Regex.IsMatch(TxtProductoTasaImpuesto.Text.Trim(), "^[0-9]*\\.?[0-9]*$"))
                    {
                        MessageBox.Show("Debe digitar la Tasa de Impuesto, debe ser un Numero.", "Error de validación", MessageBoxButtons.OK);
                        return false;
                    }
                }




                //ROL DE USUARIO
                if (CboxCategoria.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe Seleccionar la Categoria del Producto", "Error de validación", MessageBoxButtons.OK);
                    return false;
                }

            }

            return R;
        }


        private void BtnAgregar_Click(object sender, EventArgs e)
        { 
            if (ValidarDatosRequeridos())
            {
                MiProductoLocal = new Logica.Models.Producto();

                MiProductoLocal.CodigoBarras = TxtProductoCodBarras.Text.Trim();
                MiProductoLocal.NombreProducto = TxtProductoNombre.Text.Trim();
                MiProductoLocal.Costo = Convert.ToDecimal(TxtProductoCosto.Text.Trim()); 
                MiProductoLocal.CantidadStock = Convert.ToDecimal(txtStock.Text.Trim()); 
                MiProductoLocal.PrecioUnitario = Convert.ToDecimal(TxtProductoPrecioUnitario.Text.Trim()); 
                MiProductoLocal.SubTotal = Convert.ToDecimal(TxtProductoSubTotal.Text.Trim()); 
                MiProductoLocal.Utilidad = Convert.ToDecimal(TxtProductoUtilidad.Text.Trim()); 
                MiProductoLocal.TasaImpuesto = Convert.ToDecimal(TxtProductoTasaImpuesto.Text.Trim()); 
                MiProductoLocal.TasaImpuesto = Convert.ToDecimal(TxtProductoTasaImpuesto.Text.Trim()); 

                //con el combo de rol hay que extraer el valuemember seleccionado. 
                MiProductoLocal.MiCategoria.ProductoCategoriaID = Convert.ToInt32(CboxCategoria.SelectedValue); 

                bool CodBarrasOK = MiProductoLocal.ConsultarPorCodigoBarras(MiProductoLocal.CodigoBarras); 

                if (CodBarrasOK == false)
                {
                    //se solicita confirmación por parte del usuario 
                    string Pregunta = string.Format("¿Está seguro de agregar el Producto {0}?", MiProductoLocal.NombreProducto);
                    DialogResult respuesta = MessageBox.Show(Pregunta, "???", MessageBoxButtons.YesNo);

                    if (respuesta == DialogResult.Yes)
                    {
                        //procedemos a Agregar el usuario
                        bool ok = MiProductoLocal.Agregar();

                        if (ok)
                        {
                            MessageBox.Show("Producto ingresado correctamente!", ":)", MessageBoxButtons.OK);

                            LimpiarForm();
                            CargarListaProductos(CbVerActivos.Checked);

                        }
                        else
                        {
                            MessageBox.Show("El Producto no se pudo agregar...", ":(", MessageBoxButtons.OK);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("El Codigo de Barras ya existe...", ":(", MessageBoxButtons.OK);
                }
            }
        }


        void LimpiarForm()
        {
            txtStock.Text = string.Empty;
            TxtProductoCodBarras.Text = string.Empty;
            TxtProductoCodigo.Text = string.Empty;
            TxtProductoCosto.Text = string.Empty;
            TxtProductoNombre.Text = string.Empty;
            TxtProductoPrecioUnitario.Text = string.Empty;
            TxtProductoSubTotal.Text = string.Empty;
            TxtProductoTasaImpuesto.Text = string.Empty;
            TxtProductoUtilidad.Text = string.Empty;
            CboxCategoria.SelectedIndex = -1;
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            string Pregunta = string.Format("¿Está seguro de Limpiar la pantalla?");
            DialogResult respuesta = MessageBox.Show(Pregunta, "???", MessageBoxButtons.YesNo);

            if (respuesta == DialogResult.Yes)
            {
                LimpiarForm();
            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(TxtBuscar.Text.Trim()) && TxtBuscar.Text.Count() >= 3)
            {
                CargarListaProductos(CbVerActivos.Checked, TxtBuscar.Text.Trim());
            }
            else
            {
                CargarListaProductos(CbVerActivos.Checked);
            }
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
