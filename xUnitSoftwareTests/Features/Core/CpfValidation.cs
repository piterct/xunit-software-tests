namespace Features.Core
{
    public class CpfValidation
    {
        public bool IsValid(string cpf)
        {
            var multiplier1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplier2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            var hasCpf = cpf.Substring(0, 9);
            var soma = 0;

            for (var i = 0; i < 9; i++)
                soma += int.Parse(hasCpf[i].ToString()) * multiplier1[i];
            var resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            var digito = resto.ToString();
            hasCpf += digito;
            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += int.Parse(hasCpf[i].ToString()) * multiplier2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito += resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}
