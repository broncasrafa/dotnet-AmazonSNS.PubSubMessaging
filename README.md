# AmazonSQS-getting-start-dotnet-core

Projeto para praticas na utilização do Amazon SQS, Lambda no .NET Core/.NET 7

Foi criado 2 projetos em .NET7 para representar o producer e o consumer.

> Producer - WebApi que produz/envia mensagens para uma fila SQS

> Consumer - .NET Core Lambda Function que consome/lê as mensagens da fila SQS. Foi usado a ferramenta do Visual Studio chamada
AWS Toolkit para o template modelo do AWS Lambda. E também pode-se usar o CloudWatch para visualizar os logs e confirmar que as mensagens foram lidas.

![Alt text](https://rsfranciscostorage.blob.core.windows.net/randomfiles/architecture.png)

![Csharp](https://img.shields.io/badge/csharp-019733?&style=for-the-badge&logo=csharp&logoColor=white)
![.NET7](https://img.shields.io/badge/.NET7-512BD4?logo=.net&logoColor=ffffff&style=for-the-badge)
![Visual Studio](https://img.shields.io/badge/VisualStudio-6C33AF?logo=visual%20studio&style=for-the-badge)
![AWS](https://img.shields.io/badge/AWS-%23FF9900.svg?style=for-the-badge&logo=amazon-aws&logoColor=white)

