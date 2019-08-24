# Portal Gateway REST

Projeto para Integração com SREP

O objetivo do Portal Gateway, é permitir acesso fácil e seguro ao dados do SREP Prime Ponto, através de diversas linguagens de programação.

Para tal adotamos o padrão de comunicação HTTPS, com autenticação Basic, trafegando dados no formato JSON, em requisições POST.
Como ferramenta para testes e exemplos escolhemos o SoapUI 5.5.0 Community, mas você pode utilizar o client REST de sua preferência.

Outra ferramenta muito util para usuário de Prompt é o CURL.

As requisições devem ser feitas para o endpoint base:
https://portal.primesw.com.br/gateway	

Seguido do PATH para o service desejado.
