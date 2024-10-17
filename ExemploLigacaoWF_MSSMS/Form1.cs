using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExemploLigacaoWF_MSSMS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string connectionString = "Data Source=LEONARDO\\SQLEXPRESS;Initial Catalog=ContatosDB;Integrated Security=True";
        private void btnGravar_Click(object sender, EventArgs e)
        {
            string nome = txtnome.Text;
            string email = txtemail.Text;
            int numeroTlfn = Convert.ToInt32(txttelefone.Text);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Contatos (ct_Id, ct_nome, ct_email, ct_telefone) VALUES (@ct_Id, @ct_nome, @ct_email, @ct_telefone)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                   
                    command.Parameters.AddWithValue("@ct_nome", nome);
                    command.Parameters.AddWithValue("@ct_email", email);
                    command.Parameters.AddWithValue("@ct_telefone", numeroTlfn);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registo efetuado com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show(nome, ", erro ao efetuar registo.",
                            (MessageBoxButtons)MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRegistodeUtilizadores_Click(object sender, EventArgs e)
        {     
            string query = "SELECT * FROM Contatos";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                dataAdapter.Fill(dataTable);

                dgvContatos.DataSource = dataTable;
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Contatos";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                dataAdapter.Fill(dataTable);

                dgvContatos.DataSource = dataTable;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvContatos.SelectedRows.Count > 0)
            {
                int idRowSelected = Convert.ToInt32(dgvContatos.SelectedRows[0].Cells["ct_Id"].Value);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Contatos WHERE ct_Id = @ct_Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ct_Id", idRowSelected);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            MessageBox.Show("\tContato eliminado com sucesso!");
                        else
                            MessageBox.Show("\tNão foi possivel eliminar o contato.");
                    }
                }
            }
        }

        private void dgvContatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvContatos.Rows[e.RowIndex];

                txtnome.Text = row.Cells["ct_nome"].Value.ToString();
                txtemail.Text = row.Cells["ct_nome"].Value.ToString();
                txttelefone.Text = row.Cells["ct_nome"].Value.ToString();
            }

        }
    }
}
