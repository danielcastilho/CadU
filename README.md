# CadU
Sistema de Login e cadastro de Usuários
O propósito deste projeto .Net core é para aprender e praticar nossas habilidades e conhecimentos na plataforma dotnet core/5 com Web-API, JWT e também clientes consumindo em Angular, Vue ou React.

_Em fase de desenvolvimento_:
   isso será continuo, a idéia é sempre **melhorar**

### O que já temos:
- Sistema de autenticação e autorização baseado em JWT
- API para CRUD
- Testes via Postman
- Adição de testes unitários
- Utilização de banco relacional Postgres
- Script para rodar o postgres em docker (ele instala tambem o pgAdmin)

### Próximoes passos:
- Maior cobertura de testes unitários e integração "CI"
- Criação de client simples para CRUD baseado em Angular
- Adicionar arquivos para configuração e implementação de ambiente docker completo
- Sistema de migrações para atualizações de versão

## Para a criação do banco:
Na raiz do projeto tem arquivos para instalar o postgres e criar o database
- Executar o arquivo docker-env-dev-setup.sh se tiver o docker instalado ou instalar manualmente o servidor
- Ajustar o arquivo appSettings.json se preferir usar login diferente
- executar o arquivo createSchema.pgsql para criar o banco