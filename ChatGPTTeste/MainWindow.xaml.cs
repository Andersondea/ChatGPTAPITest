//#define USE_PROXY

using MahApps.Metro.Controls;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatGPTTeste
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string apiKey = "secret_key";  // Substitua com sua chave da OpenAI API
        private static readonly string apiUrl = "https://api.openai.com/v1/chat/completions";
        private static readonly int maxRetryCount = 5;  // Número máximo de retentativas

        private const double FonteMinima = 10;
        private const double FonteMaxima = 24;

        // Propriedade para binding do tamanho da fonte
        public double FonteResultado { get; set; } = 12;

        public MainWindow()
        {
            InitializeComponent();
            AtualizarEstadoBotoesFonte();
        }

        // Evento do botão Ok adaptado
        private async void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            string prompt = txtPrompt.Text;

            if (!string.IsNullOrWhiteSpace(prompt))
            {
                // Mostrar o indicador de progresso
                progressRing.IsActive = true;
                progressRing.Visibility = Visibility.Visible;
                btnOk.IsEnabled = false;

                try
                {
                    // Chama a API e exibe o resultado
                    string resultado = await GetChatGptResponse(prompt);
                    txtResultado.Text = resultado;
                }
                catch (Exception ex)
                {
                    // Trate qualquer exceção que possa ocorrer durante a chamada à API
                    MessageBox.Show($"Ocorreu um erro: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    // Ocultar o indicador de progresso
                    progressRing.IsActive = false;
                    progressRing.Visibility = Visibility.Collapsed;
                    btnOk.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("Por favor, insira um prompt.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Função para fazer a chamada à API do ChatGPT com política de retentativa
        private static async Task<string> GetChatGptResponse(string prompt)
        {
#if USE_PROXY
            var proxy = new WebProxy("HTTP_PROXY", true)
            {
                Credentials = new NetworkCredential("USER", "PASSWORD", "DOMAIN")
            };

            var httpClientHandler = new HttpClientHandler
            {
                Proxy = proxy,
                UseProxy = true
            };
#else
            var httpClientHandler = new HttpClientHandler();
#endif

            using (HttpClient client = new HttpClient(httpClientHandler))
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "system", content = "Você é um assistente útil." },
                        new { role = "user", content = prompt }
                    }
                };

                var jsonBody = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                int retryCount = 0;

                while (retryCount < maxRetryCount)
                {
                    var response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(responseContent))
                        {
                            dynamic? result = JsonConvert.DeserializeObject(responseContent);

                            if (result?.choices != null && result.choices.Count > 0)
                            {
                                string chatGptResponse = result.choices[0].message.content;
                                return chatGptResponse;
                            }
                        }
                        return "Erro: A resposta da API não contém dados válidos.";
                    }
                    else if (response.StatusCode == (HttpStatusCode)429)  // TooManyRequests
                    {
                        // Retentativa após erro de muitas requisições
                        var retryAfter = response.Headers.RetryAfter?.Delta ?? TimeSpan.FromSeconds(10);
                        await Task.Delay(retryAfter);  // Aguarda o tempo indicado pelo cabeçalho Retry-After
                        retryCount++;
                    }
                    else
                    {
                        return $"Erro: {response.StatusCode}";
                    }
                }

                return "Erro: Limite de tentativas excedido. Tente novamente mais tarde.";
            }
        }

        private void BtnAumentarFonte_Click(object sender, RoutedEventArgs e)
        {
            if (FonteResultado < FonteMaxima)
            {
                FonteResultado += 1;
                AtualizarFonte();
            }
        }

        private void BtnDiminuirFonte_Click(object sender, RoutedEventArgs e)
        {
            if (FonteResultado > FonteMinima)
            {
                FonteResultado -= 1;
                AtualizarFonte();
            }
        }

        // Evento do botão Copiar
        private void BtnCopiar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtResultado.Text))
            {
                Clipboard.SetText(txtResultado.Text);
                MessageBox.Show("Resposta copiada para a área de transferência.", "Copiar", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Não há conteúdo para copiar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void AtualizarFonte()
        {
            // Notifica a mudança na propriedade FonteResultado
            txtResultado.FontSize = FonteResultado;
            AtualizarEstadoBotoesFonte();
        }

        private void AtualizarEstadoBotoesFonte()
        {
            btnAumentarFonte.IsEnabled = FonteResultado < FonteMaxima;
            btnDiminuirFonte.IsEnabled = FonteResultado > FonteMinima;
        }

        // Evento do botão Limpar
        private void BtnLimpar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Tem certeza de que deseja limpar os campos?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                txtPrompt.Clear();
                txtResultado.Clear();
            }
        }

        // Evento do botão Sair
        private void BtnSair_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Tem certeza de que deseja sair?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}