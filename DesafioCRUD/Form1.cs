﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DesafioCRUD
{
    public partial class Form1 : Form
    {
        private readonly string connectionString = "Data Source=10.0.0.2,214;Initial Catalog=Integrator_DEV;User ID=Vjesus;Password=Zac25715;";



        public Form1()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO usuarios (Nome, Idade, CPF, Sexo) VALUES (@Nome, @Idade, @CPF, @Sexo)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Adicione parâmetros para evitar injeção de SQL
                        string nome = txtNome.Text;
                        int idade = Convert.ToInt32(txtIdade.Text);
                        string cpf = txtCPF.Text;
                        string sexo = txtSexo.Text;

                        command.Parameters.AddWithValue("@Nome", nome);
                        command.Parameters.AddWithValue("@Idade", idade);
                        command.Parameters.AddWithValue("@CPF", cpf);
                        command.Parameters.AddWithValue("@Sexo", sexo);

                        // Execute a instrução SQL
                        command.ExecuteNonQuery();

                        MessageBox.Show("Dados inseridos com sucesso!");
                        LimparTextBoxes();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir dados: " + ex.Message);
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query;
                    connection.Open();

                    if (txtConsulta.Text == "")
                    {
                        query = "SELECT * FROM usuarios";
                    }
                    else
                    {
                        query = $"select * from usuarios where nome like '%{txtConsulta.Text}%'";
                    }
                    


                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                // Limpe as colunas existentes no DataGridView
                                gridUsers.Columns.Clear();

                                // Vincule os dados ao DataGridView
                                gridUsers.DataSource = dataTable;
                            }
                        }
                    }
                
                catch (Exception ex)
                {

                    MessageBox.Show("Erro ao mostrar dados: " + ex.Message);
                }
            } 
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    int idUser = Convert.ToInt32(txtExcluir.Text);
                    string query = $"delete from usuarios where id = {idUser}";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();

                        MessageBox.Show("Usuario excluido com sucesso!");
                        LimparTextBoxes();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao deletar usuario: " + ex.Message);
                }
            }
        }

        private void LimparTextBoxes()
        {
            txtNome.Text = "";
            txtIdade.Text = "";
            txtCPF.Text = "";
            txtSexo.Text = "";
            txtExcluir.Text = "";
            txtConsulta.Text = "";
        }
    }
    }

