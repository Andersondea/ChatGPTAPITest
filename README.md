# ChatGPT OpenAI API Teste

Bem-vindo ao **ChatGPT OpenAI API Teste**, um aplicativo desktop desenvolvido em C# utilizando WPF (Windows Presentation Foundation). Este projeto demonstra como integrar a API do OpenAI ChatGPT em uma aplicação WPF, permitindo que os usuários interajam com o modelo de linguagem diretamente de uma interface gráfica amigável.

## Índice

- [Visão Geral](#visão-geral)
- [Funcionalidades](#funcionalidades)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Pré-requisitos](#pré-requisitos)
- [Configuração](#configuração)
- [Como Usar](#como-usar)
- [Bibliotecas de Terceiros](#bibliotecas-de-terceiros)
- [Recursos](#recursos)
- [Licença](#licença)
- [Contato](#contato)

## Visão Geral

Este aplicativo permite que você envie prompts para a API do ChatGPT e receba respostas diretamente no ambiente desktop. É uma ferramenta útil para testar e interagir com o modelo de IA sem a necessidade de utilizar um navegador ou interface web.

## Funcionalidades

- **Interface Intuitiva**: Design simples e fácil de usar.
- **Envio de Prompts**: Insira sua pergunta ou mensagem e envie para o ChatGPT.
- **Exibição de Respostas**: As respostas são exibidas em um campo de texto com suporte a quebra de linha e rolagem.
- **Configurações Personalizáveis**:
  - **Chave da API**: Armazene sua chave de API do OpenAI de forma segura.
  - **Configurações de Proxy**: Suporte a proxy com autenticação para ambientes corporativos.
- **Controle de Fonte**: Aumente ou diminua o tamanho da fonte da resposta para melhor legibilidade.
- **Funcionalidades Adicionais**:
  - **Copiar Resposta**: Copie a resposta do ChatGPT para a área de transferência.
  - **Limpar Campos**: Limpe o prompt e a resposta facilmente.
  - **Indicador de Carregamento**: Indicador de progresso enquanto aguarda a resposta da API.

## Tecnologias Utilizadas

- **C# com WPF**: Para a construção da interface gráfica e lógica de aplicação.
- **.NET 6.0**: Plataforma de desenvolvimento utilizada.
- **MahApps.Metro**: Biblioteca para aprimorar a interface WPF com um design moderno.
- **Newtonsoft.Json**: Para manipulação de dados JSON.
- **System.Net.Http**: Para realizar requisições HTTP para a API do OpenAI.

## Pré-requisitos

- **.NET 6.0 SDK**: Certifique-se de ter o SDK instalado em sua máquina.
- **Chave de API do OpenAI**: Necessária para acessar a API do ChatGPT.

## Configuração

1. **Clone o Repositório**:

   ```bash
   git clone https://github.com/seu-usuario/ChatGPTAPITest.git
   ```

2. **Navegue até o Diretório do Projeto**:

   ```bash
   cd ChatGPTAPITest
   ```

3. **Configurar as Informações Sensíveis**:

   As configurações sensíveis, como a chave da API e configurações de proxy, são armazenadas no arquivo `appsettings.json`. Este arquivo deve estar presente no diretório de execução do aplicativo.

   - **Criar ou Editar `appsettings.json`**:

     ```json
     {
       "OpenAI": {
         "ApiKey": "sua-chave-api-aqui"
       },
       "Proxy": {
         "UseProxy": false,
         "Address": "",
         "User": "",
         "Password": "",
         "Domain": ""
       }
     }
     ```

   - **Ajustar as Configurações**:

     - **ApiKey**: Insira sua chave de API do OpenAI.
     - **UseProxy**: Defina como `true` se precisar usar um proxy.
     - **Address**: Endereço do proxy.
     - **User** e **Password**: Credenciais para autenticação no proxy (se necessário).
     - **Domain**: Domínio do proxy (se aplicável).

4. **Configurar o Projeto no Visual Studio**:

   - Abra o projeto no Visual Studio.
   - Certifique-se de que o `appsettings.json` esteja com a propriedade **Copiar para o Diretório de Saída** definida como **Copiar sempre** ou **Copiar se mais novo**.

## Como Usar

1. **Executar o Aplicativo**:

   - Inicie o aplicativo através do Visual Studio ou executando o arquivo executável gerado na pasta `bin/Debug`.

2. **Inserir um Prompt**:

   - No campo **Prompt**, digite sua pergunta ou mensagem para o ChatGPT.

3. **Enviar o Prompt**:

   - Clique no botão **Enviar** para enviar o prompt para a API.

4. **Visualizar a Resposta**:

   - A resposta do ChatGPT será exibida no campo **Resposta**.

5. **Utilizar Funcionalidades Adicionais**:

   - **Aumentar/Diminuir Fonte**: Use os botões "+" e "-" para ajustar o tamanho da fonte da resposta.
   - **Copiar Resposta**: Clique no botão de copiar para copiar a resposta para a área de transferência.
   - **Limpar Campos**: Use o botão **Limpar** para apagar o conteúdo dos campos.
   - **Configurações**: Acesse o menu de configurações para ajustar a chave da API e configurações de proxy.

## Bibliotecas de Terceiros

- [**MahApps.Metro**](https://mahapps.com/): Fornece estilos e controles personalizados para WPF, proporcionando uma interface moderna.
- [**Newtonsoft.Json**](https://www.newtonsoft.com/json): Biblioteca popular para manipulação de JSON em .NET.
- **System.Net.Http**: Biblioteca padrão do .NET para fazer requisições HTTP.

## Recursos

- **Retry Policy**: Implementado mecanismo de retentativa para lidar com respostas de taxa limite da API (código 429).
- **Asynchronous Programming**: Uso de `async` e `await` para chamadas assíncronas à API.
- **Data Binding**: Utilizado para atualizar a interface do usuário de forma reativa.
- **Extensibilidade**: Código estruturado de forma a facilitar futuras expansões e melhorias.

## Licença

Este projeto está licenciado sob os termos da [MIT License](LICENSE).

## Contato

- **Desenvolvedor**: Anderson de Assis
- **Email**: anderson.net@outlook.com
- **GitHub**: [andersondea](https://github.com/andersondea)

---

**Nota**: Este aplicativo deve ser usado apenas para fins educacionais e de teste. Certifique-se de seguir os termos de uso da API do OpenAI e proteger sua chave de API.
