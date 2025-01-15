$(document).ready(function () {
    // Função para formatar o valor como moeda
    function formatCurrency(input) {
        let value = $(input).val();

        // Remove qualquer caractere que não seja número ou vírgula
        value = value.replace(/[^\d,]/g, '');

        // Converte para número decimal com duas casas
        value = parseFloat(value.replace(',', '.')).toFixed(2).toString().replace('.', ',');

        // Adiciona o prefixo "R$"
        $(input).val('R$ ' + value);
    }

    // Aplica a máscara nos inputs com a classe "money"
    $('.money').on('blur', function () {
        formatCurrency(this);
    });

    // Permite apenas números e vírgula durante a digitação
    $('.money').on('input', function () {
        let value = $(this).val();
        $(this).val(value.replace(/[^\d,]/g, ''));
    });
});