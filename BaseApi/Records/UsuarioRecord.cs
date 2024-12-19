namespace PGP.Records;

public record ListarUsuarioRecord(
    string Cpf,
    string Login,
    string Senha
    );

public record CadastrarUsuarioRecord(
    string Cpf,
    string Login,
    string Senha,
    string ConfirmarSenha
    );

public record LogarUsuarioRecord(
    string Cpf,
    string Senha
);
    