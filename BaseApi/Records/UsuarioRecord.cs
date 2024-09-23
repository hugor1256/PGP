namespace PGP.Records;

public record ListarUsuario(
    string Cpf,
    string Login,
    string Senha
    );

public record CadastrarUsuario(
    string Cpf,
    string Login,
    string Senha,
    string ConfirmarSenha
    );
    