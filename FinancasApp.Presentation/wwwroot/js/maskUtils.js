
class MaskUtils {
    static applyCurrencyMask(element) {
        // Remove tudo que não for número
        let valor = element.value.replace(/\D/g, '');

        // Converte para decimal e formata com duas casas decimais
        valor = (parseFloat(valor) / 100).toFixed(2);

        // Formata no padrão brasileiro com 'R$'
        element.value = 'R$ ' + valor.replace('.', ',');
    }
}
