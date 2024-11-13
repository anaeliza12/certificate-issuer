<div align="center"> <h1>Global Solution - Emissor de Certificados</h1></div>


### 🔎 Componentes:

Api.Certification.Issuer, possui funcionalidade de  receber a requisição de certificado, com objeto StudentModel 
passado como payload da request, juntamente com a função de consultar o certificado emitido passando o nome do estudante como query. Foi utilizado
o padrão mediator para arquitetura e organização das classes e funcionaliades do código. O serviço foi dividido em três projetos:
- Controller: requisições HTTPS
- Handler: intermediador da controller para a camada de serviços, classes Utils e Model
- Infra: lógica do serviço, conexão com o banco de dados local e Redis e injeção de dependência

Api.Processor, possui a funcionalidade de processar as solicitações de certificado da fila, vinda no rabbitmq e armazenar em formato PDF no diretório dentro do container.


 ### 📑 cURL:

`POST request para geração de certificado`

```
curl --location 'https://localhost:8080/api/Certificate/v1/generate' \
--header 'Content-Type: application/json' \
--data '{
    "StudentModel": {
        "Name": "Carlos Eduardo da Silva",
        "Nationality": "Brasileiro",
        "State": "Rio de Janeiro",
        "BirthDate": "08/09/1998",
        "RG": "12.345.678-9",
        "ConclusionDate": "15/07/2024",
        "IssueDate": "10/09/2024",
        "Course": "Engenharia de Computação",
        "WorkLoad": "4000",
        "Sign": "Carlos Eduardo",
        "Role": "Desenvolvedor"
    }
}'

```

`GET request para o retorno de certificado emitido`

```
curl --location 'https://localhost:8080/api/Certificate/v1/find?name=Carlos%20Eduardo%20da%20Silva'

```
