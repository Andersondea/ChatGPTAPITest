using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace ChatGPTTeste
{
    public partial class ConfigWindow : Window
    {
        private IConfiguration Configuration { get; set; }

        public ConfigWindow()
        {
            InitializeComponent();

            // Carrega as configurações atuais
            CarregarConfiguracoes();

            // Eventos para mostrar ou ocultar configurações de proxy
            chkUsarProxy.Checked += ChkUsarProxy_Checked;
            chkUsarProxy.Unchecked += ChkUsarProxy_Unchecked;
        }

        private void ChkUsarProxy_Checked(object sender, RoutedEventArgs e)
        {
            stackProxy.Visibility = Visibility.Visible;
        }

        private void ChkUsarProxy_Unchecked(object sender, RoutedEventArgs e)
        {
            stackProxy.Visibility = Visibility.Collapsed;
        }

        private void CarregarConfiguracoes()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            // Carrega as configurações
            txtApiKey.Password = Configuration["OpenAI:ApiKey"] ?? "";

            bool usarProxy = bool.Parse(Configuration["Proxy:UseProxy"] ?? "false");
            chkUsarProxy.IsChecked = usarProxy;
            stackProxy.Visibility = usarProxy ? Visibility.Visible : Visibility.Collapsed;

            txtProxyAddress.Text = Configuration["Proxy:Address"] ?? "";
            txtProxyUser.Text = Configuration["Proxy:User"] ?? "";
            txtProxyPassword.Password = Configuration["Proxy:Password"] ?? "";
            txtProxyDomain.Text = Configuration["Proxy:Domain"] ?? "";
        }


        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            var configFilePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            dynamic config;
            if (File.Exists(configFilePath))
            {
                config = JsonConvert.DeserializeObject(File.ReadAllText(configFilePath)) ?? new JObject();
            }
            else
            {
                config = new JObject();
            }

            // Garante que as seções existem
            config.OpenAI = config.OpenAI ?? new JObject();
            config.Proxy = config.Proxy ?? new JObject();

            // Atualiza os valores
            config.OpenAI.ApiKey = txtApiKey.Password;
            config.Proxy.UseProxy = chkUsarProxy.IsChecked ?? false;
            config.Proxy.Address = txtProxyAddress.Text;
            config.Proxy.User = txtProxyUser.Text;
            config.Proxy.Password = txtProxyPassword.Password;
            config.Proxy.Domain = txtProxyDomain.Text;

            // Salva o arquivo de configuração
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(configFilePath, json);

            // Aplica as mudanças imediatamente
            if (Owner is MainWindow mainWindow)
            {
                mainWindow.AtualizarConfiguracoes();
            }

            MessageBox.Show("Configurações salvas com sucesso!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);

            Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
