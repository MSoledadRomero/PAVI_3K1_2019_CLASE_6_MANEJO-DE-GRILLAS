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
    public partial class frmDetalleBug : Form
    {
        public frmDetalleBug()
        {
            InitializeComponent();
        }

        internal void InicializarDetalleBug(string idBug)
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
                                      " WHERE id_bug = " + idBug.ToString());
            

            var resultado = DBHelper.GetDBHelper().ConsultaSQL(strSql);

            if (resultado.Rows.Count > 0)
            {
                var oBugSelected = resultado.Rows[0];
                txtNroBug.Text = oBugSelected["id_bug"].ToString();
                txtTitulo.Text = oBugSelected["titulo"].ToString();
                txtDescripcion.Text = oBugSelected["descripcion"].ToString();
                txtFechaAlta.Text = oBugSelected["fecha_alta"].ToString();
                txtProducto.Text = oBugSelected["producto"].ToString();
                txtPrioridad.Text = oBugSelected["prioridad"].ToString();
                txtCriticidad.Text = oBugSelected["criticidad"].ToString();
                txtEstado.Text = oBugSelected["estado"].ToString();
            }

            InicializarHistoricoBug(idBug);
        }


        internal void InicializarHistoricoBug(string idBug)
        {
            InitializeDataGridView();
            var strSql = String.Concat("SELECT historico.id_bug, ",
                                      "        historico.titulo,",
                                      "        historico.descripcion,",
                                      "        historico.fecha_historico,",
                                      "        responsable.usuario as responsable,  ",
                                      "        asignado.usuario as asignado, ",
                                      "        producto.nombre as producto, ",
                                      "        prioridad.nombre as prioridad, ",
                                      "        criticidad.nombre as criticidad, ",
                                      "        estado.nombre as estado",
                                      "   FROM BugsHistorico as historico",
                                      "   LEFT JOIN Usuarios as responsable ON responsable.id_usuario = historico.id_usuario_responsable",
                                      "   LEFT JOIN Usuarios as asignado ON asignado.id_usuario = historico.id_usuario_asignado",
                                      "  INNER JOIN Productos as producto ON producto.id_producto = historico.id_producto",
                                      "  INNER JOIN Prioridades as prioridad ON  prioridad.id_prioridad = historico.id_prioridad",
                                      "  INNER JOIN Criticidades as criticidad ON criticidad.id_criticidad = historico.id_criticidad",
                                      "  INNER JOIN Estados as estado ON estado.id_estado = historico.id_estado",
                                      "  WHERE id_bug = " + idBug.ToString());

            var resultado = DBHelper.GetDBHelper().ConsultaSQL(strSql);

            dgvHistoricoBug.DataSource = resultado;
        }

        private void InitializeDataGridView()
        {
            // Cree un DataGridView no vinculado declarando un recuento de columnas.
            dgvHistoricoBug.ColumnCount = 10;
            dgvHistoricoBug.ColumnHeadersVisible = true;

            // Configuramos la AutoGenerateColumns en false para que no se autogeneren las columnas
            dgvHistoricoBug.AutoGenerateColumns = false;

            // Cambia el estilo de la cabecera de la grilla.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            dgvHistoricoBug.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Definimos el nombre de la columnas y el DataPropertyName que se asocia a DataSource
            dgvHistoricoBug.Columns[0].Name = "ID";
            dgvHistoricoBug.Columns[0].DataPropertyName = "id_bug";
            // Definimos el ancho de la columna.
            dgvHistoricoBug.Columns[0].Width = 50;

            dgvHistoricoBug.Columns[1].Name = "Título";
            dgvHistoricoBug.Columns[1].DataPropertyName = "titulo";

            dgvHistoricoBug.Columns[2].Name = "Descripción";
            dgvHistoricoBug.Columns[2].DataPropertyName = "descripcion";

            dgvHistoricoBug.Columns[3].Name = "Responsable";
            dgvHistoricoBug.Columns[3].DataPropertyName = "responsable";

            dgvHistoricoBug.Columns[4].Name = "Asignado";
            dgvHistoricoBug.Columns[4].DataPropertyName = "asignado";

            dgvHistoricoBug.Columns[5].Name = "Prioridad";
            dgvHistoricoBug.Columns[5].DataPropertyName = "prioridad";

            dgvHistoricoBug.Columns[6].Name = "Criticidad";
            dgvHistoricoBug.Columns[6].DataPropertyName = "criticidad";

            dgvHistoricoBug.Columns[7].Name = "Producto";
            dgvHistoricoBug.Columns[7].DataPropertyName = "producto";

            dgvHistoricoBug.Columns[8].Name = "Fecha Histórico";
            dgvHistoricoBug.Columns[8].DataPropertyName = "fecha_historico";

            dgvHistoricoBug.Columns[9].Name = "Estado";
            dgvHistoricoBug.Columns[9].DataPropertyName = "estado";

            // Cambia el tamaño de la altura de los encabezados de columna.
            dgvHistoricoBug.AutoResizeColumnHeadersHeight();

            // Cambia el tamaño de todas las alturas de fila para ajustar el contenido de todas las celdas que no sean de encabezado.
            dgvHistoricoBug.AutoResizeRows(
                DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
