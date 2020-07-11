using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TatooReport.Models.ViewModels.User
{
    public class Create
    {
        [Required(ErrorMessage = "Por favor, informe o CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Por favor, informe o Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o Sobrenome")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o E-mail")]
        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        [Required(ErrorMessage = "Por favor, informe a Senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Por favor, preencha a Confirmação de Senha")]
        [Compare(nameof(Senha), ErrorMessage = "As senhas são diferentes")]
        public string ConfirmacaoSenha { get; set; }
    }
}
