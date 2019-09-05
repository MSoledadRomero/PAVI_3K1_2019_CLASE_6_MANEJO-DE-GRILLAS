using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BugTracker
{
    public partial class frmConsultaBugs : Form
    {
        public frmConsultaBugs()
        {
            InitializeComponent();
            // Inicializamos la grilla de bugs
            InitializeDataGridView();
        }

        private void frmBugs_Load(object sender, EventArgs e)
        {

            //LLenar combos y limpiar grid
            LlenarCombo(cboEstados, DBHelper.GetDBHelper().ConsultaSQL("Select * from Estados"), "nombre", "id_estado");

            LlenarCombo(cboPrioridades, DBHelper.GetDBHelper().ConsultaSQL("Select * from Prioridades"), "nombre", "id_prioridad");

            LlenarCombo(cboCriticidades, DBHelper.GetDBHelper().ConsultaSQL("Select * from Criticidades"), "nombre", "id_criticidad");

            LlenarCombo(cboAsignadoA, DBHelper.GetDBHelper().ConsultaSQL("Select * from Usuarios"), "usuario", "id_usuario");

            LlenarCombo(cboProductos, DBHelper.GetDBHelper().ConsultaSQL("Select * from Productos"), "nombre", "id_producto");

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            var strSql = String.Concat("SELECT B.id_bug, ",
                                      "        B.titulo,",
                                      "        B.descripcion,",
                                      "        B.fecha_alta,",
                                      "        U.usuario as responsable,  ",
                                      "        U.usuario as asignado, ",
                                      "        Pro.nombre as producto, ",
                                      "        Pri.nombre as prioridad, ",
                                      "        Cri.nombre as criticidad, ",
                                      "        E.nombre as estado",
                                      "   FROM Bugs B",
                                      "   LEFT JOIN Usuarios U ON U.id_usuario = B.id_usuario_responsable",
                                      "   LEFT JOIN Usuarios ON U.id_usuario = B.id_usuario_asignado",
                                      "  INNER JOIN Productos Pro ON Pro.id_producto = B.id_producto",
                                      "  INNER JOIN Prioridades Pri ON  Pri.id_prioridad = B.id_prioridad",
                                      "  INNER JOIN Criticidades Cri ON  Cri.id_criticidad = B.id_criticidad",
                                      "  INNER JOIN Estados E ON E.id_estado = B.id_estado",
                                      "  WHERE 1=1");

            // Dictionary: Representa una colección de claves y valores.
            // Dictionary: Solo se usa si pasamos por parámetro los filtros de la selección de los combos
            Dictionary<string, object> parametros = new Dictionary<string, object>();

                     
           DateTime fechaDesde;
            DateTime fechaHasta;
            if (DateTime.TryParse(txtFechaDesde.Text, out fechaDesde) &&
                DateTime.TryParse(txtFechaHasta.Text, out fechaHasta))
            {
                //strSql += " AND (B.fecha_alta>=@fechaDesde AND B.fecha_alta<=@fechaHasta) ";
                strSql += " AND (B.fecha_alta>=" + txtFechaDesde.Text + " AND B.fecha_alta<=" + txtFechaHasta.Text + ")";
                //parametros.Add("fechaDesde", txtFechaDesde.Text);
                //parametros.Add("fechaHasta", txtFechaHasta.Text);
            }

            if (!string.IsNullOrEmpty(cboEstados.Text))
            {
                var idEstado = cboEstados.SelectedValue.ToString();
                //strSql += "AND (E.id_estado=@idEstado) ";
                strSql += "AND (E.id_estado=" + idEstado + ") ";

                //parametros.Add("idEstado", idEstado);
            }

              if (!string.IsNullOrEmpty(cboAsignadoA.Text))
            {
                var asignadoA = cboAsignadoA.SelectedValue.ToString();
                //strSql += "AND (B.id_usuario_asignado=@idUsuarioAsignado) ";
                strSql += "AND (B.id_usuario_asignado=" + asignadoA + ") ";
                //parametros.Add("idUsuarioAsignado", asignadoA);
            }
            
           
            if (!string.IsNullOrEmpty(cboPrioridades.Text))
            {
                var prioridad = cboPrioridades.SelectedValue.ToString();
              //  strSql += "AND (B.id_prioridad=@idPrioridad) ";
                strSql += "AND (B.id_prioridad=" + prioridad + ") ";
                //parametros.Add("idCriticidad", prioridad);
            }

            if (!string.IsNullOrEmpty(cboCriticidades.Text))
            {
                var criticidad = cboCriticidades.SelectedValue.ToString();
                //strSql += "AND (B.id_criticidad=@idCriticidad) ";
                strSql += "AND (B.id_criticidad=" + criticidad + ") ";
                //parametros.Add("idCriticidad", criticidad);
            }

            if (!string.IsNullOrEmpty(cboProductos.Text))
            {
                var producto = cboProductos.SelectedValue.ToString();
                //strSql += "AND (B.id_producto=@idProducto) ";
                strSql += "AND (B.id_producto=" + producto + ") ";
                //parametros.Add("idProducto", producto);
            }


            strSql += " ORDER BY B.fecha_alta DESC";
            //Consulta sin parametros
            dgvBugs.DataSource = DBHelper.GetDBHelper().ConsultaSQL(strSql);

            //Consulta con parametros
           // dgvBugs.DataSource = DBHelper.GetDBHelper().ConsultaSQLConParametros(strSql, parametros);
            if (dgvBugs.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron coincidencias para el/los filtros ingresados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void LlenarCombo(ComboBox cbo, Object source, string display, String value)
        {
            // Datasource: establece el origen de datos de este objeto.
            cbo.DataSource = source;
            // DisplayMember: establece la propiedad que se va a mostrar para este ListControl.
            cbo.DisplayMember = display;
            // ValueMember: establece la ruta de acceso de la propiedad que se utilizará como valor real para los elementos de ListControl.
            cbo.ValueMember = value;
            //SelectedIndex: establece el índice que especifica el elemento seleccionado actualmente.
            cbo.SelectedIndex = -1;
        }


        private void InitializeDataGridView()
        {
            // Cree un DataGridView no vinculado declarando un recuento de columnas.
            dgvBugs.ColumnCount = 10;
            dgvBugs.ColumnHeadersVisible = true;

            // Configuramos la AutoGenerateColumns en false para que no se autogeneren las columnas
            dgvBugs.AutoGenerateColumns = false;

            // Cambia el estilo de la cabecera de la grilla.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            dgvBugs.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Definimos el nombre de la columnas y el DataPropertyName que se asocia a DataSource
            dgvBugs.Columns[0].Name = "ID";
            dgvBugs.Columns[0].DataPropertyName = "id_bug";
            // Definimos el ancho de la columna.
            dgvBugs.Columns[0].Width = 50;

            dgvBugs.Columns[1].Name = "Título";
            dgvBugs.Columns[1].DataPropertyName = "titulo";
            
            dgvBugs.Columns[2].Name = "Descripción";
            dgvBugs.Columns[2].DataPropertyName = "descripcion";

            dgvBugs.Columns[3].Name = "Responsable";
            dgvBugs.Columns[3].DataPropertyName = "responsable";

            dgvBugs.Columns[4].Name = "Asignado";
            dgvBugs.Columns[4].DataPropertyName = "asignado";
            
            dgvBugs.Columns[5].Name = "Prioridad";
            dgvBugs.Columns[5].DataPropertyName = "prioridad";
            
            dgvBugs.Columns[6].Name = "Criticidad";
            dgvBugs.Columns[6].DataPropertyName = "criticidad";
            
            dgvBugs.Columns[7].Name = "Producto";
            dgvBugs.Columns[7].DataPropertyName = "producto";

            dgvBugs.Columns[8].Name = "Fecha Alta";
            dgvBugs.Columns[8].DataPropertyName = "fecha_alta";

            dgvBugs.Columns[9].Name = "Estado";
            dgvBugs.Columns[9].DataPropertyName = "estado";

            // Cambia el tamaño de la altura de los encabezados de columna.
            dgvBugs.AutoResizeColumnHeadersHeight();

            // Cambia el tamaño de todas las alturas de fila para ajustar el contenido de todas las celdas que no sean de encabezado.
            dgvBugs.AutoResizeRows(
                DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
        }

        private void btnDetalleBug_Click(object sender, EventArgs e)
        {
            if (dgvBugs.CurrentRow != null)
            {
                frmDetalleBug frmDetalle = new frmDetalleBug();
                var selectedItem = (DataRowView)dgvBugs.CurrentRow.DataBoundItem;
                frmDetalle.InicializarDetalleBug(selectedItem["id_bug"].ToString());
                frmDetalle.ShowDialog();
            }
        }

        private void dgvBugs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Cuando seleccionamos una fila de la grilla habilitamos el boton btnDetalleBug.
            btnDetalleBug.Enabled = true;
        }
    }
}
