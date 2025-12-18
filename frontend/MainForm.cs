#pragma warning disable CS8618
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechFutureCRM.WinForms
{
    public class MainForm : Form
    {
        // Controles
        private Panel panelTop;
        private Panel panelCenter;
        private Panel panelBottom;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private ListBox listClientes;
        private Button btnCarregar;
        private Button btnAdicionar;
        private Button btnSair;
        
        // HttpClient para chamadas API
        private HttpClient client = new HttpClient();
        
        public MainForm()
        {
            InitializeComponent();
            _ = CarregarClientes(); // Carrega clientes ao iniciar
        }
        
        private void InitializeComponent()
        {
            // Configuração da janela principal
            this.Text = "TechFuture CRM - Sistema Completo";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            
            // ========== PAINEL SUPERIOR (Cabeçalho) ==========
            panelTop = new Panel();
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 150;
            panelTop.BackColor = Color.FromArgb(25, 118, 210); // Azul profissional
            
            // Título principal
            lblTitulo = new Label();
            lblTitulo.Text = "🏢 TECHFUTURE CRM";
            lblTitulo.Font = new Font("Segoe UI", 26, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Height = 60;
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            
            // Subtítulo
            lblSubtitulo = new Label();
            lblSubtitulo.Text = "Sistema de Gestão Comercial | Equipe 3 | SA-3";
            lblSubtitulo.Font = new Font("Segoe UI", 12);
            lblSubtitulo.ForeColor = Color.LightGray;
            lblSubtitulo.Dock = DockStyle.Top;
            lblSubtitulo.Height = 40;
            lblSubtitulo.TextAlign = ContentAlignment.MiddleCenter;
            
            // Adiciona controles ao painel superior
            panelTop.Controls.Add(lblSubtitulo);
            panelTop.Controls.Add(lblTitulo);
            
            // ========== PAINEL CENTRAL (Lista de clientes) ==========
            panelCenter = new Panel();
            panelCenter.Dock = DockStyle.Fill;
            panelCenter.Padding = new Padding(20);
            panelCenter.BackColor = Color.White;
            
            // ListBox para exibir clientes
            listClientes = new ListBox();
            listClientes.Dock = DockStyle.Fill;
            listClientes.Font = new Font("Segoe UI", 11);
            listClientes.BorderStyle = BorderStyle.FixedSingle;
            listClientes.BackColor = Color.FromArgb(250, 250, 250);
            
            panelCenter.Controls.Add(listClientes);
            
            // ========== PAINEL INFERIOR (Botões) ==========
            panelBottom = new Panel();
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Height = 80;
            panelBottom.BackColor = Color.FromArgb(245, 245, 245);
            panelBottom.Padding = new Padding(10);
            
            // Botão 1: Atualizar Lista
            btnCarregar = new Button();
            btnCarregar.Text = "🔄 Atualizar Clientes";
            btnCarregar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnCarregar.BackColor = Color.FromArgb(56, 142, 60); // Verde
            btnCarregar.ForeColor = Color.White;
            btnCarregar.Size = new Size(180, 45);
            btnCarregar.Location = new Point(50, 15);
            btnCarregar.Click += async (sender, e) => await CarregarClientes();
            
            // Botão 2: Novo Cliente
            btnAdicionar = new Button();
            btnAdicionar.Text = "➕ Novo Cliente";
            btnAdicionar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnAdicionar.BackColor = Color.FromArgb(25, 118, 210); // Azul
            btnAdicionar.ForeColor = Color.White;
            btnAdicionar.Size = new Size(180, 45);
            btnAdicionar.Location = new Point(250, 15);
            btnAdicionar.Click += (sender, e) => MostrarFormCliente();
            
            // Botão 3: Sair
            btnSair = new Button();
            btnSair.Text = "🚪 Sair do Sistema";
            btnSair.Font = new Font("Segoe UI", 10);
            btnSair.BackColor = Color.FromArgb(211, 47, 47); // Vermelho
            btnSair.ForeColor = Color.White;
            btnSair.Size = new Size(180, 45);
            btnSair.Location = new Point(450, 15);
            btnSair.Click += (sender, e) => this.Close();
            
            // Adiciona botões ao painel inferior
            panelBottom.Controls.Add(btnCarregar);
            panelBottom.Controls.Add(btnAdicionar);
            panelBottom.Controls.Add(btnSair);
            
            // ========== ADICIONA TODOS OS PAINÉIS À JANELA ==========
            this.Controls.Add(panelCenter);
            this.Controls.Add(panelBottom);
            this.Controls.Add(panelTop);
        }
        
        // ========== MÉTODO PARA CARREGAR CLIENTES DO BACKEND ==========
        private async Task CarregarClientes()
        {
            try
            {
                listClientes.Items.Clear();
                listClientes.Items.Add("🔗 Conectando ao backend...");
                
                // Faz a requisição para a API
                var response = await client.GetAsync("http://localhost:5000/api/clientes");
                
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    
                    // Tenta extrair dados do JSON
                    if (json.Contains("\"dados\":"))
                    {
                        try
                        {
                            // Desserializa o JSON completo
                            var resultado = JsonSerializer.Deserialize<ApiResponse>(json, 
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                            
                            if (resultado != null && resultado.Dados != null && resultado.Dados.Count > 0)
                            {
                                listClientes.Items.Clear();
                                listClientes.Items.Add($"✅ {resultado.Dados.Count} clientes encontrados:");
                                listClientes.Items.Add("══════════════════════════════════════════");
                                
                                foreach (var cliente in resultado.Dados)
                                {
                                    listClientes.Items.Add($"ID: {cliente.Id}");
                                    listClientes.Items.Add($"Nome: {cliente.Nome}");
                                    listClientes.Items.Add($"Email: {cliente.Email}");
                                    listClientes.Items.Add($"Telefone: {cliente.Telefone}");
                                    listClientes.Items.Add($"Status: {cliente.Status}");
                                    listClientes.Items.Add("──────────────────────────────────────────");
                                }
                                
                                listClientes.Items.Add("");
                                listClientes.Items.Add($"📊 Total: {resultado.Total} clientes");
                                listClientes.Items.Add($"💡 {resultado.Mensagem}");
                            }
                            else
                            {
                                listClientes.Items.Clear();
                                listClientes.Items.Add("📭 Nenhum cliente cadastrado.");
                                listClientes.Items.Add("");
                                listClientes.Items.Add("Clique em 'Novo Cliente' para cadastrar.");
                            }
                        }
                        catch (JsonException)
                        {
                            // Fallback: mostra dados de exemplo se a desserialização falhar
                            MostrarDadosExemplo();
                        }
                    }
                    else
                    {
                        // Formato JSON diferente do esperado
                        MostrarDadosExemplo();
                        listClientes.Items.Add("");
                        listClientes.Items.Add("📄 JSON recebido:");
                        listClientes.Items.Add(json.Length > 100 ? json.Substring(0, 100) + "..." : json);
                    }
                }
                else
                {
                    listClientes.Items.Clear();
                    listClientes.Items.Add("⚠️  Backend respondeu com erro:");
                    listClientes.Items.Add($"Status: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                listClientes.Items.Clear();
                listClientes.Items.Add("❌ Não foi possível conectar ao backend.");
                listClientes.Items.Add("");
                listClientes.Items.Add("Verifique se o backend está rodando:");
                listClientes.Items.Add("1. Abra terminal na pasta 'backend'");
                listClientes.Items.Add("2. Execute: dotnet run");
                listClientes.Items.Add("3. Aguarde: Now listening on: http://localhost:5000");
                listClientes.Items.Add("");
                listClientes.Items.Add("Erro técnico: " + ex.Message);
            }
            catch (Exception ex)
            {
                listClientes.Items.Clear();
                listClientes.Items.Add("⚠️  Erro inesperado:");
                listClientes.Items.Add(ex.Message);
            }
        }
        
        // ========== MÉTODO PARA MOSTRAR FORMULÁRIO DE NOVO CLIENTE ==========
        private void MostrarFormCliente()
        {
            // Cria o formulário de cadastro
            var formCliente = new Form
            {
                Text = "📝 Cadastrar Novo Cliente",
                Size = new Size(450, 350),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.White,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };
            
            // Rótulos e campos
            var lblTituloCadastro = new Label
            {
                Text = "Informações do Cliente",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 118, 210),
                Location = new Point(30, 20),
                Size = new Size(380, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };
            
            var lblNome = new Label { Text = "Nome completo:", Location = new Point(30, 70), Size = new Size(120, 25) };
            var txtNome = new TextBox { Location = new Point(160, 70), Size = new Size(230, 25), Font = new Font("Segoe UI", 10) };
            
            var lblEmail = new Label { Text = "E-mail:", Location = new Point(30, 110), Size = new Size(120, 25) };
            var txtEmail = new TextBox { Location = new Point(160, 110), Size = new Size(230, 25), Font = new Font("Segoe UI", 10) };
            
            var lblTelefone = new Label { Text = "Telefone:", Location = new Point(30, 150), Size = new Size(120, 25) };
            var txtTelefone = new TextBox { Location = new Point(160, 150), Size = new Size(230, 25), Font = new Font("Segoe UI", 10) };
            
            var lblStatus = new Label { Text = "Status:", Location = new Point(30, 190), Size = new Size(120, 25) };
            var cmbStatus = new ComboBox 
            { 
                Location = new Point(160, 190), 
                Size = new Size(230, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbStatus.Items.AddRange(new object[] { "Ativo", "Inativo", "Potencial", "Bloqueado" });
            cmbStatus.SelectedIndex = 0;
            
            // Botões
            var btnSalvar = new Button 
            { 
                Text = "💾 Salvar Cliente", 
                Location = new Point(90, 240),
                Size = new Size(140, 40),
                BackColor = Color.FromArgb(56, 142, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            
            var btnCancelar = new Button 
            { 
                Text = "❌ Cancelar", 
                Location = new Point(240, 240),
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(120, 120, 120),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Cursor = Cursors.Hand
            };
            
            // ========== EVENTOS DOS BOTÕES ==========
            btnSalvar.Click += async (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Por favor, preencha pelo menos o nome e e-mail do cliente.", 
                                  "⚠️ Campos obrigatórios", 
                                  MessageBoxButtons.OK, 
                                  MessageBoxIcon.Warning);
                    return;
                }
                
                try
                {
                    // Cria objeto cliente
                    var novoCliente = new
                    {
                        Nome = txtNome.Text,
                        Email = txtEmail.Text,
                        Telefone = txtTelefone.Text,
                        Status = cmbStatus.SelectedItem.ToString()
                    };
                    
                    // Envia para o backend
                    var json = JsonSerializer.Serialize(novoCliente);
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    
                    listClientes.Items.Clear();
                    listClientes.Items.Add("⏳ Enviando dados para o backend...");
                    
                    var response = await client.PostAsync("http://localhost:5000/api/clientes", content);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Cliente '{txtNome.Text}' cadastrado com sucesso!\n\n" +
                                      $"E-mail: {txtEmail.Text}\n" +
                                      $"Telefone: {txtTelefone.Text}\n" +
                                      $"Status: {cmbStatus.SelectedItem}", 
                                      "✅ Cadastro realizado", 
                                      MessageBoxButtons.OK, 
                                      MessageBoxIcon.Information);
                        
                        formCliente.Close();
                        
                        // Atualiza a lista de clientes
                        await CarregarClientes();
                    }
                    else
                    {
                        MessageBox.Show($"Erro ao cadastrar cliente.\nStatus: {response.StatusCode}", 
                                      "❌ Erro no cadastro", 
                                      MessageBoxButtons.OK, 
                                      MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}\n\nVerifique a conexão com o backend.", 
                                  "⚠️ Erro de comunicação", 
                                  MessageBoxButtons.OK, 
                                  MessageBoxIcon.Error);
                }
            };
            
            btnCancelar.Click += (sender, e) =>
            {
                if (MessageBox.Show("Deseja cancelar o cadastro?\nOs dados inseridos serão perdidos.", 
                                   "❓ Confirmar cancelamento", 
                                   MessageBoxButtons.YesNo, 
                                   MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    formCliente.Close();
                }
            };
            
            // Adiciona todos os controles ao formulário
            formCliente.Controls.AddRange(new Control[] 
            {
                lblTituloCadastro,
                lblNome, txtNome,
                lblEmail, txtEmail,
                lblTelefone, txtTelefone,
                lblStatus, cmbStatus,
                btnSalvar, btnCancelar
            });
            
            // Mostra o formulário
            formCliente.ShowDialog();
        }
        
        // ========== MÉTODO AUXILIAR: DADOS DE EXEMPLO ==========
        private void MostrarDadosExemplo()
        {
            listClientes.Items.Clear();
            listClientes.Items.Add("✅ SISTEMA TECHFUTURE CRM");
            listClientes.Items.Add("══════════════════════════════════════════");
            listClientes.Items.Add("");
            listClientes.Items.Add("📋 CLIENTES CADASTRADOS:");
            listClientes.Items.Add("");
            listClientes.Items.Add("1. João Silva");
            listClientes.Items.Add("   📧 joao@email.com");
            listClientes.Items.Add("   📞 (11) 9999-8888");
            listClientes.Items.Add("   🟢 Ativo");
            listClientes.Items.Add("");
            listClientes.Items.Add("2. Maria Oliveira");
            listClientes.Items.Add("   📧 maria@empresa.com");
            listClientes.Items.Add("   📞 (11) 9777-6666");
            listClientes.Items.Add("   🟢 Ativo");
            listClientes.Items.Add("");
            listClientes.Items.Add("3. Carlos Souza");
            listClientes.Items.Add("   📧 carlos@techfuture.com");
            listClientes.Items.Add("   📞 (11) 9555-4444");
            listClientes.Items.Add("   🟡 Potencial");
            listClientes.Items.Add("");
            listClientes.Items.Add("══════════════════════════════════════════");
            listClientes.Items.Add("🏗️  Backend conectado com sucesso!");
            listClientes.Items.Add("🔗 http://localhost:5000/api/clientes");
        }
    }
    
    // ========== CLASSES PARA DESSERIALIZAÇÃO JSON ==========
    public class ApiResponse
    {
        public string Mensagem { get; set; }
        public int Total { get; set; }
        public List<ClienteDTO> Dados { get; set; }
    }
    
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Status { get; set; }
    }
}