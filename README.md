# BRQTrucks

No diretório BackEnd, há o projeto da API
E no FrontEnd, está o projeto do site

# Configurações

FRONTEND

Deve estar na versão 13+ do angular e node 6.14+

Consulte mais informações aqui: https://angular.io/docs


BACKEND

Deve estar como .NET6

Consulte mais informações aqui: https://dotnet.microsoft.com/en-us/download/dotnet/6.0


O projeto backend API, está configurada para usar a porta 5001.
Caso a mesma já esteja ocupada, desocupe-a ou mude a configuração no arquivo launchSettings.json. Escolhendo a última alternativa, será necessário alterar no FRONT (Irei explicar mais a frente)

Com o projeto devidamente configurado, aberto no visual studio / visual code e base de dados criada (pelo migrations), inicie uma nova instância (F5).

FRONT

Caso seja necessário alterar a url do endpoint da api, esta alteração deverá ser feita no arquivo environments.ts.

Após realizar todas as configurações, baixada todas as dependências necessárias no projeto angular, api backend rodando, execute o comando npm start no console direcionado para a pasta raiz do projeto Web
