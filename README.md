# Portal Gateway REST

Projeto para Integração com SREP

O objetivo do Portal Gateway, é permitir acesso fácil e seguro ao dados do SREP Prime Ponto, através de diversas linguagens de programação.

Para tal adotamos o padrão de comunicação HTTPS, com autenticação Basic, trafegando dados no formato JSON, em requisições POST.
Como ferramenta para testes e exemplos escolhemos o SoapUI 5.5.0 Community, mas você pode utilizar o client REST de sua preferência.

Outra ferramenta muito util para usuário de Prompt é o CURL.

As requisições devem ser feitas para o endpoint base:
https://portal.primesw.com.br/gateway	

O administrador utilizado na autenticação deverá possuir a permissão: "Integração"

Exemplo de solicitação para AFD (portaria 671):

(NFR = "99999999999999999" para REP-A)

curl -X POST -H 'Content-Type: application/json' -u "login:password" -d "{\"cnpj\":\"17930815000172\",\"nfr\":\"99999999999999999\", \"dataHoraInicio\": \"20180301\", \"dataHoraTermino\":\"20180330\"}" https://portal.primesw.com.br/gateway/rest/portaria671/afd

Assinado (p7s)
curl -X POST -H 'Content-Type: application/json' -u "main@primesw.com.br:536853" -d "{\"cnpj\":\"82.231.739/0001-79\",\"nfr\":\"00009003660002808\", \"dataHoraInicio\": \"20230101\", \"dataHoraTermino\":\"20230201\"}" https://portal.primesw.com.br/gateway/rest/portaria671/afd-signed --output afd.zip

* Filtrar por Device ID (opcional):

** JSON
curl -X POST -H 'Content-Type: application/json' -u "main@primesw.com.br:536853" -d "{\"cnpj\":\"83.310.441/0022-41\", \"deviceId\":\"546908\", \"nfr\":\"99999999999999999\", \"dataHoraInicio\": \"20240311\", \"dataHoraTermino\":\"20240311\"}" https://portal.primesw.com.br/gateway/rest/portaria671/afd

** ZIP com p7s
curl -X POST -H 'Content-Type: application/json' -u "main@primesw.com.br:536853" -d "{\"cnpj\":\"83.310.441/0022-41\", \"deviceId\":\"546908\", \"nfr\":\"99999999999999999\", \"dataHoraInicio\": \"20240311\", \"dataHoraTermino\":\"20240311\"}" https://portal.primesw.com.br/gateway/rest/portaria671/afd-signed --output afd.zip

Exemplo de solicitação para AFD (portaria 1510):

curl -X POST -H 'Content-Type: application/json' -u "login:password" -d "{\"nfr\":\"\", \"nsr\":0}" https://portal.primesw.com.br/gateway/rest/afd
curl -X POST -H 'Content-Type: application/json' -u "login:password" -d "{\"nfr\":\"\", \"dataHoraInicio\": \"\", \"dataHoraTermino\":\"\"}" https://portal.primesw.com.br/gateway/rest/afd

Exemplo de solicitação para inserção de registros REP-A:

*Obrigatorio informar CPF (caso contrário não emite AFD 671)
*Opcional informar CNPJ
*Opcional informar ID (max 20 caracteres)

curl -X POST -H 'Content-Type: application/json' -u "login:password" -d "{\"records\":[{\"cnpj\":\"17930815000172\",\"pis\":\"08474306343\",\"dateTime\":\"2022-10-05 12:00:00\",\"id\":\"meuid\",\"position\":{\"latitude\":1345.2,\"longitude\":65765.8,\"country\":\"BR\",\"uf\":\"SP\",\"city\":\"Sao Paulo\",\"address\":\"Av Paulista, 22838\"}},{\"cpf\":\"64661539087\",\"dateTime\":\"2022-10-05 13:00:00\",\"position\":{\"latitude\":1345.2,\"longitude\":65765.8,\"country\":\"BR\",\"uf\":\"SP\",\"city\":\"Sao Paulo\",\"address\":\"Av Paulista, 22838\"}}]}" https://portal.primesw.com.br/gateway/rest/repa/records_upload

Seguido do PATH para o service desejado.
