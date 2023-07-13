![ploomes](https://github.com/jose-luan19/Teste-Ploomes/assets/54694573/584a2335-fd4b-4889-846a-3a2d71a14af2)
# Teste Ploomes (teste prático para Dev C# Jr)


## A API possui uma documentação Swagger que é iniciado usando o IIS Express, mas também é possivel iniciar a API sem o swagger na porta 8080
## Já há pré-configuração de ambientes de Development e de Production, e para facilitar a configuração criei o arquivo #appsettings.Development-Example.json#, basta renomea-lo para 'appsettings.Development.json' e infomar o servidor e nome do banco que irá criar ou conectar (lembrando que as migrations criaram o banco automaticamente na primeira execução)

### A API serve para controle de login e register de usuario, com algumas validações como de e-mail e e-mail unico por usuario . . .
### Também para upload de arquivos, o arquivo é convertido para base64 e salvo no banco
### E o foco principal da API que é aceitar inscrições para um Reality onde essa inscrição é realizada com nome, email e id's de arquivos que foram upados no banco de dados (as inscrições possuem um relacionamento N:N com os arquivos)

##### Vale a citar algumas libs que utilizei no projeto como AutoMapper, MediatR e BCrypt
###### Esta API fazia parte de um projeto full stack que fiz com .NET 5 e Angular, esse repositorio não está seguindo bem o git flow por conta que eu upei ele para cá e fiz apenas algumas alterações.
